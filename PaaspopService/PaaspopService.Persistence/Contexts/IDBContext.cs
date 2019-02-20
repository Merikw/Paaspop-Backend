using MongoDB.Driver;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Contexts
{
    public interface IDBContext
    {
        IMongoCollection<User> GetUsersCollection();
        IMongoCollection<Performance> GetPerformanceCollection();
        IMongoCollection<Place> GetPlaceCollection();
        IMongoCollection<Stage> GetStagesCollection();
        IMongoCollection<Artist> GetArtistsCollection();
    }
}
