using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class LocationCoordinateInvalidException : CustomException
    {
        public LocationCoordinateInvalidException(double latitude, double longitude)
            : base($"Location with Longitude:\"{longitude}\" and Latitude: \"{latitude}\" is invalid.")
        {
        }

        public LocationCoordinateInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}