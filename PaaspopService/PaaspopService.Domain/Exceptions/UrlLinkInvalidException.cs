using System;

namespace PaaspopService.Domain.Exceptions
{
    public class UrlLinkInvalidException : Exception
    {
        public UrlLinkInvalidException(string urlLink)
            : base($"Url link \"{urlLink}\" is invalid.")
        {
        }
    }
}