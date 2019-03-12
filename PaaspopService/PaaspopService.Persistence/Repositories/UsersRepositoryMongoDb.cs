using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Persistence.Repositories
{
    public class UsersRepositoryMongoDb : GeneralRepository, IUsersRepository
    {
        public UsersRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await DbContext.GetUsers().InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(user.Id));
            var update = Builders<User>.Update.Set("Gender", user.Gender)
                .Set("Age", user.Age)
                .Set("WantsWeatherForecast", user.WantsWeatherForecast)
                .Set("WantsWaterDrinkNotification", user.WantsWaterDrinkNotification)
                .Set("CurrentLocation", user.CurrentLocation)
                .Set("FavoritePerformances", user.FavoritePerformances);
            await DbContext.GetUsers().FindOneAndUpdateAsync<User>(filter, update);
        }
    }
}