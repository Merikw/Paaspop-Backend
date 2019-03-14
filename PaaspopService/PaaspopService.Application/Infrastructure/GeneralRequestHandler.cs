using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace PaaspopService.Application.Infrastructure
{
    public abstract class GeneralRequestHandler<TQuery, TModel> : IRequestHandler<TQuery, TModel>
        where TQuery : IRequest<TModel>
    {
        protected readonly IMapper Mapper;
        protected IMediator Mediator;

        protected GeneralRequestHandler(IMapper mapper, IMediator mediator = null)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        public abstract Task<TModel> Handle(TQuery request, CancellationToken cancellationToken);
    }
}