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

        [Fact]
        public async Task GenerateMeetingPoint_Correct()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/places/generateMeetingPoint/7.5/55.3");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GenerateMeetingPoint_Wrong_Coordinates()
        {
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/places/generateMeetingPoint/777.5/555.3");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
