using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Enums;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace CompetitionWebApi.Application.Services;

public class PerformanceService : IPerformancesService
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

    public async Task CreatePerformanceInfoAsync(PerformanceRequest request)
    {
        int nameIdentifierClaimValue = _jwtService.GetNameIdentifier();
        
        if (nameIdentifierClaimValue != request.UserId)
        {
            throw new ForbiddenException()
            {
                ErrorMessage = "This account does not have rights to create performances on behalf of other users."
            };
        }

        List<Performance> performancesFromDb = await _unitOfWork.PerformanceRepository.GetPerformancesByUserId(request.UserId);

        if (performancesFromDb.Any(x => x.Piece.Period.Equals(request.Period)))
        {
            throw new DuplicateException()
            {
                ErrorMessage = $"There's already an uploaded performance from {request.Period} period."
            };
        }

        Performance newPerformance = Mapper.PerformanceRequestToPerformanceEntity(request);

        await _unitOfWork.PerformanceRepository.CreatePerformanceAsync(newPerformance);
        await _unitOfWork.SaveAsync();
    }

    public async Task SavePerformanceVideoAsync(string boundary, Stream requestBody, int performanceId)
    {
        Performance? performanceFromDb = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(performanceId);

        if (performanceFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                ErrorMessage = $"The performance with id {performanceId} doesn't exist."
            };
        }

        string? videoFilePath = performanceFromDb.VideoUri;

        if (videoFilePath != null)
        {
            throw new DuplicateException()
            {
                ErrorMessage = "This performance already has a video uploaded."
            };
        }

        var reader = new MultipartReader(boundary, requestBody);
        var section = await reader.ReadNextSectionAsync();

        while (section != null)
        {
            var contentDisposition = ContentDispositionHeaderValue.Parse(section.ContentDisposition);

            if (contentDisposition.IsFileDisposition())
            {
                var fileSection = section.AsFileSection();

                videoFilePath = await _fileService.UploadLargeFile(fileSection);
            }

            section = await reader.ReadNextSectionAsync();
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
                ErrorMessage = $"The performance with id {performanceId} doesn't exist."
            };
        }

        if (performanceFromDb.VideoUri == null)
        {
            throw new VideoNotFoundException()
            {
                ErrorMessage = $"The video from this performance is not uploaded yet."
            };
        }

        User? user = await _unitOfWork.UserRepository.GetUserByIdAsync(performanceFromDb.UserId);

        Piece piece = performanceFromDb.Piece;

        string pieceName = piece.Name.Replace(' ', '_');
        string composer = piece.Composer.Replace(' ', '_');

        string fileName = $"{user.FirstName}_{user.LastName}_{pieceName}_{composer}.mp4";
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
