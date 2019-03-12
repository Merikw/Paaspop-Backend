using System;
using System.Collections.Generic;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class Percentage : ValueObject
    {
        private Percentage()
        {
        }

        public Percentage(double partOfTotal, double total)
        {
            var percentage = partOfTotal / total * 100;
            if (partOfTotal <= total && Math.Abs(partOfTotal) > 0 && Math.Abs(total) > 0)
            {
                AbsolutePercentage = (int)percentage;
            } 
            else
            {
                throw new PercentageInvalidException((int)percentage);
            }
        }

        public Percentage(int absolutePercentage)
        {
            if (BetweenHandler.IsInBetween(absolutePercentage, 0, 100))
                AbsolutePercentage = absolutePercentage;
            else
                throw new PercentageInvalidException(absolutePercentage);
        }

        public int AbsolutePercentage;

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