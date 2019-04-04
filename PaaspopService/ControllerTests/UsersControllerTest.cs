using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using Newtonsoft.Json;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Application.Users.Commands.RemoveUser;
using PaaspopService.Application.Users.Commands.UpdateUser;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace ControllerTests
{
    public class UsersControllerTest
    {
        public async Task<HttpResponseMessage> PostUser(CreateUserCommand createUserCommand)
        {
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(createUserCommand), Encoding.UTF8, "application/json");
            return await GeneralControllerTest.Instance.Client.PostAsync("/api/users", stringContent);
        }

        public async Task<HttpResponseMessage> PutUser(UpdateUserCommand updateUserCommand)
        {
            updateUserCommand.Id = ObjectId.GenerateNewId().ToString();
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserCommand), Encoding.UTF8, "application/json");
            return await GeneralControllerTest.Instance.Client.PutAsync("/api/users", stringContent);
        }

        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await GeneralControllerTest.Instance.Client.DeleteAsync("/api/users/" + id);
        }

        [Fact]
        public async Task CreateUser_Correct()
        {
            var response = await PostUser(new CreateUserCommand {Age = 80, Gender = 0});
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateUser_Wrong_Age()
        {
            var response = await PostUser(new CreateUserCommand { Age =-1, Gender = 1 });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_Wrong_Gender()
        {
            var response = await PostUser(new CreateUserCommand { Age = 80, Gender = 3 });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUser_Correct()
        {
            var userCommand = new UpdateUserCommand
            {
                Age = new Age(80),
                Gender = 1,
                CurrentLocation = new LocationCoordinate(6.7, 90.3),
                FavoritePerformances = new HashSet<string>(),
                WantsWaterDrinkNotification = true,
                WantsWeatherForecast = false
            };

            var response = await PutUser(userCommand);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void UpdateUser_Wrong_Age()
        {
            var userCommand = new UpdateUserCommand();

            Assert.Throws<AgeInvalidException>(() => userCommand = new UpdateUserCommand
            {
                Age = new Age(150),
                Gender = 1,
                CurrentLocation = new LocationCoordinate(6.7, 90.3),
                FavoritePerformances = new HashSet<string>(),
                WantsWaterDrinkNotification = true,
                WantsWeatherForecast = false
            });
        }

        [Fact]
        public async Task UpdateUser_Wrong_Gender()
        {
            var userCommand = new UpdateUserCommand
            {
                Age = new Age(80),
                Gender = 3,
                CurrentLocation = new LocationCoordinate(6.7, 90.3),
                FavoritePerformances = new HashSet<string>(),
                WantsWaterDrinkNotification = true,
                WantsWeatherForecast = false
            };

            var response = await PutUser(userCommand);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UpdateUser_Wrong_Location_Lat()
        {
            var userCommand = new UpdateUserCommand();

            Assert.Throws<LocationCoordinateInvalidException>(() => userCommand = new UpdateUserCommand
            {
                Age = new Age(80),
                Gender = 1,
                CurrentLocation = new LocationCoordinate(688.4, 90.3),
                FavoritePerformances = new HashSet<string>(),
                WantsWaterDrinkNotification = true,
                WantsWeatherForecast = false
            });
        }

        [Fact]
        public void UpdateUser_Wrong_Location_Lon()
        {
            var userCommand = new UpdateUserCommand();

            Assert.Throws<LocationCoordinateInvalidException>(() => userCommand = new UpdateUserCommand
            {
                Age = new Age(80),
                Gender = 1,
                CurrentLocation = new LocationCoordinate(6.7, -1000.3),
                FavoritePerformances = new HashSet<string>(),
                WantsWaterDrinkNotification = true,
                WantsWeatherForecast = false
            });
        }

        [Fact]
        public async Task RemoveUser_Correct()
        {
            var response = await DeleteUser("123456789012345678901234");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task RemoveUser_Wrong_Id()
        {
            var response = await DeleteUser("1234567890123456789012345");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}