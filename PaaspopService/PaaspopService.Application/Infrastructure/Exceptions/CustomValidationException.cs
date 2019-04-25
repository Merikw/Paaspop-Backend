using PaaspopService.Common.Middleware;

namespace PaaspopService.Application.Infrastructure.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(string failure)
            : base($"Validation failure has occurred ${failure}")
        {
        }
    }
}