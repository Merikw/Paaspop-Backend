using MediatR;
using PaaspopService.Application.Artists.Models;

namespace PaaspopService.Application.Artists.Queries
{
    public class GetArtistQuery : IRequest<ArtistViewModel>
    {
        public int Id { get; set; }
    }
}
