using System.Collections.Generic;
using System.Threading.Tasks;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<long> GetUsersCountAsync();
        Task<User> GetUserByIdAsync(string id);
        Task RemoveUserAsync(string id);
        Task<List<User>> GetUsersByFavorites(string performanceId);
    }
}