using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Places.Queries.GetMeetingPointQuery
{
    public class GetMeetingPointHandler : GeneralRequestHandler<GetMeetingPointQuery, Place>
    {
        private readonly IPlacesRepository _placesRepository;

        public GetMeetingPointHandler(IMapper mapper, IPlacesRepository placesRepository, IMediator mediator = null) : base(mapper, mediator)
        {
            _placesRepository = placesRepository;
        }

        public override async Task<Place> Handle(GetMeetingPointQuery request, CancellationToken cancellationToken)
        {
            var places = await _placesRepository.GetPlacesByType(PlaceType.MeetingPoint);
            var closestPlace = new Place()
            {
                Location = new LocationCoordinate(0.0, 0.0)
            };
            foreach (var place in places)
            {
                if (place.GetDistanceFrom(request.LocationOfUser).AbsoluteDistance <
                    closestPlace.GetDistanceFrom(request.LocationOfUser).AbsoluteDistance)
                {
                    closestPlace = place;
                }
            }

            return closestPlace;
        }
    }
}
