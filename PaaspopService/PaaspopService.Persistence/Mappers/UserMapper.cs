using MongoDB.Bson.Serialization;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Persistence.Mappers
{
    public class UserMapper
    {
        private UserMapper() { }

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<User>(userMap =>
            {
                userMap.AutoMap();
            });
        }
    }
}
