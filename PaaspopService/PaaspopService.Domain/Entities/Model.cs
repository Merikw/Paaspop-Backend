using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaaspopService.Domain.Entities
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}