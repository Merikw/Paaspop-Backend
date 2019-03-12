using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Places.Commands.UpdatePlace
{
    public class UpdatePlaceHandler : GeneralRequestHandler<UpdatePlaceCommand, Place>
    {
        private readonly IPlacesRepository _placesRepository;

        public UpdatePlaceHandler(IMapper mapper, IPlacesRepository placesRepository) : base(mapper)
        {
            _placesRepository = placesRepository;
        }

        public override async Task<Place> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
        {
            var placeToBeUpdated = request.PlaceToBeUpdated;
            await _placesRepository.UpdatePlaceAsync(placeToBeUpdated);
            return placeToBeUpdated;
        }
    }
}
