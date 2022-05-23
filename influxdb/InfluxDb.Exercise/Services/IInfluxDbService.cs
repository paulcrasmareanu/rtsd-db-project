using InfluxDB.Client.Core.Flux.Domain;

namespace InfluxDb.Exercise.Services
{
    public interface IInfluxDbService
    {
        public Task WriteData<T>(List<T> data);
        public Task<List<FluxTable>> QueryData();
    }
}
