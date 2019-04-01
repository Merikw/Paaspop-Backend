using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;
using Quartz.Impl;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Water
{
    public static class WaterDrinkNotification
    {
        public static async Task<IWebHost> ScheduleWaterDrinkNotification(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var usersRepository = services.GetRequiredService<IUsersRepository>();

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                var job = JobBuilder.Create<WaterDrinkJob>()
                    .WithIdentity("job" + "waterdrink", "waterdrink")
                    .Build();

                job.JobDataMap.Put("usersRepository", usersRepository);

                var cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                    .WithIdentity("trigger" + "waterdrink", "waterdrink")
                    .WithCronSchedule("0 * 0/2 ? * * *")
                    .ForJob("job" + "waterdrink", "waterdrink")
                    .Build();

                await scheduler.ScheduleJob(job, cronTrigger);
            }

            return webHost;
        }
    }
}
