using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    public class DistanceInvalidException : CustomException
    {
        public DistanceInvalidException(int distance)
            : base($"Distance \"{distance}\" is invalid.")
        {
        }
    }
}
