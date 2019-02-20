using FluentValidation;
using PaaspopService.Application.Artists.Models;

namespace PaaspopService.Application.Artists.Validators
{
    public class ArtistViewModelValidator : AbstractValidator<ArtistViewModel>
    {
        public ArtistViewModelValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
            RuleFor(req => req.Name).NotEmpty();
        }
    }
}
