using MongoDB.Bson.Serialization;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class StageMapper
    {
        private StageMapper()
        {
        }

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Stage>(stageMap => { stageMap.AutoMap(); });
        }
    }
}