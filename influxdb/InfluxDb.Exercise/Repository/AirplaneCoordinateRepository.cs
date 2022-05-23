using InfluxDb.Exercise.Services;

namespace InfluxDb.Exercise.Repository
{
    public class AirplaneCoordinateRepository : IAirplaneCoordinateRepository
    {
        private readonly IInfluxDbService _influxDbService;
        public AirplaneCoordinateRepository(IInfluxDbService influxDbService)
        {
           _influxDbService = influxDbService;
        }

        public async Task CreateAirplaneCooridnates(int count)
        {
            var coordinates = new List<AirplaneCoordinate>();
            if (count > 0)
            {
                for (var i = 1; i <= count; i++)
                {
                    coordinates.Add(CreateCoordinate(i));
                }
            }

            await _influxDbService.WriteData(coordinates);
        }


        // TODO: Mapping properties from db in model 
         public async Task<List<AirplaneCoordinate>> ReadAirplaneCoordinates()
        {
            var tables =  await _influxDbService.QueryData();

            var list = new List<AirplaneCoordinate>();

            return list;
        }

        private AirplaneCoordinate CreateCoordinate(int count)
        {
            var airplaneCoordinate = new AirplaneCoordinate
            {
                TimeStamp = DateTime.Now,
                Latitude = 0.1 + count,
                Longitude = 0.1 + count,
                Altitude  = 1000 * count,
            };

            return airplaneCoordinate;
        }
    }
}
