﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Queries.GetPerformancesById;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser
{
    public class GetFavoritePerformancesFromUserHandler : GeneralRequestHandler<GetFavoritePerformancesFromUserQuery, List<Performance>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetFavoritePerformancesFromUserHandler(IMapper mapper, IUsersRepository usersRepository, IMediator mediator) : base(mapper, mediator)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<List<Performance>> Handle(GetFavoritePerformancesFromUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUserByIdAsync(request.UserId);
            var favoritedPerformances = new List<Performance>();
            foreach (var performanceId in user.FavoritePerformances)
            {
                var performance = await Mediator.Send(new GetPerformanceByIdQuery {Id = performanceId}, cancellationToken);
                favoritedPerformances.Add(performance);
            }
            favoritedPerformances.Sort();
            return favoritedPerformances;
        }
    }
}
