using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Application.Artists.Queries
{
    public class GetArtistsQueryHandler : GeneralRequestHandler<GetArtistQuery, ArtistViewModel>
    {
        public GetArtistsQueryHandler(IDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override async Task<ArtistViewModel> Handle(GetArtistQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Artist>.Filter.Eq("_id", ObjectId.Parse(request.Id));
            var result = await Context.GetArtists().FindAsync(filter, cancellationToken: cancellationToken);

            return Mapper.Map<ArtistViewModel>(result.FirstOrDefault());
        }
    }
}