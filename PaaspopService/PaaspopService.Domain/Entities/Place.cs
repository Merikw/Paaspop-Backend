using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Entities
{
    public class Place : Model
    {
        public string Name { get; set; }
        public PlaceType Type { get; set; }
        public Percentage CrowdPercentage { get; set; }
        public LocationCoordinate Location { get; set; }
    }
}
