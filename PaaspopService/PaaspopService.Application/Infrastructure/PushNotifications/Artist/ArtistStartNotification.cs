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
                    int.TryParse(performance.PerformanceTime.StartTime.Substring(0, 2), out var hour);
                    int.TryParse(performance.PerformanceTime.StartTime.Substring(3, 2), out var minute);
                    minute = minute - 10;
                    if (minute < 0)
                    {
                        hour = hour == 0 ? 23 : hour - 1;
                        minute = 60 + minute;
                    }

                    var job = JobBuilder.Create<ArtistStartsJob>()
                        .WithIdentity("job" + performance.Id, "ArtistPlays")
                        .Build();

                    job.JobDataMap.Put("performance", performance);
                    job.JobDataMap.Put("usersRepository", usersRepository);
                    job.JobDataMap.Put("hour", hour);
                    job.JobDataMap.Put("minute", minute);

                    var simpleTrigger = (ISimpleTrigger) TriggerBuilder.Create()
                        .WithIdentity("trigger" + performance.Id, "ArtistPlays")
                        .StartAt(new DateTimeOffset(2019, 4, performance.PerformanceTime.Day + 14,
                            hour, minute, 0, TimeSpan.Zero))
                        .ForJob("job" + performance.Id, "ArtistPlays")
                        .Build();

                    await scheduler.ScheduleJob(job, simpleTrigger);
                }
            }

            return webHost;
        }
    }
}