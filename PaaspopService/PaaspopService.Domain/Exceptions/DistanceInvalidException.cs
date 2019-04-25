using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class DistanceInvalidException : CustomException
    {
        public DistanceInvalidException(int distance)
            : base($"Distance \"{distance}\" is invalid.")
        {
        }

        protected DistanceInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
