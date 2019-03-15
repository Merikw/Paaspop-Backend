using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class ModelMapper
    {
        private ModelMapper()
        {
        }

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Model>(modelMap =>
            {
                modelMap.MapIdMember(model => model.Id).
                    SetIdGenerator(ObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}