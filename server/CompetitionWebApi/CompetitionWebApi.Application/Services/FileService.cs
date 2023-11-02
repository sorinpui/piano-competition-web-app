using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace CompetitionWebApi.Application.Services;

public class FileService : IFilesService
{
    public async Task<string> UploadLargeFile(FileMultipartSection section)
    {
        var untrustedFileName = Path.GetFileName(section.FileName);

        if (!Path.GetExtension(untrustedFileName).Contains(".mp4"))
        {
            throw new InvalidRequestException()
            {
                Title = "Bad File Extension",
                ErrorMessage = "The file must be a performance video with mp4 extension."
            };
        } 

        string finalFileName = Path.GetRandomFileName() + Path.GetExtension(section.FileName);
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string directoryPath = Path.Combine(desktopPath, "CompetitionVideos");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, finalFileName);
        using var targetStream = File.Create(filePath);

        int chunkSize = 1024;
        byte[] buffer = new byte[chunkSize];
        int bytesRead;

        while ((bytesRead = await section.FileStream.ReadAsync(buffer)) > 0)
        {
            targetStream.Write(buffer, 0, bytesRead);
        }

        return filePath;
    }
}
