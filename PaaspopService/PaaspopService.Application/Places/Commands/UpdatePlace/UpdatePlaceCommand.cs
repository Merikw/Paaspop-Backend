using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Places.Commands.UpdatePlace
{ 
    public class UpdatePlaceCommand : IRequest<Place>
    {
        public Place PlaceToBeUpdated { get; set; }
    }
}
