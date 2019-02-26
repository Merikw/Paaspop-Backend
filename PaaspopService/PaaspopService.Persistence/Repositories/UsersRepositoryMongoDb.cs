using System.Threading.Tasks;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Persistence.Repositories
{
    public class UsersRepositoryMongoDb : GeneralRepository, IUsersRepository
    {
        public UsersRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await DbContext.GetUsers().InsertOneAsync(user);
        }
    }
}