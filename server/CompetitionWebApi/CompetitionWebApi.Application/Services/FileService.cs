using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using static System.Collections.Specialized.BitVector32;

namespace CompetitionWebApi.Application.Services;

public class FileService : IFileService
{
    public async Task<string> UploadLargeFile(FileMultipartSection section)
    {
        if (!Path.GetExtension(section.FileName.ToLowerInvariant()).Contains(".mp4"))
        {
            throw new InvalidRequestException("Invalid file extension. Only mp4 video format is allowed.");
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
