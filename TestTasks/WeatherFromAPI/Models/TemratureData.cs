using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class TemperatureData
    {
        [JsonPropertyName("day")]
        public double Day { get; }
        public TemperatureData(double day)
        {
            Day = day;
        }
    }
}