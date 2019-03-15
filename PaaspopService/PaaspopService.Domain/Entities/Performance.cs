using System;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class Performance : Model, IComparable<Performance>
    {
        public PerformanceTime PerformanceTime { get; set; }
        public Percentage InterestPercentage { get; set; }
        public Stage Stage { get; set; }
        public Artist Artist { get; set; }

        public int CompareTo(Performance other)
        {
            if (PerformanceTime.Day > other.PerformanceTime.Day &&
                string.Compare(PerformanceTime.StartTime, other.PerformanceTime.StartTime, StringComparison.Ordinal) == 1) return 1;
            if (PerformanceTime.Day == other.PerformanceTime.Day &&
                string.Compare(PerformanceTime.StartTime, other.PerformanceTime.StartTime, StringComparison.Ordinal) == 1) return 1;
            if (PerformanceTime.Day == other.PerformanceTime.Day &&
                string.Compare(PerformanceTime.StartTime, other.PerformanceTime.StartTime, StringComparison.Ordinal) == 0) return 0;
            return PerformanceTime.Day < other.PerformanceTime.Day ? -1 : 0;
        }
    }
}
