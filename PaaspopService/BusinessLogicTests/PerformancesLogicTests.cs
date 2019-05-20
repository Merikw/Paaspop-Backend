using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using MongoDB.Bson;
using Moq;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Commands.UpdatePerformance;
using PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Application.Performances.Queries.GetPerformancesById;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace BusinessLogicTests
{
    public class PerformancesLogicTests
    {
        [Fact]
        public async void Get_favorite_performances_from_user()
        {
            var userId = ObjectId.GenerateNewId(DateTime.Now).ToString();
            var performanceId = ObjectId.GenerateNewId(DateTime.Now).ToString();

            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = userId,
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4),
                FavoritePerformances = new HashSet<string> {performanceId}
            };

            var performance = new Performance
            {
                Id = performanceId,
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
                UsersFavoritedPerformance = new HashSet<string> {userId}
            };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPerformanceByIdQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(performance));

            var mapper = new Mock<IMapper>();

            var userRepository = new Mock<IUsersRepository>();
            userRepository.Setup(repo => repo.GetUserByIdAsync(It.Is<string>(id => id == user.Id)))
                .Returns(Task.FromResult(user));

            var getFavoritePerformnacesFromUserQuery = new GetFavoritePerformancesFromUserQuery {UserId = userId};
            var getFavoritePerformnacesFromUserHandler =
                new GetFavoritePerformancesFromUserHandler(mapper.Object, userRepository.Object, mediator.Object);

            var result = await getFavoritePerformnacesFromUserHandler.Handle(getFavoritePerformnacesFromUserQuery,
                default(CancellationToken));

            result[0].Should().Be(performance);
        }

        [Fact]
        public async void Get_performances()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage()
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<PerformanceViewModel>(It.IsAny<List<Performance>>())).Returns(
                new PerformanceViewModel
                {
                    Performances = new Dictionary<string, List<Performance>>
                    {
                        {"Apollo", new List<Performance> {performance}}
                    },
                    SuggestionPerformances = new List<Performance>()
                });
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetFavoritePerformancesFromUserQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new List<Performance> {performance}));

            var performanceRepository = new Mock<IPerformancesRepository>();
            performanceRepository.Setup(repo => repo.GetPerformances())
                .Returns(Task.FromResult(new List<Performance> {performance}));

            var getPerformancesQuery = new GetPerformancesQuery
                {UserId = ObjectId.GenerateNewId(DateTime.Now).ToString()};
            var getPerformancesHandler =
                new GetPerformancesQueryHandler(mapper.Object, performanceRepository.Object, mediator.Object);
            var performanceViewModel =
                await getPerformancesHandler.Handle(getPerformancesQuery, default(CancellationToken));

            performanceViewModel.Performances.TryGetValue("Apollo", out var performances);

            performances?[0].Should().Be(performance);
        }

        [Fact]
        public async void Get_performances_by_id()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage()
            };

            var mapper = new Mock<IMapper>();
            var mediator = new Mock<IMediator>();

            var performanceRepository = new Mock<IPerformancesRepository>();
            performanceRepository.Setup(repo => repo.GetPerformanceById(performance.Id))
                .Returns(Task.FromResult(performance));

            var getPerformanceByIdQuery = new GetPerformanceByIdQuery {Id = performance.Id};
            var getPerformanceByIdHandler =
                new GetPerformanceByIdHandler(mapper.Object, performanceRepository.Object, mediator.Object);

            var result = await getPerformanceByIdHandler.Handle(getPerformanceByIdQuery, default(CancellationToken));

            result.Should().Be(performance);
        }

        [Fact]
        public async void Update_performance()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
                UsersFavoritedPerformance = new HashSet<string>()
            };

            var mediator = new Mock<IMediator>();
            var mapper = new Mock<IMapper>();

            var updatePerformanceCommand = new UpdatePerformanceCommand
            {
                performanceToBeUpdated = performance
            };

            var performanceRepository = new Mock<IPerformancesRepository>();

            var updatePerformanceHandler =
                new UpdatePerformanceHandler(mapper.Object, performanceRepository.Object, mediator.Object);
            await updatePerformanceHandler.Handle(updatePerformanceCommand, default(CancellationToken));

            performanceRepository.Verify(mock =>
                mock.UpdatePerformance(It.Is<Performance>(performanceParam => Equals(performance, performanceParam))));
        }
    }
}