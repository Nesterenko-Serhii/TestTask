using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class Coordinates
    {
        [JsonPropertyName("lat")] 
        public double Latitude { get;}
        
        [JsonPropertyName("lon")]
        public double Longitude { get;}

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}