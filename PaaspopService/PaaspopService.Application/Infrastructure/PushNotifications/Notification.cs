using Newtonsoft.Json;

namespace PaaspopService.Application.Infrastructure.PushNotifications
{
    public class Notification
    {
        [JsonProperty(PropertyName = "notification")]
        public NotificationMessage NotificationMessage { get; set; }

        [JsonProperty(PropertyName = "to")] public string DeviceToken { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; } = "high";
    }
}