using Newtonsoft.Json;

namespace PaaspopService.Application.Infrastructure.PushNotifications
{
    public class NotificationMessage
    {
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
