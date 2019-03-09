using System;

namespace PaaspopService.Domain.Exceptions
{
    public class InvalidPerformanceTimeException : Exception
    {
        public InvalidPerformanceTimeException(string time)
            : base($"Performance Time \"{time}\" is invalid.")
        {
        }
    }
}
