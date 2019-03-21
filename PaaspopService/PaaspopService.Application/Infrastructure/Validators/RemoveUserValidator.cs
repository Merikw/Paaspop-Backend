using FluentValidation;
using PaaspopService.Application.Users.Commands.RemoveUser;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserValidator()
        {
            RuleFor(req => req.UserId).NotEmpty().Length(24);
        }
    }
}
