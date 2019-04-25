using System;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Places.Queries.GetBestPlacesQuery
{
    public class BestPlace : IComparable<BestPlace>
    {
        public Place Place { get; set; }
        public Distance Distance { get; set; }
        public Percentage CrowdPercentage { get; set; }

        public int CompareTo(BestPlace other)
        {
            if (this > other) return 1;
            if (this == other) return 0;
            return -1;
        }

        public static int GetCombination(Distance distance, Percentage crowdPercentage)
        {
            return distance.AbsoluteDistance + crowdPercentage.AbsolutePercentage * 2;
        }

        public static bool operator == (BestPlace left, BestPlace right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }
        public static bool operator > (BestPlace left, BestPlace right)
        {
            return decimal.Compare(GetCombination(left.Distance, left.CrowdPercentage), GetCombination(right.Distance, right.CrowdPercentage)) > 0;
        }
        public static bool operator < (BestPlace left, BestPlace right)
        {
            return decimal.Compare(GetCombination(left.Distance, left.CrowdPercentage), GetCombination(right.Distance, right.CrowdPercentage)) < 0;
        }
        public static bool operator >= (BestPlace left, BestPlace right)
        {
            return decimal.Compare(GetCombination(left.Distance, left.CrowdPercentage), GetCombination(right.Distance, right.CrowdPercentage)) >= 0;
        }
        public static bool operator <= (BestPlace left, BestPlace right)
        {
            return decimal.Compare(GetCombination(left.Distance, left.CrowdPercentage), GetCombination(right.Distance, right.CrowdPercentage)) <= 0;
        }
        public static bool operator != (BestPlace left, BestPlace right)
        {
            return !(left == right);
        }

        protected bool Equals(BestPlace other)
        {
            return Equals(Place, other.Place) && Equals(Distance, other.Distance) && Equals(CrowdPercentage, other.CrowdPercentage);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((BestPlace)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Place != null ? Place.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Distance != null ? Distance.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CrowdPercentage != null ? CrowdPercentage.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}