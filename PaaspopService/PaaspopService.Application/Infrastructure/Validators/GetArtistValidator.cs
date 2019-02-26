using FluentValidation;
using PaaspopService.Application.Artists.Queries;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GetArtistValidator : AbstractValidator<GetArtistQuery>
    {
        public GetArtistValidator()
        {
            RuleFor(req => req.Id).NotEmpty().Length(24);
        }
    }
}