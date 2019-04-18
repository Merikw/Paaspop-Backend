using System.ComponentModel;

namespace PaaspopService.Domain.Enumerations
{
    public enum PlaceType
    {
        [Description("Toiletten")]
        Toilet = 0,
        [Description("Bars")]
        Bar = 1,
        [Description("Eten")]
        Food = 2,
        [Description("Meeting punten")]
        MeetingPoint = 3,
    }
}