using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherData
    {
        [JsonPropertyName("dt")]
        public long Date { get; }

        [JsonPropertyName("temp")]
        public TemperatureData Temperature { get; }

        [JsonPropertyName("rain")]
        public double? Rain { get; }
        
        public WeatherData(long date, TemperatureData temperature, double? rain)
        {
            Date = date;
            Temperature = temperature;
            Rain = rain;
        }
    }
}