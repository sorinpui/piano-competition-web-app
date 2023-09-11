using Microsoft.AspNetCore.WebUtilities;

namespace CompetitionWebApi.Application.Interfaces;

public interface IFilesService
{
    Task<string> UploadLargeFile(FileMultipartSection section);
}
