using FluentValidation;
using PaaspopService.Application.Users.Commands.UpdateUser;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(req => req.Id).NotEmpty().Length(24);
            RuleFor(req => req.Gender).InclusiveBetween(0, 2);
            RuleFor(req => req.Age.AbsoluteAge).InclusiveBetween(0, 120);
            RuleFor(req => req.CurrentLocation.Longitude).InclusiveBetween(-180, 180);
            RuleFor(req => req.CurrentLocation.Latitude).InclusiveBetween(-90, 90);
            RuleFor(req => req.WantsWaterDrinkNotification).NotNull();
            RuleFor(req => req.WantsWeatherForecast).NotNull();
        }
    }
}
