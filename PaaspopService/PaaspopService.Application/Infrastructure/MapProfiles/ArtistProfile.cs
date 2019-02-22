using AutoMapper;
using PaaspopService.Application.Artists.Queries;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.MapProfiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistViewModel>();
        }
    }
}