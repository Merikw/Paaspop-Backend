using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    public class UrlLinkInvalidException : CustomException
    {
        public UrlLinkInvalidException(string urlLink)
            : base($"Url link \"{urlLink}\" is invalid.")
        {
        }
    }
}