using FluentValidation;
using PaaspopService.Application.Users.Commands.CreateUser;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(req => req.Gender).InclusiveBetween(0, 2);
            RuleFor(req => req.Age).InclusiveBetween(0, 120);
        }
    }
}
