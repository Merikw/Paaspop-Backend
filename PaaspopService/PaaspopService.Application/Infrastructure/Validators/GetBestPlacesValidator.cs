using FluentValidation;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GetBestPlacesValidator : AbstractValidator<GetBestPlacesQuery>
    {
        public GetBestPlacesValidator()
        {
            RuleFor(req => req.UserLocationCoordinate.Longitude).InclusiveBetween(-180, 180);
            RuleFor(req => req.UserLocationCoordinate.Latitude).InclusiveBetween(-90, 90);
        }
    }
}