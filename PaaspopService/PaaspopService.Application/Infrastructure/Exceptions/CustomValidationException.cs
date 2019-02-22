using System;
using FluentValidation.Results;

namespace PaaspopService.Application.Infrastructure.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(ValidationFailure failure)
            : base($"Validation failure has occurred ${failure.ErrorMessage}")
        {
        }
    }
}
