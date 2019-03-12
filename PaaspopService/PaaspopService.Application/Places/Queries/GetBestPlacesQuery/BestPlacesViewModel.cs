using System.Collections.Generic;

namespace PaaspopService.Application.Places.Queries.GetBestPlacesQuery
{
    public class BestPlacesViewModel
    {
        public Dictionary<string, List<BestPlace>> BestPlaces { get; set; }
    }
}
