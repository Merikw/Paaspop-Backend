using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;
using Quartz.Impl;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Artist
{
    public static class ArtistStartNotification
    {
        public static async Task<IWebHost> ScheduleArtistStartNotifications(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var performancesRepository = services.GetRequiredService<IPerformancesRepository>();
                var usersRepository = services.GetRequiredService<IUsersRepository>();

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                var performances = await performancesRepository.GetPerformances();
                foreach (var performance in performances)
                {
                    if (performance.PerformanceTime == null) continue;
                    if (performance.PerformanceTime.Day != 5 || performance.PerformanceTime.Day != 6 || performance.PerformanceTime.Day != 7) continue;
                    int.TryParse(performance.PerformanceTime.StartTime.Substring(0, 2), out var hour);
                    int.TryParse(performance.PerformanceTime.StartTime.Substring(3, 2), out var minute);
                    minute = minute - 10;
                    if (minute < 0)
                    {
                        hour = hour == 0 ? 23 : hour - 1;
                        minute = 60 + minute;
                    }

                    if(hour - 2 <= 0)
                    {
                        hour = 24 + (hour - 2);
                    }
                    else
                    {
                        hour = hour - 2;
                    }

                    var job = JobBuilder.Create<ArtistStartsJob>()
                        .WithIdentity("job" + performance.Id, "ArtistPlays")
                        .Build();

                    job.JobDataMap.Put("performance", performance);
                    job.JobDataMap.Put("usersRepository", usersRepository);
                    job.JobDataMap.Put("hour", hour);
                    job.JobDataMap.Put("minute", minute);

                    var cronString = "0 " + minute + " " + hour + " " + (14 + performance.PerformanceTime.Day) +
                                     " APR ? 2019";

                    var simpleTrigger = (ICronTrigger) TriggerBuilder.Create()
                        .WithIdentity("trigger" + performance.Id, "ArtistPlays")
                        .WithCronSchedule(cronString)
                        .ForJob("job" + performance.Id, "ArtistPlays")
                        .Build();

                    await scheduler.ScheduleJob(job, simpleTrigger);
                }
            }

            return webHost;
        }
    }
}