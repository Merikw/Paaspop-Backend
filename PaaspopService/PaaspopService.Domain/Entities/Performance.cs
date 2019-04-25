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
            if (this > other) return 1;
            if (this < other) return -1;
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
            var favoriteGenres = favoritesFromUser.SelectMany(p => p.Artist.Genres).GroupBy(item => item).OrderByDescending(group => group.Count()).Select(g => g.Key).Take(3);
            return performances.Where(p => favoritesFromUser.All(fp => fp.Id != p.Id)).OrderByDescending(performance =>
                performance.Artist.Genres.Intersect(favoriteGenres).Count()).Take(10).ToList();
        }

        public static bool operator == (Performance left, Performance right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }
        public static bool operator > (Performance left, Performance right)
        {
            return left.PerformanceTime.Day == right.PerformanceTime.Day &&
                   BetweenHandler.IsInBetween(Convert.ToInt32(left.PerformanceTime.StartTime.Substring(0, 2)), 0, 8) &&
                   !BetweenHandler.IsInBetween(Convert.ToInt32(right.PerformanceTime.StartTime.Substring(0, 2)), 0, 8);
        }
        public static bool operator < (Performance left, Performance right)
        {
            return left.PerformanceTime.Day == right.PerformanceTime.Day &&
                   !BetweenHandler.IsInBetween(Convert.ToInt32(left.PerformanceTime.StartTime.Substring(0, 2)), 0, 8) &&
                   BetweenHandler.IsInBetween(Convert.ToInt32(right.PerformanceTime.StartTime.Substring(0, 2)), 0, 8);
        }
        public static bool operator >= (Performance left, Performance right)
        {
            return left.PerformanceTime.Day == right.PerformanceTime.Day
                    && BetweenHandler.IsInBetween(Convert.ToInt32(left.PerformanceTime.StartTime.Substring(0, 2)), 0, 8)
                    && !BetweenHandler.IsInBetween(Convert.ToInt32(right.PerformanceTime.StartTime.Substring(0, 2)), 0, 8) 
                    || left.PerformanceTime.StartTime.Equals(right.PerformanceTime.StartTime);
        }
        public static bool operator <= (Performance left, Performance right)
        {
            return left.PerformanceTime.Day == right.PerformanceTime.Day &&
                    !BetweenHandler.IsInBetween(Convert.ToInt32(left.PerformanceTime.StartTime.Substring(0, 2)), 0, 8) &&
                    BetweenHandler.IsInBetween(Convert.ToInt32(right.PerformanceTime.StartTime.Substring(0, 2)), 0, 8)
                    || left.PerformanceTime.StartTime.Equals(right.PerformanceTime.StartTime);
        }
        public static bool operator != (Performance left, Performance right)
        {
            return !(left == right);
        }

        protected bool Equals(Performance other)
        {
            return Equals(PerformanceTime, other.PerformanceTime) 
                   && Equals(InterestPercentage, other.InterestPercentage)
                   && Equals(Stage, other.Stage) && Equals(Artist, other.Artist) 
                   && Equals(UsersFavoritedPerformance, other.UsersFavoritedPerformance) 
                   && PerformanceId == other.PerformanceId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Performance)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PerformanceTime != null ? PerformanceTime.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InterestPercentage != null ? InterestPercentage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Stage != null ? Stage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Artist != null ? Artist.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UsersFavoritedPerformance != null ? UsersFavoritedPerformance.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PerformanceId;
                return hashCode;
            }
        }
    }
}
