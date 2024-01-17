using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Exceptions;
using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Enums;
using CompetitionApi.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace CompetitionApi.Application.Services;

public class RenditionService : IRenditionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IStorageService _storageService;

    public RenditionService(IUnitOfWork unitOfWork, IJwtService jwtService, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _storageService = storageService;
    }

    public async Task<ApiResponse<string>> CreateRenditionAsync(HttpRequest httpRequest)
    {
        var (request, fileSection) = await ParseAndValidateMultipartRequest(httpRequest);

        int userId = _jwtService.GetSubjectClaim();

        List<Rendition> renditions = await _unitOfWork.RenditionRepository.FindRenditionsByOwnerIdAsync(userId);
        bool renditionExists = renditions
            .Any(x => x.Piece.Period.ToString().Equals(request.Period, StringComparison.InvariantCultureIgnoreCase));

        if (renditionExists)
        {
            return new ApiResponse<string>(false, "There's already an uploaded rendition from this period.", null);
        }

        // process the file
        string videoPath = await _storageService.UploadLargeFileAsync(fileSection);

        // save the rendition to db
        User? performer = await _unitOfWork.UserRepository.FindUserById(userId);
        Rendition rendition = Mapper.CreateRenditionRequestToRenditionEntity(request, videoPath, performer!);

        await _unitOfWork.RenditionRepository.CreateRenditionAsync(rendition);
        await _unitOfWork.SaveAllChangesAsync();

        return new ApiResponse<string>(false, "The rendition has been uploaded successfully.", null);
    }

    public async Task<(string?, FileStream?)> DownloadRenditionVideo(int renditionId)
    {
        Rendition? rendition = await _unitOfWork.RenditionRepository.FindRenditionByIdAsync(renditionId);

        if (rendition == null)
        {
            throw new EntityNotFoundException("The rendition with the specified id doesn't exist.");
        }

        string fileExtension = Path.GetExtension(rendition.VideoUrl).ToLower();
        string renditionInfo = $"{rendition.User.FirstName}_{rendition.User.LastName}_{rendition.Piece.Name}_{rendition.Piece.Composer}";
        string fileName = $"{renditionInfo}{fileExtension}".Replace(' ', '_');

        try
        {
            FileStream fileStream = new FileStream(rendition.VideoUrl, FileMode.Open, FileAccess.Read);

            return (fileName, fileStream);
        }
        catch (IOException ex)
        {
            // log exception
            return (null, null);
        }
    }

    public async Task<ApiResponse<List<RenditionSummaryDto>>> GetAllRenditionsAsync()
    {
        List<Rendition> renditionsFromDb = await _unitOfWork.RenditionRepository.GetAllRenditionsAsync();

        List<RenditionSummaryDto> renditions = renditionsFromDb
            .Select(Mapper.RenditionEntityToRenditionSummaryDto)
            .ToList();

        return new ApiResponse<List<RenditionSummaryDto>>(true, "Renditions retrieved successfully", renditions);
    }

    public async Task<ApiResponse<RenditionDto>> GetRenditionByIdAsync(int renditionId)
    {
        Rendition? renditionFromDb = await _unitOfWork.RenditionRepository.FindRenditionByIdAsync(renditionId);

        if (renditionFromDb == null)
        {
            return new ApiResponse<RenditionDto>(false, "The rendition with the specified id doesn't exist.", null);
        }

        RenditionDto rendition = Mapper.RenditionEntityToRenditionDto(renditionFromDb, CalculateScore(renditionFromDb.Score));

        return new ApiResponse<RenditionDto>(true, "Rendition retrieved successfully", rendition);
    }

    private static double? CalculateScore(Score? score)
    {
        if (score == null) return null;

        double overallScore = (double)(score.Interpretation + score.Technique + score.Difficulty) / 3;

        return Math.Round(overallScore, 2); // to be modified
    }

    private static async Task<(CreateRenditionRequest, FileMultipartSection)> ParseAndValidateMultipartRequest(HttpRequest request)
    {
        var mediaTypeHeader = MediaTypeHeaderValue.Parse(request.ContentType);
        var boundary = HeaderUtilities.RemoveQuotes(mediaTypeHeader.Boundary.Value).Value!;
        var reader = new MultipartReader(boundary, request.Body);

        // Parse each section of the form
        var section = await reader.ReadNextSectionAsync();

        // get the first section which should be information about the rendition (piece, name etc.)
        var dataSection = section.AsFormDataSection();

        if (dataSection == null)
        {
            string message = "The first form section must be information about the rendition (piece, composer and period).";
            throw new BadSectionOrderException(HttpStatusCode.BadRequest, message);
        }

        string json = await dataSection.GetValueAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        CreateRenditionRequest renditionRequest = JsonSerializer.Deserialize<CreateRenditionRequest>(json, options)!;

        // check if the period string is a valid member of the Period enum
        if (!Enum.TryParse<Period>(renditionRequest.Period, true, out _))
        {
            throw new BadRequestException("The period must be baroque, classical or romantic.");
        }

        // the information about the piece at this point are GOOD

        section = await reader.ReadNextSectionAsync();

        if (section == null)
        {
            throw new BadRequestException("The video file of the rendition is missing.");
        }

        // get the file section and check the file
        var fileSection = section.AsFileSection();

        if (fileSection == null)
        {
            throw new BadRequestException("This section has to be a file section with the rendition video.");
        }

        // the file at this point is probably GOOD

        return (renditionRequest, fileSection);
    }
}
