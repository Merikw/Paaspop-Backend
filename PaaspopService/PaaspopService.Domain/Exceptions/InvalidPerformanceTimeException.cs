using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class InvalidPerformanceTimeException : CustomException
    {
        public InvalidPerformanceTimeException(string time)
            : base($"Performance Time \"{time}\" is invalid.")
        {
        }

        public InvalidPerformanceTimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
