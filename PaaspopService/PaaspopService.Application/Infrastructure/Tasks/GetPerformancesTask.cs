using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using static System.String;

namespace PaaspopService.Application.Infrastructure.Tasks
{
    public static class GetPerformancesTask
    {
        public static async Task<IWebHost> GetPerformances(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var performancesRepository = services.GetRequiredService<IPerformancesRepository>();

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://www.paaspop.nl/json/timetable");
                    response.EnsureSuccessStatusCode();
                    dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                    dynamic[] days = {jsonObject.timetable.Vrijdag, jsonObject.timetable.Zaterdag, jsonObject.timetable.Zondag};
                    foreach (var day in days)
                    {
                        foreach (var stage in day)
                        {
                            foreach (var performance in stage.acts)
                            {
                                try
                                {
                                    if (performance.id == null) continue;
                                    var dayOfWeek = 0;
                                    var startTime = (string) performance.start;
                                    var endTime = (string) performance.end;
                                    var photo = (string) performance.photo;
                                    switch ((string) performance.day)
                                    {
                                        case "Vrijdag":
                                            dayOfWeek = 5;
                                            break;
                                        case "Zaterdag":
                                            dayOfWeek = 6;
                                            break;
                                        case "Zondag":
                                            dayOfWeek = 7;
                                            break;
                                        default:
                                            dayOfWeek = 5;
                                            break;
                                    }

                                    var newPerformance = new Performance
                                    {
                                        PerformanceId = performance.id,
                                        Artist = new Artist
                                        {
                                            ImageLink = photo != null
                                                ? new UrlLink("https:" + (string) performance.photo)
                                                : null,
                                            Name = performance.title,
                                            Summary = performance.title
                                        },
                                        InterestPercentage = new Percentage(0),
                                        PerformanceTime = IsNullOrEmpty(startTime) || IsNullOrEmpty(endTime) ? null : new PerformanceTime(dayOfWeek, startTime, endTime),
                                        Stage = new Stage
                                        {
                                            Name = stage.title
                                        },
                                    };

                                    await performancesRepository.InsertPerformance(newPerformance);
                                }
                                catch (Exception e)
                                {
                                    var exception = e;
                                }
                            }
                        }
                    }
                }
            }

            return webHost;
        }
    }
}
