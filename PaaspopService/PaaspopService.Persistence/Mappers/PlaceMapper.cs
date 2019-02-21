using MongoDB.Bson.Serialization;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class PlaceMapper
    {
        private PlaceMapper()
        {
        }

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Place>(placeMap => { placeMap.AutoMap(); });
        }
    }
}