using PaaspopService.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;

namespace PaaspopService.Domain.ValueObjects
{
    public class LocationCoordinate : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private LocationCoordinate() { }

        public LocationCoordinate Create(double latitude, double longitude)
        {
            if(BetweenHandler.IsInBetween(latitude, -90, 90) 
                && BetweenHandler.IsInBetween(longitude, -180, 180))
            {
                return new LocationCoordinate
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            }

            throw new LocationCoordinateInvalidException(latitude, longitude);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
