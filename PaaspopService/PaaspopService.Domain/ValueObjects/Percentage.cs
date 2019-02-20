using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.ValueObjects
{
    public class Percentage : ValueObject
    {
        public int AbsolutePercentage { get; private set; }

        private Percentage() { }

        public static Percentage Create(int partOftotal, int total)
        {
            return new Percentage
            {
                AbsolutePercentage = (partOftotal / total) * 100
            };
        }

        public static Percentage Create(int total)
        {
            if(BetweenHandler.IsInBetween(total, 0, 100))
            {
                return new Percentage
                {
                    AbsolutePercentage = total
                };
            }

            throw new PercentageInvalidException(total);
        }

        public static implicit operator string(Percentage percentage)
        {
            return percentage.ToString();
        }

        public static explicit operator Percentage(int total)
        {
            return Create(total);
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
