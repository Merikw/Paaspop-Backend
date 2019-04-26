using FluentValidation;
using PaaspopService.Application.Performances.Queries.GetPerformancesById;

namespace PaaspopService.Application.Infrastructure.Validators
{
    public class GetPerformanceByIdValidator : AbstractValidator<GetPerformanceByIdQuery>
    {
        public GetPerformanceByIdValidator()
        {
            RuleFor(req => req.Id).Length(24);
        }
    }
}
