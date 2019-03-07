using System.Collections.Generic;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class LocationCoordinate : ValueObject
    {
        private LocationCoordinate()
        {
        }

        public LocationCoordinate(double latitude, double longitude)
        {
            if (!BetweenHandler.IsInBetween(latitude, -90, 90) || !BetweenHandler.IsInBetween(longitude, -180, 180))
                throw new LocationCoordinateInvalidException(latitude, longitude);

            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}