using Microsoft.AspNetCore.WebUtilities;

namespace CompetitionWebApi.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadLargeFile(FileMultipartSection section);
}
