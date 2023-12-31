﻿using CompetitionWebApi.Application.Exceptions;
using Microsoft.Net.Http.Headers;

namespace CompetitionWebApi.Application.Helpers;

public static class MultipartRequestHelper
{
    public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

        if (string.IsNullOrWhiteSpace(boundary))
        {
            throw new InvalidRequestException() 
            {
                ErrorMessage = "This type of request must have a content type boundary."
            };
        }

        if (boundary.Length > lengthLimit)
        {
            throw new InvalidRequestException()
            {
                ErrorMessage = $"Multipart boundary length limit {lengthLimit} exceeded."
            };
        }

        return boundary;
    }

    public static bool IsMultipartContentType(string? contentType)
    {
        return !string.IsNullOrEmpty(contentType)
               && contentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase);
    }
}
