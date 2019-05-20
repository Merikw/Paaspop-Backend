using System;
using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace DatabaseIntegrationTests
{
    public class UsersDatabaseTests : MockMongoDatabase
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

            await UsersRepositoryMongoDb.CreateUserAsync(userToBeCreated);

            var result = await UsersRepositoryMongoDb.GetUserByIdAsync(userToBeCreated.Id);
            result.Should().Be(userToBeCreated);
        }

        [Fact]
        public async void Get_user_by_id()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4)
            };

            await UsersRepositoryMongoDb.CreateUserAsync(user);

            var result = await UsersRepositoryMongoDb.GetUserByIdAsync(user.Id);

            result.Should().Be(user);
        }

        [Fact]
        public async void Get_user_count()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4)
            };

            await UsersRepositoryMongoDb.CreateUserAsync(user);

            var result = await UsersRepositoryMongoDb.GetUsersCountAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async void Get_users_by_bool_field()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4),
                WantsWeatherForecast = false
            };

            await UsersRepositoryMongoDb.CreateUserAsync(user);

            var result = await UsersRepositoryMongoDb.GetUsersByBoolField("WantsWeatherForecast", false);
            result[0].Should().Be(user);
        }

        [Fact]
        public async void Get_users_by_favorites()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4),
                FavoritePerformances = new HashSet<string> {"123"}
            };

            await UsersRepositoryMongoDb.CreateUserAsync(user);

            var result = await UsersRepositoryMongoDb.GetUsersByFavorites("123");

            result[0].Should().Be(user);
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

            await UsersRepositoryMongoDb.CreateUserAsync(user);
            var insertedUser = await UsersRepositoryMongoDb.GetUserByIdAsync(user.Id);

            await UsersRepositoryMongoDb.RemoveUserAsync(user.Id);
            var result = await UsersRepositoryMongoDb.GetUserByIdAsync(user.Id);

            result.Should().Be(null);
            insertedUser.Should().Be(insertedUser);
        }

        [Fact]
        public async void Update_user()
        {
            var user = new User
            {
                Age = new Age(20),
                Gender = 0,
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                NotificationToken = "123",
                CurrentLocation = new LocationCoordinate(3.3, 4.4)
            };
            var updatedUser = new User
            {
                Age = user.Age,
                Gender = user.Gender,
                Id = user.Id,
                NotificationToken = user.NotificationToken,
                CurrentLocation = new LocationCoordinate(4.4, 4.4)
            };

            await UsersRepositoryMongoDb.CreateUserAsync(user);
            var insertedUser = await UsersRepositoryMongoDb.GetUserByIdAsync(user.Id);

            await UsersRepositoryMongoDb.UpdateUserAsync(updatedUser);
            var result = await UsersRepositoryMongoDb.GetUserByIdAsync(user.Id);

            insertedUser.Should().Be(user);
            result.Should().Be(updatedUser);
        }
    }
}