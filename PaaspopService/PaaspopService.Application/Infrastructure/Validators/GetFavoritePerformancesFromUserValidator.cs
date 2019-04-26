using FluentValidation;
using PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GetFavoritePerformancesFromUserValidator : AbstractValidator<GetFavoritePerformancesFromUserQuery>
    {
        public GetFavoritePerformancesFromUserValidator()
        {
            RuleFor(req => req.UserId).NotEmpty().Length(24);
        }
    }
}
