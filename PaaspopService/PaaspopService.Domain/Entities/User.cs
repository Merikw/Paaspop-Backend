﻿using System.Collections.Generic;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

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