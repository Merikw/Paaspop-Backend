using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Commands.UpdatePerformance
{
    public class UpdatePerformanceHandler : GeneralRequestHandler<UpdatePerformanceCommand, Performance>
    {
        private readonly IPerformancesRepository _performancesRepository;

        public UpdatePerformanceHandler(IMapper mapper, IPerformancesRepository performancesRepository,
            IMediator mediator = null) : base(mapper, mediator)
        {
            _performancesRepository = performancesRepository;
        }

        public override async Task<Performance> Handle(UpdatePerformanceCommand request,
            CancellationToken cancellationToken)
        {
            return await _performancesRepository.UpdatePerformance(request.performanceToBeUpdated);
        }
    }
}