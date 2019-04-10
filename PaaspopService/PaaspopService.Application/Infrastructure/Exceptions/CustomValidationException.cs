using FluentValidation.Results;
using PaaspopService.Common.Middleware;

namespace PaaspopService.Application.Infrastructure.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(ValidationFailure failure)
            : base($"Validation failure has occurred ${failure.ErrorMessage}")
        {
        }
    }
}