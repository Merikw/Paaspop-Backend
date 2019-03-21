using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Places.Commands.UpdatePlace;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : GeneralRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUsersRepository _usersRepository;

        public UpdateUserHandler(IMapper mapper, IMediator mediator, IUsersRepository usersRepository) : base(mapper, mediator)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToBeUpdates = Mapper.Map<User>(request);
            await _usersRepository.UpdateUserAsync(userToBeUpdates);
            await UpdatePlacesFromLocationOfUser(userToBeUpdates);
            return userToBeUpdates;
        }

        private async Task UpdatePlacesFromLocationOfUser(User user)
        {
            var places = await Mediator.Send(new GetPlacesQuery());
            var userCount = await _usersRepository.GetUsersCountAsync();
            foreach (var place in places)
            {
                if (place.GetDistanceFrom(user.CurrentLocation).AbsoluteDistance >= 10 || place.UsersOnPlace.Contains(user.Id))
                {
                    place.UsersOnPlace.Remove(user.Id);
                    continue;
                }

                place.UsersOnPlace.Add(user.Id);
                place.CrowdPercentage = place.CalculateCrowdPercentage(Convert.ToInt32(userCount), 1, Operator.Plus);
                await Mediator.Send(new UpdatePlaceCommand { PlaceToBeUpdated = place});
            }
        }
    }
}