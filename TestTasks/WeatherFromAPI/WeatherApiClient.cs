using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherApiClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }
        public async Task<Coordinates> GetCoordinates(string city)
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var locations = JsonSerializer.Deserialize<List<Coordinates>>(await response.Content.ReadAsStringAsync());

            if (locations == null || locations.Count == 0)
            {
                throw new ArgumentException($"City '{city}' not found");
            }
            
            return locations[0];
        }

        public async Task<List<WeatherData>> GetWeatherData(Coordinates coordinates, int days)
        {
            var weatherDataList = new List<WeatherData>();
            long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            for (int i = 0; i < days; i++)
            {
                
                long targetUnixTime = currentUnixTime - (i * 86400);
            
                string url = $"https://api.openweathermap.org/data/2.5/onecall/timemachine?lat={coordinates.Latitude}&lon={coordinates.Longitude}&dt={targetUnixTime}&appid={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to retrieve weather data: {response.StatusCode}");
                }
                
                var content = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(content);
                var middayWeather = weatherResponse?.Hourly
                    ?.FirstOrDefault(w => DateTimeOffset.FromUnixTimeSeconds(w.Date).UtcDateTime.Hour == 12);

                if (middayWeather != null) 
                {
                    weatherDataList.Add(middayWeather);
                }
            }

            return weatherDataList;
        }
    }
}