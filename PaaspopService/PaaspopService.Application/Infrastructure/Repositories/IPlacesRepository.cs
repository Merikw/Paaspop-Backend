using System.Collections.Generic;
using System.Threading.Tasks;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Infrastructure.Repositories
{
    public interface IPlacesRepository
    {
        Task<List<Place>> GetPlaces();
        Task UpdatePlaceAsync(Place place);
    }
}
