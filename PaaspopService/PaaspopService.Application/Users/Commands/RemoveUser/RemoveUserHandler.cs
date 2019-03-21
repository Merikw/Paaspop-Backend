using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Commands.UpdatePerformance;
using PaaspopService.Application.Places.Commands.UpdatePlace;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Domain.Enumerations;

namespace PaaspopService.Application.Users.Commands.RemoveUser
{
    public class RemoveUserHandler : GeneralRequestHandler<RemoveUserCommand, Unit>
    {
        private IUsersRepository _usersRepository;

        public RemoveUserHandler(IMapper mapper, IUsersRepository usersRepository, IMediator mediator) : base(mapper, mediator)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUserByIdAsync(request.UserId);
            var userCount = await _usersRepository.GetUsersCountAsync();
            foreach (var performance in user.FavoritePerformances)
            {
                performance.InterestPercentage = performance.CalculateInterestPercentage((int) userCount, 1, Operator.Minus);
                await Mediator.Send(new UpdatePerformanceCommand {performanceToBeUpdated = performance}, cancellationToken);
            }

            var places = await Mediator.Send(new GetPlacesQuery(), cancellationToken);
            foreach (var place in places)
            {
                if (!place.UsersOnPlace.Contains(user.Id)) continue;
                place.UsersOnPlace.Remove(user.Id);
                place.CrowdPercentage =
                    place.CalculateCrowdPercentage(Convert.ToInt32(userCount), 1, Operator.Minus);
                await Mediator.Send(new UpdatePlaceCommand {PlaceToBeUpdated = place}, cancellationToken);
            }
            await _usersRepository.RemoveUserAsync(request.UserId);
            return Unit.Value;
        }
    }
}
