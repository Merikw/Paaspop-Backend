using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Entities
{
    public class Performance : Model
    {
        public DateTime Time { get; set; }
        public Percentage InterestPercentage { get; set; }
        public Stage Stage { get; set; }
        public Artist Artist { get; set; }
    }
}
