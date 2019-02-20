using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using PaaspopService.Application.Artists.Models;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaaspopService.Application.Artists.Queries
{
    public class GetArtistsQueryHandler : IRequestHandler<GetArtistQuery, ArtistViewModel>
    {
        private readonly IDBContext _context;

        public GetArtistsQueryHandler(IDBContext context)
        {
            _context = context;
        }

        public async Task<ArtistViewModel> Handle(GetArtistQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Artist>.Filter.Eq("Id", request.Id);
            var result =  _context.GetArtistsCollection().Find(filter).FirstOrDefault();

            return ArtistViewModel.Create(result);
        }
    }
}
