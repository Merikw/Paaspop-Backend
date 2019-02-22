using System.Collections.Generic;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class Age : ValueObject
    {
        public int AbsoluteAge;

        private Age()
        {
        }

        public Age(int age)
        {
            CheckAge(age);
        }

        public Age(string ageText)
        {
            if (int.TryParse(ageText, out var age))
                CheckAge(age);

            else
                throw new AgeInvalidException(ageText);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AbsoluteAge;
        }

        private void CheckAge(int age)
        {
            if (BetweenHandler.IsInBetween(age, 0, 120))
                AbsoluteAge = age;

            else
                throw new AgeInvalidException(age);
        }
    }
}