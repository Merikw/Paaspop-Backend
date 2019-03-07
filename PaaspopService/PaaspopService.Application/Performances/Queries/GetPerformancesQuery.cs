using MediatR;

namespace PaaspopService.Application.Performances.Queries
{
    public class GetPerformancesQuery : IRequest<PerformanceViewModel>
    {
    }
}
