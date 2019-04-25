using System;
using System.Runtime.Serialization;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
    [Serializable]
    public class AgeInvalidException : CustomException
    {
        public AgeInvalidException(int age)
            : base($"Age \"{age}\" is invalid.")
        {
        }

        public AgeInvalidException(string age)
            : base($"Age \"{age}\" is invalid.")
        {
        }

        public AgeInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}