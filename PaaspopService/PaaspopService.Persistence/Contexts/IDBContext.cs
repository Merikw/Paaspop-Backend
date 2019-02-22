using MongoDB.Driver;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Contexts
{
    public interface IDbContext
    {
        IMongoCollection<User> GetUsers();
        IMongoCollection<Performance> GetPerformances();
        IMongoCollection<Place> GetPlaces();
        IMongoCollection<Stage> GetStages();
        IMongoCollection<Artist> GetArtists();
    }
}