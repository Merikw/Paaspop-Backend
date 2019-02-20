using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Domain.Exceptions
{
    public class AgeInvalidException : Exception
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
