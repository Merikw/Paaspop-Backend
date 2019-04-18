﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;
using Quartz.Impl;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Weather
{
    public static class WeatherNotification
    {
        public static async Task<IWebHost> ScheduleWeatherNotification(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var usersRepository = services.GetRequiredService<IUsersRepository>();

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                var job = JobBuilder.Create<WeatherJob>()
                    .WithIdentity("job" + "weather", "WeatherForecast")
                    .Build();

                job.JobDataMap.Put("usersRepository", usersRepository);

                var cronTrigger = (ICronTrigger) TriggerBuilder.Create()
                    .WithIdentity("trigger" + "weather", "ArtistPlays")
                    .WithCronSchedule("0 0 10,12,14,16,18,20,22 ? * * *")
                    .ForJob("job" + "weather", "WeatherForecast")
                    .Build();

                await scheduler.ScheduleJob(job, cronTrigger);
            }

            return webHost;
        }
    }
}