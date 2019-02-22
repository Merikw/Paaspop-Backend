using System.Collections.Generic;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class Artist : Model
    {
        public string Name { get; set; }
        public UrlLink ImageLink { get; set; }
        public string Summary { get; set; }
        public ISet<string> Genres { get; set; }
    }
}