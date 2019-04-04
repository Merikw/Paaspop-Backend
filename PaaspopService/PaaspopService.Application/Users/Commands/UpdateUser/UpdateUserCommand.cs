using System.Collections.Generic;
using MediatR;
using PaaspopService.Application.Infrastructure.Enums;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<User>
    {
        public string Id { get; set; }
        public int Gender { get; set; }
        public Age Age { get; set; }
        public bool WantsWeatherForecast { get; set; }
        public bool WantsWaterDrinkNotification { get; set; } 
        public LocationCoordinate CurrentLocation { get; set; }
        public ISet<string> FavoritePerformances { get; set; }
        public UserUpdateType UserUpdateType { get; set; }
    }
}
