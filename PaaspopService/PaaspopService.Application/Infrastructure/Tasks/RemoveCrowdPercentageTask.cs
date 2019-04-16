using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.ValueObjects;
using Quartz;
using Quartz.Impl;

namespace PaaspopService.Application.Infrastructure.Tasks
{
    internal class RemoveCrowdPercentageJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var placesRepository = (IPlacesRepository)context.MergedJobDataMap["placesRepository"];
            var places = await placesRepository.GetPlaces();
            foreach (var place in places)
            {
                place.CrowdPercentage = new Percentage(0);
                place.UsersOnPlace = new HashSet<string>();
                await placesRepository.UpdatePlaceAsync(place);
            }
        }
    }   

    public static class RemoveCrowdPercentageTask
    {
        public static async Task<IWebHost> RemoveCrowdsOnPlace(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var placesRepository = services.GetRequiredService<IPlacesRepository>();

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                var job = JobBuilder.Create<RemoveCrowdPercentageJob>()
                    .WithIdentity("RemoveCrowdPercentageJob", "RemoveCrowdPercentageJob")
                    .Build();

                job.JobDataMap.Put("placesRepository", placesRepository);

                var cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                    .WithIdentity("trigger" + "removecrowdpercentagejob", "RemoveCrowdPercentageJob")
                    .WithCronSchedule("0 */2 * ? * *")
                    .ForJob("RemoveCrowdPercentageJob", "RemoveCrowdPercentageJob")
                    .Build();

                await scheduler.ScheduleJob(job, cronTrigger);
            }

            return webHost;
        }
    }
}
