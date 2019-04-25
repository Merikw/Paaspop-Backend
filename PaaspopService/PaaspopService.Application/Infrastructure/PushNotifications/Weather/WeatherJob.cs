using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Weather
{
    public class WeatherJob : Job, IJob
    {
        private static readonly string openWeatherLink = "http://api.openweathermap.org/data/2.5/forecast?lat=51.642618&lon=5.4175&appid="
                                                        + Environment.GetEnvironmentVariable("OPEN_WEATHER_APPID") + "&units=metric";

        public async Task Execute(IJobExecutionContext context)
        {
            WeatherNotificationObject weatherNotificationObject;
            var userRepository = (IUsersRepository) context.MergedJobDataMap["usersRepository"];
            var users = await userRepository.GetUsersByBoolField("WantsWeatherForecast", true);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(openWeatherLink, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                weatherNotificationObject = new WeatherNotificationObject(
                    Convert.ToInt32(jsonObject.list[0].weather[0].id),
                    Convert.ToInt32(jsonObject.list[0].main.temp), Convert.ToInt32(jsonObject.list[0].dt));
            }

            foreach (var user in users)
            {
                await SendNotification(JsonConvert.SerializeObject(CreateNotification(weatherNotificationObject.Title,
                    weatherNotificationObject.Description, user.NotificationToken)));
            }
        }
    }
}