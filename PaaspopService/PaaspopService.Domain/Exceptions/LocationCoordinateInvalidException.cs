using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Exceptions
{
    public class LocationCoordinateInvalidException : Exception
    {
        public LocationCoordinateInvalidException(double latitude, double longitude)
            : base($"Location with Longitude:\"{longitude}\" and Latitude: \"{latitude}\" is invalid.")
        {
        }
    }
}
