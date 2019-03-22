using FluentValidation;
using PaaspopService.Application.Performances.Queries.GetPerformances;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GetPerformancesValidator : AbstractValidator<GetPerformancesQuery>
    {
        public GetPerformancesValidator()
        {
            RuleFor(req => req.UserId).Length(24);
        }
    }
}
