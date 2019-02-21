using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Persistence.Contexts;

namespace PaaspopService.Application.Infrastructure
{
    public abstract class GeneralRequestHandler<TQuery, TModel> : IRequestHandler<TQuery, TModel>
        where TQuery : IRequest<TModel>
    {
        protected readonly IDbContext Context;
        protected readonly IMapper Mapper;

        protected GeneralRequestHandler(IDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public abstract Task<TModel> Handle(TQuery request, CancellationToken cancellationToken);
    }
}