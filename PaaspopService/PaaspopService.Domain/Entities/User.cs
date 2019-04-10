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
    }
}