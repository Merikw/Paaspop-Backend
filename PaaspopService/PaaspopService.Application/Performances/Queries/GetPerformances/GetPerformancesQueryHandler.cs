using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;

namespace PaaspopService.Application.Performances.Queries.GetPerformances
{
    public class GetPerformancesQueryHandler : GeneralRequestHandler<GetPerformancesQuery, PerformanceViewModel>
    {
        private readonly IPerformancesRepository _performancesRepository;

        public GetPerformancesQueryHandler(IMapper mapper, IPerformancesRepository performanceRepository)
            : base(mapper)
        {
            _performancesRepository = performanceRepository;
        }

        public override async Task<PerformanceViewModel> Handle(GetPerformancesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _performancesRepository.GetPerformances();
            var mappedResult = Mapper.Map<PerformanceViewModel>(result);
            foreach (var entry in mappedResult.Performances) entry.Value.Sort();
            return mappedResult;
        }
    }
}