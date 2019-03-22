using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ControllerTests
{
    public class PerformancesControllerTest
    {
        [Fact]
        public async Task GetPerformances_Correct()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/performances/123456789012345678901234");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPerformances_Wrong_userid()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/performances/1234567890123456789012345");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}