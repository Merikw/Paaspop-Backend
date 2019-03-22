﻿using MediatR;

namespace PaaspopService.Application.Performances.Queries.GetPerformances
{
    public class GetPerformancesQuery : IRequest<PerformanceViewModel>
    {
        public string UserId { get; set; }
    }
}
