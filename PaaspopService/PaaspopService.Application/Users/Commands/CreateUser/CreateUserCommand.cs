using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public int Gender { get; set; }
        public int Age { get; set; }
    }
}