using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    public class PercentageInvalidException : CustomException
    {
        public PercentageInvalidException(int percentage)
            : base($"Percentage \"{percentage}\" is invalid. It should be between 0 and 100.")
        {
        }
    }
}