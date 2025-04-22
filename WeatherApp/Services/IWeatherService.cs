using WeatherForecastApp.Models;
using System.Threading.Tasks;

namespace WeatherForecastApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city, bool useCelsius = true);
    }
}