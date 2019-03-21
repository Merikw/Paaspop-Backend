﻿using System;
using System.Collections.Generic;
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
        public ISet<string> UsersOnPlace { get; set; } = new HashSet<string>();

        public Distance GetDistanceFrom(LocationCoordinate from)
        {
            var distance = DistanceBetweenCoordinates.GetDistanceInMeters(from.Latitude, from.Longitude, Location.Latitude,
                Location.Longitude);
            return new Distance(distance);
        }

        public Percentage CalculateCrowdPercentage(int userCount, int amountOfUsers, Operator opperator)
        {
            var peopleOnLocation = (double) userCount * CrowdPercentage.AbsolutePercentage / 100;
            if (opperator == Operator.Plus)
            {
                peopleOnLocation = peopleOnLocation + amountOfUsers;
            }
            else
            {
                peopleOnLocation = peopleOnLocation - amountOfUsers;
            }
            return new Percentage(peopleOnLocation, (double) userCount);
        }
    }
}