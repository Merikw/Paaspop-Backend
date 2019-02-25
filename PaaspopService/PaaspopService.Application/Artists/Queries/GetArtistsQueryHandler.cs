using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Artists.Queries
{
    public class GetArtistsQueryHandler : GeneralRequestHandler<GetArtistQuery, ArtistViewModel>
    {
        private readonly IArtistsRepository _artistsRepository;

        public GetArtistsQueryHandler(IMapper mapper, IArtistsRepository artistsRepository)
            : base(mapper)
        {
            _artistsRepository = artistsRepository;
        }

        public override async Task<ArtistViewModel> Handle(GetArtistQuery request, CancellationToken cancellationToken)
        {
            Artist result = await _artistsRepository.GetArtistById(request.Id, cancellationToken);
            return Mapper.Map<ArtistViewModel>(result);
        }
    }
}