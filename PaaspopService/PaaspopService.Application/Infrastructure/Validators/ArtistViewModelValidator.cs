using FluentValidation;
using PaaspopService.Application.Artists.Queries;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class ArtistViewModelValidator : AbstractValidator<GetArtistQuery>
    {
        public ArtistViewModelValidator()
        {
            RuleFor(req => req.Id).NotEmpty().Length(24);
        }
    }
}