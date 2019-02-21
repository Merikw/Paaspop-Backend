using System;

namespace PaaspopService.Common.Handlers
{
    public class BetweenHandler
    {
        private BetweenHandler()
        {
        }

        public static bool IsInBetween<T>(T x, T min, T max) where T : IComparable<T>
        {
            return x.CompareTo(min) > 0 && x.CompareTo(max) < 0;
        }
    }
}