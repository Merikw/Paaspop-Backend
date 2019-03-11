using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
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

        public async Task<Performance> GetPerformanceById(string id)
        {
            var filter = Builders<Performance>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await DbContext.GetPerformances().FindAsync(filter);
            return result.FirstOrDefault();
        }
    }
}
