using System;
using System.Collections.Generic;
using System.Linq;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class Performance : Model, IComparable<Performance>
    {
        public PerformanceTime PerformanceTime { get; set; }
        public Percentage InterestPercentage { get; set; }
        public Stage Stage { get; set; }
        public Artist Artist { get; set; }
        public ISet<string> UsersFavoritedPerformance { get; set; } = new HashSet<string>();
        public int PerformanceId { get; set; }

        public int CompareTo(Performance other)
        {
            if (PerformanceTime == null) return 1;
            if (other.PerformanceTime == null) return -1;
            if (PerformanceTime.Day == other.PerformanceTime.Day &&
                BetweenHandler.IsInBetween(Convert.ToInt32(PerformanceTime.StartTime.Substring(0, 2)), 0, 8) &&
                 !BetweenHandler.IsInBetween(Convert.ToInt32(other.PerformanceTime.StartTime.Substring(0, 2)), 0, 8))
            {
                return 1;
            }

            if (PerformanceTime.Day == other.PerformanceTime.Day &&
                !BetweenHandler.IsInBetween(Convert.ToInt32(PerformanceTime.StartTime.Substring(0, 2)), 0, 8) &&
                BetweenHandler.IsInBetween(Convert.ToInt32(other.PerformanceTime.StartTime.Substring(0, 2)), 0, 8))
            {
                return -1;
            }
            return string.Compare(PerformanceTime.PerformanceTimeText, other.PerformanceTime.PerformanceTimeText, StringComparison.Ordinal);
        }
            
        public Percentage CalculateInterestPercentage(int userCount, int amountOfUsers, Operator opperator)
        {
            var interestPercentage = UsersFavoritedPerformance.Count;
            if (opperator == Operator.Plus)
            {
                interestPercentage = interestPercentage + amountOfUsers;
            }
            else if(opperator == Operator.Minus)
            {
                interestPercentage = interestPercentage - amountOfUsers;
            }

            return interestPercentage >= 0 ? new Percentage(interestPercentage, (double) userCount) : InterestPercentage;
        }

        public static List<Performance> GetSuggestions(List<Performance> favoritesFromUser, List<Performance> performances)
        {
            var favoriteGenres = favoritesFromUser.SelectMany(p => p.Artist.Genres).ToList().GroupBy(item => item).OrderByDescending(group => group.Count()).Select(g => g.Key).Take(3).ToList();
            return performances.Where(p => favoritesFromUser.All(fp => fp.Id != p.Id)).OrderByDescending(performance =>
                performance.Artist.Genres.Intersect(favoriteGenres).Count()).Take(15).ToList();
        }
    }
}
