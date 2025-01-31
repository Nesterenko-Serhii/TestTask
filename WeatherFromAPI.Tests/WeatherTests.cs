using Moq;
using TestTasks.WeatherFromAPI;
using TestTasks.WeatherFromAPI.Models;

namespace WeatherFromAPI.Tests;

public class WeatherTests
{
    [Fact]
    public void WeatherStats_Calculate_ReturnsCorrectCounts()
    {
        // Arrange
        var weatherA = new List<WeatherData>
        {
            new WeatherData(1, new TemperatureData(25), 2),
            new WeatherData(2, new TemperatureData(20), 0),
            new WeatherData(3, new TemperatureData(22), 1)
        };
        var weatherB = new List<WeatherData>
        {
            new WeatherData(1, new TemperatureData(20), 1),
            new WeatherData(2, new TemperatureData(22), 5),
            new WeatherData(3, new TemperatureData(21), 0)
        };

        // Act
        var (warmerDays, rainierDays) = WeatherStats.Calculate(weatherA, weatherB);
        
        // Assert
        Assert.Equal(2, warmerDays);
        Assert.Equal(2, rainierDays);
    }
    
    [Fact]
    public async Task WeatherManager_CompareWeather_ReturnsCorrectResult()
    {
        // Arrange
        var mockApiClient = new Mock<IWeatherApiClient>();
        mockApiClient.Setup(m => m.GetCoordinates("CityA")).ReturnsAsync(new Coordinates(10, 20));
        mockApiClient.Setup(m => m.GetCoordinates("CityB")).ReturnsAsync(new Coordinates(30, 40));
        
        var weatherA = new List<WeatherData>
        {
            new WeatherData(1, new TemperatureData(25), 2),
            new WeatherData(2, new TemperatureData(20), 0)
        };
        var weatherB = new List<WeatherData>
        {
            new WeatherData(1, new TemperatureData(20), 1),
            new WeatherData(2, new TemperatureData(22), 5)
        };
        
        mockApiClient.Setup(m => m.GetWeatherData(It.IsAny<Coordinates>(), It.IsAny<int>()))
            .ReturnsAsync((Coordinates c, int d) => c.Latitude == 10 ? weatherA : weatherB);
        
        // Act
        var manager = new WeatherManager(mockApiClient.Object);
        var result = await manager.CompareWeather("CityA", "CityB", 2);
        
        // Assert
        Assert.Equal("CityA", result.CityA);
        Assert.Equal("CityB", result.CityB);
        Assert.Equal(1, result.WarmerDaysCount);
        Assert.Equal(1, result.RainierDaysCount);
    }
}