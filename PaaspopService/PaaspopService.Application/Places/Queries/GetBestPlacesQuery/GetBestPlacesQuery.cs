using MediatR;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Places.Queries.GetBestPlacesQuery
{
    public class GetBestPlacesQuery : IRequest<BestPlacesViewModel>
    {
        public LocationCoordinate UserLocationCoordinate { get; set; }
    }
}
