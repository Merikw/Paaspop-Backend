using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Persistence.Repositories
{
    public abstract class GeneralRepository
    {
        protected readonly IDbContext DbContext;

        protected GeneralRepository(IDbContext context)
        {
            DbContext = context;
        }
    }
}