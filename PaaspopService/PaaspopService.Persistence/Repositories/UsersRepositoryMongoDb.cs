using System.Collections.Generic;
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

        public async Task<long> GetUsersCountAsync()
        {
            return await DbContext.GetUsers().CountDocumentsAsync(new BsonDocument());
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await DbContext.GetUsers().FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task RemoveUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            await DbContext.GetUsers().DeleteOneAsync(filter);
        }

        public async Task<List<User>> GetUsersByFavorites(string performanceId)
        {
            var filter = Builders<User>.Filter.Where(user => user.FavoritePerformances.Contains(performanceId));
            var result = await DbContext.GetUsers().FindAsync(filter);
            var userList = await result.ToListAsync();
            return userList;
        }

        public async Task<List<User>> GetUsersByBoolField(string field, bool status)
        {
            var filter = Builders<User>.Filter.Eq(field, status);
            var result = await DbContext.GetUsers().FindAsync(filter);
            var userList = await result.ToListAsync();
            return userList;
        }
    }
}