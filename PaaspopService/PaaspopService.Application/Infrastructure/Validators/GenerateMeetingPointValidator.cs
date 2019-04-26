using FluentValidation;
using PaaspopService.Application.Places.Queries.GetMeetingPointQuery;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GenerateMeetingPointValidator : AbstractValidator<GetMeetingPointQuery>
    {
        public GenerateMeetingPointValidator()
        {
            RuleFor(req => req.LocationOfUser.Longitude).InclusiveBetween(-180, 180);
            RuleFor(req => req.LocationOfUser.Latitude).InclusiveBetween(-90, 90);
        }
    }
}
