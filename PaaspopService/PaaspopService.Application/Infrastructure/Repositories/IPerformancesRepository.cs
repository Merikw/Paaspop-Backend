using System.Collections.Generic;
using System.Threading.Tasks;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.Repositories
{
    public interface IPerformancesRepository
    {
        Task<List<Performance>> GetPerformances();
        Task<Performance> GetPerformanceById(string id);
    }
}