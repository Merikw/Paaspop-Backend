using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;


namespace PaaspopService.Persistence.Repositories
{
    public class PlacesRepositoryMongoDb : GeneralRepository, IPlacesRepository
    {
        public PlacesRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task<List<Place>> GetPlaces()
        {
            var result = await DbContext.GetPlaces().FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task UpdatePlaceAsync(Place place)
        {
            var filter = Builders<Place>.Filter.Eq("_id", ObjectId.Parse(place.Id));
            var update = Builders<Place>.Update.Set("Name", place.Name)
                .Set("Type", place.Type)
                .Set("CrowdPercentage", place.CrowdPercentage)
                .Set("Location", place.Location)
                .Set("UsersOnPlace", place.UsersOnPlace);
            await DbContext.GetPlaces().FindOneAndUpdateAsync<Place>(filter, update);
        }
    }
}
