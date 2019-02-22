namespace PaaspopService.Persistence.Mappers
{
    public class GeneralMapper
    {
        public static void Map()
        {
            ArtistMapper.Map();
            ModelMapper.Map();
            PerformanceMapper.Map();
            PlaceMapper.Map();
            StageMapper.Map();
            UserMapper.Map();
        }
    }
}
