using MongoDB.Bson.Serialization;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class PerformanceMapper
    {
        private PerformanceMapper()
        {
        }

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Performance>(performanceMap => { performanceMap.AutoMap(); });
        }
    }
}