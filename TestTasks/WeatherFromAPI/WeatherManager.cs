using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public class WeatherManager
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherManager() : this(new WeatherApiClient(new HttpClient(), "YOUR_API_KEY")) { }

        public WeatherManager(IWeatherApiClient weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }
        public async Task<WeatherComparisonResult> CompareWeather(string cityA, string cityB, int dayCount)
        {
            if (dayCount < 1 || dayCount > 5)
            {
                throw new ArgumentException("Number of days should be between 1 and 5.");
            }
            
            var coordinatesA = await _weatherApiClient.GetCoordinates(cityA);
            var coordinatesB = await _weatherApiClient.GetCoordinates(cityB);
            
            var weatherA = await _weatherApiClient.GetWeatherData(coordinatesA, dayCount);
            var weatherB = await _weatherApiClient.GetWeatherData(coordinatesB, dayCount);

            var (warmerDays, rainierDays) = WeatherStats.Calculate(weatherA, weatherB);
            
            return new WeatherComparisonResult(cityA, cityB, warmerDays, rainierDays);
        }
    }
}
    