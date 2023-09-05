using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Helpers;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using CompetitionWebApi.Attributes;
using CompetitionWebApi.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace CompetitionWebApi.Controllers;

[Route("api/[controller]s")]
[ApiController]
[DisableFormValueModelBinding]
public class PerformanceController : ControllerBase
{
    private readonly int _maxBoundaryLength = 70;

    [RequestSizeLimit(300 * 1024 * 10^6)]
    public async Task<IActionResult> UploadPerformance()
    {
        string? contentType = Request.ContentType;

        if (!MultipartRequestHelper.IsMultipartContentType(contentType))
        {
            return BadRequest(
                new ErrorResponse
                {
                    Type = "about:blank",
                    Title = "Wrong media type.",
                    Status = (int)HttpStatusCode.BadRequest
                });
        }

        string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(contentType), _maxBoundaryLength);
        var reader = new MultipartReader(boundary, Request.Body);
        var section = await reader.ReadNextSectionAsync();

        PerformanceRequest request = new PerformanceRequest();

        while (section != null)
        {
            var contentDisposition = ContentDispositionHeaderValue.Parse(section.ContentDisposition);

            if (contentDisposition.IsFileDisposition())
            {
                var fileSection = section.AsFileSection();

                if (!Path.GetExtension(fileSection.FileName.ToLowerInvariant()).Contains(".mp4"))
                {
                    throw new InvalidRequestException("Invalid file extension. Only specific video formats are allowed.");
                }

                string finalFileName = Path.GetRandomFileName() + Path.GetExtension(fileSection.FileName);
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string finalDirectoryPath = Path.Combine(desktopPath, "CompetitionVideos");

                if (!Directory.Exists(finalDirectoryPath))
                {
                    Directory.CreateDirectory(finalDirectoryPath);
                }

                string filePath = Path.Combine(finalDirectoryPath, finalFileName);
                using var targetStream = System.IO.File.Create(filePath);

                int chunkSize = 1024;
                byte[] buffer = new byte[chunkSize];
                int bytesRead;

                while ((bytesRead = await fileSection.FileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    targetStream.Write(buffer, 0, bytesRead);
                }

                request.VideoUri = filePath;
            }

            if (contentDisposition.IsFormDisposition())
            {
                
            }

            section = await reader.ReadNextSectionAsync();
        }

        return Created(string.Empty, new SuccessResponse<string> { StatusCode = HttpStatusCode.Created, Payload = string.Empty });
    }
}
