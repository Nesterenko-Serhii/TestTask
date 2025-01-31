using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherApiResponse
    {
        [JsonPropertyName("hourly")]
        public List<WeatherData> Hourly { get; set; } = new();
    }
}