using MediatR;

namespace PaaspopService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public int Gender { get; set; }
        public int Age { get; set; }
    }
}