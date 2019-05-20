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
            var distance = DistanceBetweenCoordinates.GetDistanceInMeters(from.Latitude, from.Longitude,
                Location.Latitude,
                Location.Longitude);
            return new Distance(distance);
        }

        public Percentage CalculateCrowdPercentage(int userCount, int amountOfUsers, Operator mathOperator)
        {
            var crowdPercentage = UsersOnPlace.Count;
            if (mathOperator == Operator.Plus)
                crowdPercentage = crowdPercentage + amountOfUsers;
            else if (mathOperator == Operator.Minus) crowdPercentage = crowdPercentage - amountOfUsers;

            return crowdPercentage >= 0 ? new Percentage(crowdPercentage, userCount) : CrowdPercentage;
        }

        protected bool Equals(Place other)
        {
            return string.Equals(Name, other.Name)
                   && Type == other.Type 
                   && Equals(CrowdPercentage, other.CrowdPercentage)
                   && Equals(Location, other.Location) 
                   && UsersOnPlace.SetEquals(other.UsersOnPlace);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Place) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int) Type;
                hashCode = (hashCode * 397) ^ (CrowdPercentage != null ? CrowdPercentage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UsersOnPlace != null ? UsersOnPlace.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}