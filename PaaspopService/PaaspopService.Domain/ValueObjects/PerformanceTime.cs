using System.Collections.Generic;
using System.Text.RegularExpressions;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class PerformanceTime : ValueObject
    {
        public int Day;
        public string EndTime;
        public string PerformanceTimeText;
        public string StartTime;

        private PerformanceTime()
        {
        }

        public PerformanceTime(int day, string startTime, string endTime)
        {
            PerformanceTimeText = day + " (" + startTime + " - " + endTime + " )";
            if (CheckDay(day) && CheckTime(startTime, endTime))
            {
                Day = day;
                StartTime = startTime;
                EndTime = endTime;
            }
            else
            {
                throw new InvalidPerformanceTimeException(PerformanceTimeText);
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Day;
            yield return StartTime;
            yield return EndTime;
        }

        private bool CheckDay(int day)
        {
            return BetweenHandler.IsInBetween(day, 1, 7);
        }

        private bool CheckTime(string startTime, string endTime)
        {
            return Regex.IsMatch(startTime, "[0-9]{2,2}|:|[0-9]{2,2}") &&
                   Regex.IsMatch(endTime, "[0-9]{2,2}|:|[0-9]{2,2}");
        }
    }
}