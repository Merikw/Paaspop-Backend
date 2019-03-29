using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PaaspopService.Application.Infrastructure.PushNotifications;

namespace PaaspopService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().ScheduleArtistStartNotifications().GetAwaiter().GetResult().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}