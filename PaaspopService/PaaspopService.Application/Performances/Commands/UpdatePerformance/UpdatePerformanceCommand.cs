using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Commands.UpdatePerformance
{
    public class UpdatePerformanceCommand : IRequest<Performance>
    {
        public Performance performanceToBeUpdated { get; set; }
    }
}
