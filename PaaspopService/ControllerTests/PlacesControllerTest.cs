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
            var response = await GeneralControllerTest.Instance.Client.GetAsync("/api/places/best");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
