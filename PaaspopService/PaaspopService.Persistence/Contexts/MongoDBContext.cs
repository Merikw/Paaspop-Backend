using MongoDB.Driver;
using PaaspopService.Persistence.Settings;
using Microsoft.Extensions.Options;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Contexts
{
    public class MongoDBContext : IDBContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<User> GetUsersCollection() { return _database.GetCollection<User>("Users"); }
        public IMongoCollection<Performance> GetPerformanceCollection() { return _database.GetCollection<Performance>("Performances"); }
        public IMongoCollection<Place> GetPlaceCollection() { return _database.GetCollection<Place>("Places"); }
        public IMongoCollection<Stage> GetStagesCollection() { return _database.GetCollection<Stage>("Stages"); }
        public IMongoCollection<Artist> GetArtistsCollection() { return _database.GetCollection<Artist>("Artists"); }
    }
}
