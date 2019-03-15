using System.Threading.Tasks;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<long> GetUsersCountAsync();
    }
}