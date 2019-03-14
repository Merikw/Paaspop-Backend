using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    public class LocationCoordinateInvalidException : CustomException
    {
        public LocationCoordinateInvalidException(double latitude, double longitude)
            : base($"Location with Longitude:\"{longitude}\" and Latitude: \"{latitude}\" is invalid.")
        {
        }
    }
}