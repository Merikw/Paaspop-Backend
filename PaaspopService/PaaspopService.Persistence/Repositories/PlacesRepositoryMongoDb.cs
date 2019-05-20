using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Persistence.Contexts;


namespace PaaspopService.Persistence.Repositories
{
    public class PlacesRepositoryMongoDb : GeneralRepository, IPlacesRepository
    {
        public PlacesRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task InsertPlace(Place place)
        {
            await DbContext.GetPlaces().InsertOneAsync(place);
        }

        public async Task<Place> GetPlaceById(string id)
        {
            var filter = Builders<Place>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await DbContext.GetPlaces().FindAsync(filter);
            return result.FirstOrDefault();
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

        public async Task<List<Place>> GetPlacesByType(PlaceType type)
        {
            var filter = Builders<Place>.Filter.Eq("Type", type);
            var result = await DbContext.GetPlaces().FindAsync(filter);
            var placesList = await result.ToListAsync();
            return placesList;
        }
    }
}
