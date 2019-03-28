using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Application.Infrastructure.PushNotifications
{
    public class NotificationMessage
    {
        public string[] Registration_ids { get; set; }
        public Notification Notification { get; set; }
        public object Data { get; set; }
    }
}
