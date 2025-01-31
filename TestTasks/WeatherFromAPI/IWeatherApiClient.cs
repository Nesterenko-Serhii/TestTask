using System.Collections.Generic;
using System.Threading.Tasks;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public interface IWeatherApiClient
    {
        Task<Coordinates> GetCoordinates(string city);
        Task<List<WeatherData>> GetWeatherData(Coordinates coordinates, int days);
    }
}