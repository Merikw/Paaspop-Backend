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
            var thisCombination = GetCombination(Distance, Place.CrowdPercentage);
            var otherCombination = other.GetCombination(other.Distance, other.Place.CrowdPercentage);
            if (thisCombination > otherCombination) return 1;
            if (thisCombination == otherCombination) return 0;
            return -1;
        }

        public int GetCombination(Distance distance, Percentage crowdPercentage)
        {
            return distance.AbsoluteDistance + crowdPercentage.AbsolutePercentage * 5;
        }
    }
}