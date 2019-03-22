using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetPerformances
{
    public class GetPerformancesQueryHandler : GeneralRequestHandler<GetPerformancesQuery, PerformanceViewModel>
    {
        private readonly IPerformancesRepository _performancesRepository;

        public GetPerformancesQueryHandler(IMapper mapper, IPerformancesRepository performanceRepository, IMediator mediator)
            : base(mapper, mediator)
        {
            _performancesRepository = performanceRepository;
        }

        public override async Task<PerformanceViewModel> Handle(GetPerformancesQuery request,
            CancellationToken cancellationToken)
        {
            var performances = await _performancesRepository.GetPerformances();
            var favoritePerformancesFromUser = await Mediator.Send(new GetFavoritePerformancesFromUserQuery {UserId = request.UserId}, cancellationToken);
            var mappedResult = Mapper.Map<PerformanceViewModel>(performances);
            foreach (var entry in mappedResult.Performances) entry.Value.Sort();
            if (favoritePerformancesFromUser.Count >= 5)
            {
                mappedResult.SuggestionPerformances =
                    Performance.GetSuggestions(favoritePerformancesFromUser, performances);
            }
            return mappedResult;
        }
    }
}