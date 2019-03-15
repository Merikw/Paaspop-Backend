using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    public class InvalidPerformanceTimeException : CustomException
    {
        public InvalidPerformanceTimeException(string time)
            : base($"Performance Time \"{time}\" is invalid.")
        {
        }
    }
}
