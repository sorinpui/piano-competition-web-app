using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace CompetitionWebApi.Application.Services;

public class PerformanceService : IPerformanceService
{
    private readonly IFilesService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public PerformanceService(IFilesService fileService, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<IActionResult> CreatePerformanceInfoAsync(PerformanceRequest request)
    {
        int userId = _jwtService.GetSubjectClaim();

        List<Performance> performancesFromDb = await _unitOfWork.PerformanceRepository.GetPerformancesByUserId(userId);

        bool pieceAlreadyExists = performancesFromDb.Any(x => x.Piece.Period.Equals(request.Period));
        
        if (pieceAlreadyExists)
        {
            var errorResponse = new ErrorResponse
            {
                Title = "Duplicate Performance",
                Detail = $"There's already an uploaded performance from {request.Period} period."
            };

            return new ConflictObjectResult(errorResponse);
        }

        Performance newPerformance = Mapper.PerformanceRequestToPerformanceEntity(request, userId);

        await _unitOfWork.PerformanceRepository.CreatePerformanceAsync(newPerformance);
        await _unitOfWork.SaveAsync();

        var successResponse = new SuccessResponse<string>
        {
            Message = "Performance information created successfully.",
            Payload = null
        };

        return new CreatedResult(string.Empty, successResponse);
    }

    public async Task SavePerformanceVideoAsync(string boundary, Stream requestBody, int performanceId)
    {
        Performance? performanceFromDb = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(performanceId);

        if (performanceFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                Title = "Resource Not Found",
                ErrorMessage = $"The performance with id {performanceId} doesn't exist."
            };
        }

        string? videoFilePath = performanceFromDb.VideoUri;

        if (videoFilePath != null)
        {
            throw new DuplicateException()
            {
                Title = "Duplicate Video",
                ErrorMessage = "This performance already has a video uploaded."
            };
        }

        var reader = new MultipartReader(boundary, requestBody);
        var section = await reader.ReadNextSectionAsync();

        var contentDisposition = ContentDispositionHeaderValue.Parse(section.ContentDisposition);

        if (contentDisposition.IsFileDisposition())
        {
            var fileSection = section.AsFileSection();

            videoFilePath = await _fileService.UploadLargeFile(fileSection);
        }
        
        if (videoFilePath == null)
        {
            throw new VideoNotFoundException
            {
                Title = "Missing Video",
                ErrorMessage = "Performance video was not uploaded."
            };
        }

        performanceFromDb.VideoUri = videoFilePath;

        await _unitOfWork.SaveAsync();
    }

    public async Task<PerformanceVideoDto> GetPerformanceVideoAsync(int performanceId)
    {
        Performance? performanceFromDb = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(performanceId);

        if (performanceFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                Title = "Performance Not Found",
                ErrorMessage = $"The performance with id {performanceId} doesn't exist."
            };
        }

        if (performanceFromDb.VideoUri == null)
        {
            throw new VideoNotFoundException()
            {
                Title = "Video Not Found",
                ErrorMessage = $"The video from this performance has not been uploaded yet."
            };
        }

        User? user = await _unitOfWork.UserRepository.GetUserByIdAsync(performanceFromDb.UserId);

        Piece piece = performanceFromDb.Piece;

        string pieceName = piece.Name.Replace(' ', '_');
        string composer = piece.Composer.Replace(' ', '_');

        string fileName = $"{user!.FirstName}_{user.LastName}_{pieceName}_{composer}.mp4";
        FileStream stream = new(performanceFromDb.VideoUri, FileMode.Open, FileAccess.Read);

        return new PerformanceVideoDto() 
        {
            VideoStream = stream,
            FileName = fileName 
        };
    }

    public async Task<List<PerformanceDto>> GetAllPerformancesAsync()
    {
        List<Performance> performances = await _unitOfWork.PerformanceRepository.GetAllPerformancesAsync();
        List<User> users = await _unitOfWork.UserRepository.GetAllUsersAsync();

        var performancesDto = from performance in performances
                              join user in users on performance.UserId equals user.Id
                              select Mapper.PerformanceEntityToPerformanceDto(performance, user);

        return performancesDto.ToList();
    }
}
