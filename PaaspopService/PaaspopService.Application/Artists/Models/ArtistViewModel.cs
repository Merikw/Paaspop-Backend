using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaaspopService.Application.Artists.Models
{
    public class ArtistViewModel : Model
    {
        public string Name { get; set; }
        public UrlLink ImageLink { get; set; }
        public string Summary { get; set; }
        public ISet<string> Genres { get; set; }

        public static Expression<Func<Artist, ArtistViewModel>> Projection
        {
            get
            {
                return artist => new ArtistViewModel
                {
                    Id = artist.Id
                };
            }
        }

        public static ArtistViewModel Create(Artist artist)
        {
            return Projection.Compile().Invoke(artist);
        }
    }
}
