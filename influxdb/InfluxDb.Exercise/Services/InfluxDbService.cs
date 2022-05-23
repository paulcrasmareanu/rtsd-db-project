using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using System.Diagnostics;

namespace InfluxDb.Exercise.Services
{
    public class InfluxDbService: IInfluxDbService
    {
        private readonly InfluxDBClient _client;
        private readonly Stopwatch timer;
        private readonly string token = "zEUvMqLY5DbUPaTtFMAxkvdCkur6XTaIWUKXdXvsbjJF2MpyiaMwyHUAg1t7HujI2Ro7xQYFKNN1Yl_7T1jEYg==";
        private const string bucket = "rtsd";
        private const string org = "home";
        
        public InfluxDbService()
        {
            _client = InfluxDBClientFactory.Create("http://localhost:8086", token);
            timer = new Stopwatch();
        }

        public async Task WriteData<T>(List<T> data)
        {
            using var writeApi = _client.GetWriteApi();
            timer.Start();
            writeApi.WriteMeasurements(data, WritePrecision.Ns, bucket, org);
            timer.Stop();

            WriteInReport("Write", timer, data.Count);
        }

        public async Task<List<FluxTable>> QueryData()
        {
            var query = "from(bucket: \"rtsd\") |> range(start: -24h)";

            timer.Start();
            var tables = await _client.GetQueryApi().QueryAsync(query, org);
            timer.Stop();

            WriteInReport("Read", timer, tables.SelectMany(x => x.Records).Count() /3);

            return tables;
        }

        private async void WriteInReport(string requestType, Stopwatch timer, int count)
        {
            TimeSpan timeTaken = timer.Elapsed;
            string time = timeTaken.ToString(@"m\:ss\.fff");

            using StreamWriter file = new("report.txt", append: true);
            await file.WriteAsync($"{requestType} \t Execution time: {time} \t Entries: {count}\n");
        }

    }
}
