using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.ValueObjects
{
    public class Age : ValueObject
    {
        public int AbsoluteAge;

        private Age()
        {
        }

        public static Age Create(int age)
        {
            if(BetweenHandler.IsInBetween(age, 0, 120))
            {
                return new Age
                {
                    AbsoluteAge = age
                };
            }

            throw new AgeInvalidException(age);
        }

        public static Age Create(string ageText)
        {

            if (int.TryParse(ageText, out int age))
            {
                Create(age);
            }

            throw new AgeInvalidException(ageText);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AbsoluteAge;
        }
    }
}
