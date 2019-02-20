using System;

namespace PaaspopService.Domain.Exceptions
{
    public class PercentageInvalidException : Exception
    {
        public PercentageInvalidException(int percentage)
            : base($"Percentage \"{percentage}\" is invalid. It should be between 0 and 100.")
        {
        }
    }
}
