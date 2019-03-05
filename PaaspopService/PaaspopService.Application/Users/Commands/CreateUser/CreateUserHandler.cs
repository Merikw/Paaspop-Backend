using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : GeneralRequestHandler<CreateUserCommand, User>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserHandler(IMapper mapper, IUsersRepository usersRepository) : base(mapper)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToBeCreated = Mapper.Map<User>(request);
            await _usersRepository.CreateUserAsync(userToBeCreated);
            return userToBeCreated;
        }
    }
}