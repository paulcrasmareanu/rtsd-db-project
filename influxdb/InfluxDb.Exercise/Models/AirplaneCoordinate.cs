using InfluxDB.Client.Core;

namespace InfluxDb.Exercise
{
    [Measurement("airplaneCoordinate")]
    public class AirplaneCoordinate
    {
        [Column(IsTimestamp = true)]
        public DateTimeOffset TimeStamp { get; set; }

        [Column("latitude")]
        public double Latitude { get; set; }

        [Column("longitude")]
        public double Longitude { get; set; }

        [Column("altitude")]
        public long Altitude { get; set; }
    }
}