using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Enums;
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

    public async Task CreatePerformance(string boundary, Stream requestBody)
    {
        PerformanceRequest request = new();
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

            if (contentDisposition.IsFormDisposition())
            {
                var dataSection = section.AsFormDataSection();

                string formName = dataSection.Name;
                string formValue = await dataSection.GetValueAsync();

                switch (formName)
                {
                    case "PieceName":
                        request.PieceName = formValue; 
                        break;

                    case "Composer":
                        request.Composer = formValue;
                        break;

                    case "Period":
                        request.Period = (Period)int.Parse(formValue); 
                        break;

                    case "UserId":
                        request.UserId = int.Parse(formValue);
                        break;

                    default:
                        throw new InvalidRequestException("Only the following form fields are allowed: PieceName, Composer, Period and UserId");
                }
            }

            section = await reader.ReadNextSectionAsync();
        }

        await _validationService.ValidateRequest(request);

        User? userFromDb = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);

        if (userFromDb == null)
        {
            throw new UserNotFoundException($"The user with id {request.UserId} doesn't exist.");
        }
      
        Performance newPerformance = Mapper.PerformanceRequestToPerformanceEntity(request, videoFilePath);
       
        await _unitOfWork.PerformanceRepository.CreatePerformance(newPerformance);
        await _unitOfWork.SaveAsync();
    }
}
