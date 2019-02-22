using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.Domain.Entities
{
    public class Stage : Model
    {
        public string Name { get; set; }
        public LocationCoordinate Location { get; set; }
    }
}