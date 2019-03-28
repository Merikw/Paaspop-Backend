using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications
{
    public class ArtistStartsJob : NotificationSettings, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var performance = (Performance) context.MergedJobDataMap["performance"];
            var userRepository = (IUsersRepository)context.MergedJobDataMap["userRepository"];
            var users = await userRepository.GetUsersByFavorites(performance.Id);
            var deviceTokens = users.Select(u => u.NotificationToken).ToArray();

            var messageInformation = new NotificationMessage
            {
                Notification = new Notification
                {
                    Title = performance.Artist.Name + " begint zo!",
                    Text = performance.Artist.Name + " begint over 10 minuten op " + performance.Stage.Name
                },
                Registration_ids = deviceTokens
            };

            var authorizationKey = $"key={Environment.GetEnvironmentVariable("FCM_KEY")}";
            var jsonBody = JsonConvert.SerializeObject(messageInformation);

            using (var client = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    await client.SendAsync(httpRequest);
                }
            }
        }
    }
}