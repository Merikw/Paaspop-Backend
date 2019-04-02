using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Places.Queries.GetPlacesQuery
{
    public class GetPlacesHandler : GeneralRequestHandler<GetPlacesQuery, List<Place>>
    {
        private readonly IPlacesRepository _placesRepository;

        public GetPlacesHandler(IMapper mapper, IPlacesRepository placesRepository, IMediator mediator = null) : base(mapper, mediator)
        {
            _placesRepository = placesRepository;
        }

        public override async Task<List<Place>> Handle(GetPlacesQuery request, CancellationToken cancellationToken)
        {
            return await _placesRepository.GetPlaces();
        }
    }
}
