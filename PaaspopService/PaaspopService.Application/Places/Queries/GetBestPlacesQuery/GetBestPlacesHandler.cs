using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PaaspopService.Application.Infrastructure;
using PaaspopService.Application.Infrastructure.Repositories;

namespace PaaspopService.Application.Places.Queries.GetBestPlacesQuery
{
    public class GetBestPlacesHandler : GeneralRequestHandler<GetBestPlacesQuery, BestPlacesViewModel>
    {
        private readonly IPlacesRepository _placessRepository;

        public GetBestPlacesHandler(IMapper mapper, IPlacesRepository placesRepository) : base(mapper)
        {
            _placessRepository = placesRepository;
        }

        public override async Task<BestPlacesViewModel> Handle(GetBestPlacesQuery request,
            CancellationToken cancellationToken)
        {
            var places = await _placessRepository.GetPlaces();
            var bestPlacesDict = new Dictionary<string, List<BestPlace>>();
            var maxPercentage = 0;
            foreach (var place in places)
            {
                List<BestPlace> bestPlacesList;
                if (bestPlacesDict.ContainsKey(place.Type.ToString()))
                {
                    bestPlacesDict.TryGetValue(place.Type.ToString(), out bestPlacesList);
                    bestPlacesDict.Remove(place.Type.ToString());
                }
                else
                {
                    bestPlacesList = new List<BestPlace>();
                }

                bestPlacesList?.Add(Mapper.Map<BestPlace>(place,
                    opt => opt.Items["userlocation"] = request.UserLocationCoordinate));
                bestPlacesDict.Add(place.Type.ToString(), bestPlacesList);
                if (maxPercentage < place.CrowdPercentage.AbsolutePercentage)
                    maxPercentage = place.CrowdPercentage.AbsolutePercentage;
            }

            foreach (var entry in bestPlacesDict) entry.Value.Sort();

            return Mapper.Map<BestPlacesViewModel>(bestPlacesDict, opt => opt.Items["maxpercentage"] = maxPercentage);
        }
    }
}