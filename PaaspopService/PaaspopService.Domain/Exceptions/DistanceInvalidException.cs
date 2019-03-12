using System;

namespace PaaspopService.Domain.Exceptions
{
    public class DistanceInvalidException : Exception
    {
        public DistanceInvalidException(int distance)
            : base($"Distance \"{distance}\" is invalid.")
        {
        }
    }
}
