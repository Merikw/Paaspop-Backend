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
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/performances");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}