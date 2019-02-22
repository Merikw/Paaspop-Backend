using MediatR;

namespace PaaspopService.Application.Artists.Queries
{
    public class GetArtistQuery : IRequest<ArtistViewModel>
    {
        public string Id { get; set; }
    }
}
