using CompetitionApi.Application.Exceptions;
using CompetitionApi.Application.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace CompetitionApi.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _storagePath;
        private readonly string[] _permittedFileExtensions = [".mp4", ".mov"];

        public StorageService()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            _storagePath = Path.Combine(desktopPath, "CompetitionVideos");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<string> UploadLargeFileAsync(FileMultipartSection section)
        {
            string fileExtension = Path.GetExtension(section.FileName);
            
            if (!_permittedFileExtensions.Contains(fileExtension.ToLower()))
            {
                string message = $"The video file extension must be one of the following: {string.Join(", ", _permittedFileExtensions)}";

                throw new BadRequestException(message);
            }

            string trustedFileName = Path.GetRandomFileName();
            string finalFilePath = Path.Combine(_storagePath, $"{trustedFileName}.{fileExtension}");

            using var targetStream = File.Create(finalFilePath);
            await section.FileStream.CopyToAsync(targetStream);

            return finalFilePath;
        }
    }
}
