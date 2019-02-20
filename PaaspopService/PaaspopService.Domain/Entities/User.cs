using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Entities
{
    public class User : Model
    {
        public Gender Gender { get; set; }
        public Age Age { get; set; }
        public bool WantsWeatherForecast { get; set; }
        public bool WantsWaterDrinkNotification { get; set; }
        public LocationCoordinate CurrentLocation { get; set; }
        public ISet<Performance> FavoritePerformances { get; set; }
    }
}
