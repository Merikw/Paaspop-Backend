using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Persistence.Repositories
{
    public class PerformancesRepositoryMongoDb : GeneralRepository, IPerformancesRepository
    {
        public PerformancesRepositoryMongoDb(IDbContext context) : base(context)
        {
        }

        public async Task<List<Performance>> GetPerformances()
        {
            var result = await DbContext.GetPerformances().FindAsync(_ => true);
            return await result.ToListAsync();
        }
    }
}
