namespace InfluxDb.Exercise.Repository
{
    public interface IAirplaneCoordinateRepository
    {
        Task CreateAirplaneCooridnates(int count);
        Task<List<AirplaneCoordinate>> ReadAirplaneCoordinates();
    }
}
