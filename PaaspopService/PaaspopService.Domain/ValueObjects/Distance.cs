using System;
using System.Collections.Generic;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class Distance : ValueObject
    {
        public int AbsoluteDistance { get; set; }

        private Distance()
        {
        }

        public Distance(int distance)
        {
            CheckDistance(distance);
        }

        public Distance(double distance)
        {
            CheckDistance(Convert.ToInt32(Math.Ceiling(distance)));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AbsoluteDistance;
        }

        private void CheckDistance(int distance)
        {
            if (distance > 0)
                AbsoluteDistance = distance;

            else
                throw new DistanceInvalidException(distance);
        }
    }
}
