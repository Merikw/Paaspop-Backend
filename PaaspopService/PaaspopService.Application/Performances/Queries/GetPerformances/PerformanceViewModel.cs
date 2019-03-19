using System.Collections.Generic;
using PaaspopService.Domain.Entities;

namespace PaaspopService.Application.Performances.Queries.GetPerformances
{
    public class PerformanceViewModel
    {
        public Dictionary<string, List<Performance>> Performances { get; set; }
    }
}
