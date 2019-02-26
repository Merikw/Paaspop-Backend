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
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> GetUsers()
        {
            return _database.GetCollection<User>("Users");
        }

        public IMongoCollection<Performance> GetPerformances()
        {
            return _database.GetCollection<Performance>("Performances");
        }

        public IMongoCollection<Place> GetPlaces()
        {
            return _database.GetCollection<Place>("Places");
        }

        public IMongoCollection<Stage> GetStages()
        {
            return _database.GetCollection<Stage>("Stages");
        }

        public IMongoCollection<Artist> GetArtists()
        {
            return _database.GetCollection<Artist>("Artists");
        }
    }
}