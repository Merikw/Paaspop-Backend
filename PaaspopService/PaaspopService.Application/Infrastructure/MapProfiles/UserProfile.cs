using System.Collections.Generic;
using AutoMapper;
using MongoDB.Bson;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Application.Users.Commands.UpdateUser;
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
                    opts => opts.MapFrom(src => new Age(src.Age)))
                .ForMember(dest => dest.NotificationToken,
                    opts => opts.MapFrom(src => src.NotificationToken));

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Id,
                    opts => opts.MapFrom(src => ObjectId.Parse(src.Id)))
                .ForMember(dest => dest.Gender,
                    opts => opts.MapFrom(src => (Gender) src.Gender))
                .ForMember(dest => dest.Age,
                    opts => opts.MapFrom(src => src.Age))
                .ForMember(dest => dest.CurrentLocation,
                    opts => opts.MapFrom(src => src.CurrentLocation))
                .ForMember(dest => dest.WantsWaterDrinkNotification,
                    opts => opts.MapFrom(src => src.WantsWaterDrinkNotification))
                .ForMember(dest => dest.WantsWeatherForecast,
                    opts => opts.MapFrom(src => src.WantsWeatherForecast))
                .ForMember(dest => dest.FavoritePerformances,
                    opts => opts.MapFrom(src => new HashSet<string>(src.FavoritePerformances)));
        }
    }
}