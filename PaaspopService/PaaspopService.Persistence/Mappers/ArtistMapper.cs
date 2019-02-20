using MongoDB.Bson.Serialization;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class ArtistMapper
    {
        private ArtistMapper() { }

        public static void Map() {
            BsonClassMap.RegisterClassMap<Artist>(artistMap =>
            {
                artistMap.AutoMap();
            });
        }
    }
}
