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
            var response = await GeneralControllerTest.Instance.Client.PostAsync("/api/users", stringContent);

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
            var response = await GeneralControllerTest.Instance.Client.PostAsync("/api/users", stringContent);

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
            var response = await GeneralControllerTest.Instance.Client.PostAsync("/api/users", stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}