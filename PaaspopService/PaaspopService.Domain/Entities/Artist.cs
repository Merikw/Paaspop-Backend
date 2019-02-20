using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;

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
