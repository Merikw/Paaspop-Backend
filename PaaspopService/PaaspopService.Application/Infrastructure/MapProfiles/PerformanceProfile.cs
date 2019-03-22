using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaaspopService.Application.Performances.Queries;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.MapProfiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            CreateMap<List<Performance>, PerformanceViewModel>()
                .ForMember(dest => dest.Performances,
                    opt => opt.MapFrom(src =>
                        src.GroupBy(x => x.Stage.Name).ToDictionary(x => x.Key, x => x.ToList())));
        }
    }
}