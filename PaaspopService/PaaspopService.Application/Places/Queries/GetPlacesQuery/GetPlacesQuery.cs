using System.Collections.Generic;
using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Places.Queries.GetPlacesQuery
{
    public class GetPlacesQuery : IRequest<List<Place>>
    {
    }
}