using System.Collections.Generic;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Application.Artists.Queries
{
    public class ArtistViewModel : Model
    {
        public string Name { get; set; }
        public UrlLink ImageLink { get; set; }
        public string Summary { get; set; }
        public ISet<string> Genres { get; set; }
    }
}