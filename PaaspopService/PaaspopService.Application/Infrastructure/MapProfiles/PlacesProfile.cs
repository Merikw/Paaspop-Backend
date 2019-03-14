using System.Collections.Generic;
using AutoMapper;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Infrastructure.MapProfiles
{
    public class PlacesProfile : Profile
    {
        public PlacesProfile()
        {
            CreateMap<Place, BestPlace>()
                .ForMember(dest => dest.Place,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Distance,
                    opt => opt.MapFrom((source, dest, destMember, context) =>
                        source.GetDistanceFrom(context.Items["userlocation"] as LocationCoordinate)));

            CreateMap<Dictionary<string, List<BestPlace>>, BestPlacesViewModel>()
                .ForMember(dest => dest.BestPlaces,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.MaxPercentage,
                    opt => opt.MapFrom((source, dest, destMember, context) =>
                        context.Items["maxpercentage"]));
        }
    }
}
