using System.Threading;
using System.Threading.Tasks;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.Repositories
{
    public interface IArtistsRepository
    {
        Task<Artist> GetArtistById(string id, CancellationToken cancellationToken);
    }
}