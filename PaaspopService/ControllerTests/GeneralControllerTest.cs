using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PaaspopService.WebApi;

namespace ControllerTests
{
    public class GeneralControllerTest
    {
        private static GeneralControllerTest instance = null;
        public HttpClient Client { get; }

        private GeneralControllerTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static GeneralControllerTest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeneralControllerTest();
                }
                return instance;
            }
        }
    }
}
