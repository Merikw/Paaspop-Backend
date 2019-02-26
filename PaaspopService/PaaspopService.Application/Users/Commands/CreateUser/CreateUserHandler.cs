using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : GeneralRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserHandler(IMapper mapper, IUsersRepository usersRepository) : base(mapper)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User userToBeCreated = Mapper.Map<User>(request);
            await _usersRepository.CreateUserAsync(userToBeCreated);
            return Unit.Value;
        }
    }
}
