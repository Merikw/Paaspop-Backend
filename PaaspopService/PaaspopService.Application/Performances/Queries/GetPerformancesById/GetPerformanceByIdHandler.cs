using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetPerformancesById
{
    public class GetPerformanceByIdHandler : GeneralRequestHandler<GetPerformanceByIdQuery, Performance>
    {
        private readonly IPerformancesRepository _performancesRepository;

        public GetPerformanceByIdHandler(IMapper mapper, IPerformancesRepository performanceRepository, IMediator mediator)
            : base(mapper, mediator)
        {
            _performancesRepository = performanceRepository;
        }

        public override async Task<Performance> Handle(GetPerformanceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _performancesRepository.GetPerformanceById(request.Id);
        }
    }
}
