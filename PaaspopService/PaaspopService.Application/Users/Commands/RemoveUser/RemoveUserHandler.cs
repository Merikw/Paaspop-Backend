using System.Linq;
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

namespace PaaspopService.Application.Users.Commands.RemoveUser
{
    public class RemoveUserHandler : GeneralRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IUsersRepository _usersRepository;

        public RemoveUserHandler(IMapper mapper, IUsersRepository usersRepository, IMediator mediator) : base(mapper,
            mediator)
        {
            _usersRepository = usersRepository;
            _mediator = mediator;
        }

        public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUserByIdAsync(request.UserId);
            if(user == null) return Unit.Value;
            var performances = await _mediator.Send(new GetPerformancesQuery {UserId = user.Id}, cancellationToken);
            var places = await _mediator.Send(new GetPlacesQuery(), cancellationToken);
            await _usersRepository.RemoveUserAsync(request.UserId);
            var userCount = await _usersRepository.GetUsersCountAsync();

            foreach (var performanceLists in performances.Performances.Values)
                performanceLists.ForEach(performance =>
                    UpdatePerformance(performance, (int) userCount, user).GetAwaiter().GetResult());

            places.ForEach(place => UpdatePlace(place, (int) userCount, user).GetAwaiter().GetResult());
            return Unit.Value;
        }

        private async Task UpdatePerformance(Performance performance, int userCount, User user)
        {
            if (user.FavoritePerformances.Contains(performance.Id))
            {
                performance.InterestPercentage = performance.CalculateInterestPercentage(userCount, 1, Operator.Minus);
                performance.UsersFavoritedPerformance.Remove(
                    performance.UsersFavoritedPerformance.FirstOrDefault(userId => userId == user.Id));
            }
            else
            {
                performance.InterestPercentage = performance.CalculateInterestPercentage(userCount, 0, Operator.None);
            }

            await Mediator.Send(new UpdatePerformanceCommand {performanceToBeUpdated = performance});
        }

        private async Task UpdatePlace(Place place, int userCount, User user)
        {
            if (!place.UsersOnPlace.Contains(user.Id))
            {
                place.CrowdPercentage =
                    place.CalculateCrowdPercentage(userCount, 0, Operator.None);
            }
            else
            {
                place.CrowdPercentage =
                    place.CalculateCrowdPercentage(userCount, 1, Operator.Minus);
                place.UsersOnPlace.Remove(user.Id);
            }

            await Mediator.Send(new UpdatePlaceCommand {PlaceToBeUpdated = place});
        }
    }
}