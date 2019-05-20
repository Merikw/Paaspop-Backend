using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PaaspopService.Application.Infrastructure.PushNotifications.Artist;
using PaaspopService.Application.Infrastructure.PushNotifications.Water;
using PaaspopService.Application.Infrastructure.PushNotifications.Weather;
using PaaspopService.Application.Infrastructure.Tasks;

namespace PaaspopService.WebApi
{
    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .ScheduleArtistStartNotifications().GetAwaiter().GetResult()
                .ScheduleWeatherNotification().GetAwaiter().GetResult()
                .ScheduleWaterDrinkNotification().GetAwaiter().GetResult()
                .RemoveCrowdsOnPlace().GetAwaiter().GetResult().Run();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}