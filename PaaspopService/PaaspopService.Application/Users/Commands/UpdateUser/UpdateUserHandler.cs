﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Enums;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Commands.UpdatePerformance;
using PaaspopService.Application.Performances.Queries.GetPerformances;
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
            var userToBeUpdated = Mapper.Map<User>(request);
            if (request.UserUpdateType == UserUpdateType.Performance)
            {
                var newFavorites = await UpdatePerformancesFromUser(userToBeUpdated);
                userToBeUpdated.FavoritePerformances = newFavorites;
            } else if (request.UserUpdateType == UserUpdateType.Location)
            {
                await UpdatePlacesFromLocationOfUser(userToBeUpdated);
            }

            await _usersRepository.UpdateUserAsync(userToBeUpdated);
            return userToBeUpdated;
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

        private async Task<ISet<Performance>> UpdatePerformancesFromUser(User user)
        {
            var performances = await Mediator.Send(new GetPerformancesQuery {UserId = user.Id});
            var toBeAddedFavorites = new List<Performance>();
            var toBeRemovedFavorites = new List<Performance>();
            var newListOfFavorites = user.FavoritePerformances;
            foreach (var performancesValue in performances.Performances.Values)
            {
                toBeRemovedFavorites.AddRange(
                    performancesValue.Where(p => p.UsersFavoritedPerformance.Contains(user.Id) 
                                           && !user.FavoritePerformances.Any(up => up.Id == p.Id))
                );

                toBeAddedFavorites.AddRange(
                    performancesValue.Where(p => !p.UsersFavoritedPerformance.Contains(user.Id)
                                                 && user.FavoritePerformances.Any(up => up.Id == p.Id))
                    );
            }

            var userCount = await _usersRepository.GetUsersCountAsync();
            foreach (var performance in toBeAddedFavorites)
            {
                performance.InterestPercentage = performance.CalculateInterestPercentage((int)userCount, 1, Operator.Plus);
                performance.UsersFavoritedPerformance.Add(user.Id);
                await Mediator.Send(new UpdatePerformanceCommand { performanceToBeUpdated = performance });
                newListOfFavorites.Remove(newListOfFavorites.FirstOrDefault(p => p.Id == performance.Id));
                newListOfFavorites.Add(performance);
            }

            foreach (var performance in toBeRemovedFavorites)
            {
                performance.InterestPercentage = performance.CalculateInterestPercentage((int)userCount, 1, Operator.Minus);
                performance.UsersFavoritedPerformance.Remove(user.Id);
                await Mediator.Send(new UpdatePerformanceCommand { performanceToBeUpdated = performance });
                newListOfFavorites.Remove(newListOfFavorites.FirstOrDefault(p => p.Id == performance.Id));
            }

            return newListOfFavorites;
        }
    }
}