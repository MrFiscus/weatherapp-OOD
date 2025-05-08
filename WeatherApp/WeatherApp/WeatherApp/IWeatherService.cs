using System.Threading.Tasks;

namespace WeatherApp
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(string city);
    }
}