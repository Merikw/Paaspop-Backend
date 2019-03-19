using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser
{
    public class GetFavoritePerformancesFromUserQuery : IRequest<List<Performance>>
    {
        public string UserId { get; set; }
    }
}
