using System.Collections.Generic;
using System.Linq;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public static class WeatherStats
    {
        public static (int WarmerDaysCount, int RainierDaysCount) Calculate(List<WeatherData> weatherA, List<WeatherData> weatherB)
        {
            var warmerDays = weatherA.Zip(weatherB, (a, b) => a.Temperature.Day > b.Temperature.Day).Count(x => x);
            var rainierDays = weatherA.Zip(weatherB, (a, b) => (a.Rain ?? 0) > (b.Rain ?? 0)).Count(x => x);

            return (warmerDays, rainierDays);
        }
    }
}