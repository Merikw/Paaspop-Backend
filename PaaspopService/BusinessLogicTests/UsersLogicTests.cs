using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Bson;
using Moq;
using PaaspopService.Application.Infrastructure.Enums;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Application.Users.Commands.RemoveUser;
using PaaspopService.Application.Users.Commands.UpdateUser;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace BusinessLogicTests
{
    public class UsersLogicTests
    {
        [Fact]
        public async void Create_user()
        {
            var userToBeCreated = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123"
            };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPerformancesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new PerformanceViewModel
                { Performances = new Dictionary<string, List<Performance>>() }));
            mediator.Setup(m => m.Send(It.IsAny<GetPlacesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new List<Place>()));

            var createUserCommand = new CreateUserCommand
            {
                Age = userToBeCreated.Age.AbsoluteAge,
                Gender = (int)userToBeCreated.Gender,
                NotificationToken = userToBeCreated.NotificationToken
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User>(createUserCommand)).Returns(userToBeCreated);

            var userRepository = new Mock<IUsersRepository>();

            var createUserHandler = new CreateUserHandler(mapper.Object, userRepository.Object, mediator.Object);
            await createUserHandler.Handle(createUserCommand, default(CancellationToken));

            userRepository.Verify(mock => mock.CreateUserAsync(It.Is<User>(user => user.Id == userToBeCreated.Id)));
        }

        [Fact]
        public async void Remove_user()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123"
            };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPerformancesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new PerformanceViewModel
                { Performances = new Dictionary<string, List<Performance>>() }));
            mediator.Setup(m => m.Send(It.IsAny<GetPlacesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new List<Place>()));

            var mapper = new Mock<IMapper>();

            var usersRepositoryMongoDb = new Mock<IUsersRepository>();
            usersRepositoryMongoDb.Setup(repo => repo.GetUserByIdAsync(It.Is<string>(id => id == user.Id)))
                .Returns(Task.FromResult(user));

            var removeUserCommand = new RemoveUserCommand { UserId = user.Id };

            var removeUserHandler = new RemoveUserHandler(mapper.Object, usersRepositoryMongoDb.Object, mediator.Object);
            await removeUserHandler.Handle(removeUserCommand, default(CancellationToken));

            usersRepositoryMongoDb.Verify(mock => mock.RemoveUserAsync(It.Is<string>(id => id == user.Id)));
        }

        [Fact]
        public async void Update_user()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123"
            };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPlacesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(new List<Place>()));

            var updateUserCommand = new UpdateUserCommand
            {
                Age = user.Age,
                CurrentLocation = new LocationCoordinate(4.4, 4.4),
                FavoritePerformances = new HashSet<string>(),
                Id = user.Id,
                Gender = (int)user.Gender,
                WantsWeatherForecast = user.WantsWeatherForecast,
                WantsWaterDrinkNotification = user.WantsWaterDrinkNotification,
                UserUpdateType = UserUpdateType.Location
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User>(updateUserCommand)).Returns(user);

            var usersRepositoryMongoDb = new Mock<IUsersRepository>();
            usersRepositoryMongoDb.Setup(repo => repo.GetUsersCountAsync()).Returns(Task.FromResult((long) 1));

            var updateUserHandler = new UpdateUserHandler(mapper.Object, mediator.Object, usersRepositoryMongoDb.Object);
            await updateUserHandler.Handle(updateUserCommand, default(CancellationToken));

            usersRepositoryMongoDb.Verify(mock => mock.UpdateUserAsync(It.Is<User>(userParam => Equals(userParam, user))));
        }
    }
}
