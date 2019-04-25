using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaaspopService.Application.Infrastructure.PushNotifications
{
    public abstract class Job
    {
        protected const string sendLink = "https://fcm.googleapis.com/fcm/send";
        protected static readonly string authorizationKey = $"key={Environment.GetEnvironmentVariable("FCM_KEY")}";

        protected Notification CreateNotification(string title, string body, string deviceToken)
        {
            return new Notification
            {
                NotificationMessage = new NotificationMessage
                {
                    Title = title,
                    Body = body,
                },
                DeviceToken = deviceToken
            };
        }

        protected async Task SendNotification(string jsonBody)
        {
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
