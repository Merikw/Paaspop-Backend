using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : GeneralRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUsersRepository _usersRepository;

        public UpdateUserHandler(IMapper mapper, IUsersRepository usersRepository) : base(mapper)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToBeUpdates = Mapper.Map<User>(request);
            await _usersRepository.UpdateUserAsync(userToBeUpdates);
            return userToBeUpdates;
        }
    }
}