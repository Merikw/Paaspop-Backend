using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Entities
{
    public class Stage : Model
    {
        public string Name { get; set; }
        public LocationCoordinate Location { get; set; }
    }
}
