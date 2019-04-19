using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Artist
{
    public class ArtistStartsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var performance = (Performance) context.MergedJobDataMap["performance"];
            var userRepository = (IUsersRepository) context.MergedJobDataMap["usersRepository"];
            var users = await userRepository.GetUsersByFavorites(performance.Id);

            foreach (var user in users)
            {
                var messageInformation = new Notification
                {
                    NotificationMessage = new NotificationMessage
                    {
                        Title = performance.Artist.Name + " begint zo!",
                        Body = performance.Artist.Name + " begint over 10 minuten op " + performance.Stage.Name
                    },
                    DeviceToken = user.NotificationToken
                };

                var authorizationKey = $"key={Environment.GetEnvironmentVariable("FCM_KEY")}";
                var jsonBody = JsonConvert.SerializeObject(messageInformation);

                using (var client = new HttpClient())
                {
                    using (var httpRequest =
                        new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", authorizationKey);
                        httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        await client.SendAsync(httpRequest);
                    }
                }
            }
        }
    }
}