using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class PercentageInvalidException : CustomException
    {
        public PercentageInvalidException(int percentage)
            : base($"Percentage \"{percentage}\" is invalid. It should be between 0 and 100.")
        {
        }

        public PercentageInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}