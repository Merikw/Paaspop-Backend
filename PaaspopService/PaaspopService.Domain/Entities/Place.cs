using System;
using PaaspopService.Common.DistanceBetweenCoordinates;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class Place : Model
    {
        public string Name { get; set; }
        public PlaceType Type { get; set; }
        public Percentage CrowdPercentage { get; set; }
        public LocationCoordinate Location { get; set; }

        public Distance GetDistanceFrom(LocationCoordinate from)
        {
            var distance = DistanceBetweenCoordinates.GetDistanceInMeters(from.Latitude, from.Longitude, Location.Latitude,
                Location.Longitude);
            return new Distance(distance);
        }

        public Percentage CalculateCrowdPercentage(int userCount, int addAmountOfUsers)
        {
            var peopleOnLocation = (double) userCount * CrowdPercentage.AbsolutePercentage / 100;
            peopleOnLocation = peopleOnLocation + addAmountOfUsers;
            return new Percentage(peopleOnLocation, (double) userCount);
        }
    }
}