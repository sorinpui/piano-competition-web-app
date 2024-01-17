using Microsoft.AspNetCore.WebUtilities;

namespace CompetitionApi.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadLargeFileAsync(FileMultipartSection section);
    }
}
