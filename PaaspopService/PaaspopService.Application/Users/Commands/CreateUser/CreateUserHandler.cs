using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Commands.UpdatePerformance;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Application.Places.Commands.UpdatePlace;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;

namespace PaaspopService.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : GeneralRequestHandler<CreateUserCommand, User>
    {
        private readonly IMediator _mediator;
        private readonly IUsersRepository _usersRepository;

        public CreateUserHandler(IMapper mapper, IUsersRepository usersRepository, IMediator mediator) : base(mapper,
            mediator)
        {
            _usersRepository = usersRepository;
            _mediator = mediator;
        }

        public override async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToBeCreated = Mapper.Map<User>(request);
            await _usersRepository.CreateUserAsync(userToBeCreated);

            var performances =
                await _mediator.Send(new GetPerformancesQuery {UserId = userToBeCreated.Id}, cancellationToken);
            var places = await _mediator.Send(new GetPlacesQuery(), cancellationToken);
            var userCount = await _usersRepository.GetUsersCountAsync();

            foreach (var performanceLists in performances.Performances.Values)
                performanceLists.ForEach(performance =>
                    UpdatePerformance(performance, (int) userCount).GetAwaiter().GetResult());

            places.ForEach(place => UpdatePlace(place, (int) userCount).GetAwaiter().GetResult());

            return userToBeCreated;
        }

        private async Task UpdatePerformance(Performance performance, int userCount)
        {
            performance.InterestPercentage = performance.CalculateInterestPercentage(userCount, 0, Operator.None);
            await _mediator.Send(new UpdatePerformanceCommand {performanceToBeUpdated = performance});
        }

        private async Task UpdatePlace(Place place, int userCount)
        {
            place.CrowdPercentage = place.CalculateCrowdPercentage(userCount, 0, Operator.None);
            await _mediator.Send(new UpdatePlaceCommand {PlaceToBeUpdated = place});
        }
    }
}