using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Persistence.Repositories
{
    public class ArtistsRepositoryMongoDb : GeneralRepository, IArtistsRepository
    {
        public ArtistsRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task<Artist> GetArtistById(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Artist>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await DbContext.GetArtists().FindAsync(filter, cancellationToken: cancellationToken);
            return result.FirstOrDefault();
        }
    }
}
