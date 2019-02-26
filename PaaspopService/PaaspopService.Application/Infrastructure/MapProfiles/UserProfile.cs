using AutoMapper;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Infrastructure.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Gender,
                    opts => opts.MapFrom(src => (Gender) src.Gender))
                .ForMember(dest => dest.Age,
                    opts => opts.MapFrom(src => new Age(src.Age)));
        }
    }
}
