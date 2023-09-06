using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace CompetitionWebApi.Application.Services;

public class PerformanceService : IPerformanceService
{
    private readonly IFileService _fileService;
    private readonly IValidationService _validationService;
    private readonly IUnitOfWork _unitOfWork;

    public PerformanceService(IFileService fileService, IValidationService validationService, IUnitOfWork unitOfWork)
    {
        _fileService = fileService;
        _validationService = validationService;
        _unitOfWork = unitOfWork;
    }

    public async Task CreatePerformanceInfoAsync(PerformanceRequest request)
    {
        User? userFromDb = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);

        if (userFromDb == null)
        {
            throw new EntityNotFoundException($"The user with id {request.UserId} doesn't exist.");
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
            throw new EntityNotFoundException($"The performance with id {performanceId} doesn't exist.");
        }

        string videoFilePath = string.Empty;

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

        if (string.IsNullOrEmpty(videoFilePath)) 
        {
            throw new InvalidRequestException("Video performance missing.");
        }

        performanceFromDb.VideoUri = videoFilePath;

        await _unitOfWork.SaveAsync();
    }

    public async Task<Stream> GetPerformanceVideoAsync(int performanceId)
    {
        Performance? performanceFromDb = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(performanceId);

        if (performanceFromDb == null)
        {
            throw new EntityNotFoundException($"The performance with id {performanceId} doesn't exist.");
        }

        var stream = new FileStream(performanceFromDb.VideoUri, FileMode.Open, FileAccess.Read);

        return stream;
    }
}
