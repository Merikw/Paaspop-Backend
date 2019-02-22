using System.Collections.Generic;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class Percentage : ValueObject
    {
        public int AbsolutePercentage { get; }

        private Percentage()
        {
        }

        public Percentage(int partOftotal, int total)
        {
            AbsolutePercentage = partOftotal / total * 100;
        }

        public Percentage(int total)
        {
            if (BetweenHandler.IsInBetween(total, 0, 100))
                AbsolutePercentage = total;
            else
                throw new PercentageInvalidException(total);
        }

        public static implicit operator string(Percentage percentage)
        {
            return percentage.ToString();
        }

        public static explicit operator Percentage(int total)
        {
            return new Percentage(total);
        }

        public override string ToString()
        {
            return AbsolutePercentage.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AbsolutePercentage;
        }
    }
}