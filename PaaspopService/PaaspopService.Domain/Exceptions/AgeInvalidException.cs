using System;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Domain.Exceptions
{
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
    }
}