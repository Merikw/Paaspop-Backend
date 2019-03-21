using MediatR;

namespace PaaspopService.Application.Users.Commands.RemoveUser
{
    public class RemoveUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}
