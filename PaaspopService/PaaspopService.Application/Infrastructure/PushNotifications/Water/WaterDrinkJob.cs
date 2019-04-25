using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Water
{
    public class WaterDrinkJob : IJob
    {
        private const string sendLink = "https://fcm.googleapis.com/fcm/send";

        public async Task Execute(IJobExecutionContext context)
        {
            var userRepository = (IUsersRepository) context.MergedJobDataMap["usersRepository"];
            var users = await userRepository.GetUsersByBoolField("WantsWaterDrinkNotification", true);

            foreach (var user in users)
            {
                var messageInformation = new Notification
                {
                    NotificationMessage = new NotificationMessage
                    {
                        Title = "Vergeet niet om water te drinken!",
                        Body =
                            "Het is erg belangrijk om water te drinken op een festival om uitdroging te voorkomen, vergeet dus ook nu niet om even wat water te drinken."
                    },
                    DeviceToken = user.NotificationToken
                };

                var authorizationKey = $"key={Environment.GetEnvironmentVariable("FCM_KEY")}";
                var jsonBody = JsonConvert.SerializeObject(messageInformation);

                using (var client = new HttpClient())
                {
                    using (var httpRequest =
                        new HttpRequestMessage(HttpMethod.Post, sendLink))
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