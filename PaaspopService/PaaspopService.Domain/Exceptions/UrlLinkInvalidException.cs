using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class UrlLinkInvalidException : CustomException
    {
        public UrlLinkInvalidException(string urlLink)
            : base($"Url link \"{urlLink}\" is invalid.")
        {
        }

        public UrlLinkInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}