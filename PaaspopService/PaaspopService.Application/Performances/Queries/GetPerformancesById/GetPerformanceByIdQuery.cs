using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetPerformancesById
{
    public class GetPerformanceByIdQuery : IRequest<Performance>
    {
        public string Id { get; set; }
    }
}