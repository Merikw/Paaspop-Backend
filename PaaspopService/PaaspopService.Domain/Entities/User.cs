using System.Collections.Generic;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class User : Model
    {
        public Gender Gender { get; set; }
        public Age Age { get; set; }
        public bool WantsWeatherForecast { get; set; } = true;
        public bool WantsWaterDrinkNotification { get; set; } = true;
        public LocationCoordinate CurrentLocation { get; set; } = null;
        public ISet<string> FavoritePerformances { get; set; } = new HashSet<string>();
        public string NotificationToken { get; set; }

        protected bool Equals(User other)
        {
            return Gender == other.Gender && Equals(Age, other.Age) &&
                   WantsWeatherForecast == other.WantsWeatherForecast &&
                   WantsWaterDrinkNotification == other.WantsWaterDrinkNotification &&
                   Equals(CurrentLocation, other.CurrentLocation) &&
                   FavoritePerformances.SetEquals(other.FavoritePerformances) &&
                   string.Equals(NotificationToken, other.NotificationToken);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Gender;
                hashCode = (hashCode * 397) ^ (Age != null ? Age.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ WantsWeatherForecast.GetHashCode();
                hashCode = (hashCode * 397) ^ WantsWaterDrinkNotification.GetHashCode();
                hashCode = (hashCode * 397) ^ (CurrentLocation != null ? CurrentLocation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FavoritePerformances != null ? FavoritePerformances.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NotificationToken != null ? NotificationToken.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}