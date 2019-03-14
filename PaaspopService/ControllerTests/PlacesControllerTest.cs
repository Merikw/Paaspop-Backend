using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ControllerTests
{
    public class PlacesControllerTest
    {
        [Fact]
        public async Task GetBestPlaces_Correct()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/places/best/7.5/55.3");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBestPlaces_Wrong()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/places/best/600.13/55.3");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
