using System.Threading.Tasks;
using Newtonsoft.Json;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Artist
{
    public class ArtistStartsJob : Job, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var performance = (Performance) context.MergedJobDataMap["performance"];
            var userRepository = (IUsersRepository) context.MergedJobDataMap["usersRepository"];
            var users = await userRepository.GetUsersByFavorites(performance.Id);

            foreach (var user in users)
            {
                await SendNotification(JsonConvert.SerializeObject(CreateNotification(performance.Artist.Name + " begint zo!",
                    performance.Artist.Name + " begint over 10 minuten op " + performance.Stage.Name,
                    user.NotificationToken)));
            }
        }
    }
}