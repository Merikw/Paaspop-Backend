using MediatR;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Places.Queries.GetMeetingPointQuery
{
    public class GetMeetingPointQuery : IRequest<Place>
    {
        public LocationCoordinate LocationOfUser { get; set; }
    }
}
