using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Settings;

namespace PaaspopService.Persistence.Contexts
{
    public class MongoDbContext : IDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            MongoClient client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> GetUsersCollection()
        {
            return _database.GetCollection<User>("Users");
        }

        public IMongoCollection<Performance> GetPerformanceCollection()
        {
            return _database.GetCollection<Performance>("Performances");
        }

        public IMongoCollection<Place> GetPlaceCollection()
        {
            return _database.GetCollection<Place>("Places");
        }

        public IMongoCollection<Stage> GetStagesCollection()
        {
            return _database.GetCollection<Stage>("Stages");
        }

        public IMongoCollection<Artist> GetArtistsCollection()
        {
            return _database.GetCollection<Artist>("Artists");
        }
    }
}