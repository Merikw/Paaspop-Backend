using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.WebApi;
using Xunit;

namespace ControllerTests
{
    public class UsersControllerTest
    {
        public UsersControllerTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task CreateUser_Correct()
        {
            var input = new CreateUserCommand
            {
                Age = 80,
                Gender = 1
            };

            var stringContent =
                new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateUser_Wrong_Age()
        {
            var input = new CreateUserCommand
            {
                Age = -1,
                Gender = 1
            };

            var stringContent =
                new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_Wrong_Gender()
        {
            var input = new CreateUserCommand
            {
                Age = 80,
                Gender = 3
            };

            var stringContent =
                new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}