using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Application.Infrastructure.Exceptions
{
    [Serializable]
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(string failure)
            : base($"Validation failure has occurred ${failure}")
        {
        }

        protected CustomValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}