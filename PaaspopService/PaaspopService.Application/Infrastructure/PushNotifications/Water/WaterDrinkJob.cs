using System.Threading.Tasks;
using Newtonsoft.Json;
using PaaspopService.Application.Infrastructure.Repositories;
using Quartz;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Water
{
    public class WaterDrinkJob : Job, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var userRepository = (IUsersRepository) context.MergedJobDataMap["usersRepository"];
            var users = await userRepository.GetUsersByBoolField("WantsWaterDrinkNotification", true);

            foreach (var user in users)
            {
                await SendNotification(JsonConvert.SerializeObject(CreateNotification("Vergeet niet om water te drinken!",
                    "Het is erg belangrijk om water te drinken op een festival om uitdroging te voorkomen, vergeet dus ook nu niet om even wat water te drinken.",
                    user.NotificationToken)));
            }
        }
    }
}