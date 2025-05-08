using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient client = new HttpClient();

        private const string ApiKey = "1e8d5c7cc41ef6807530ea00968e568a";

        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";
            var response = await client.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement;

            var weatherInfo = new WeatherInfo
            {
                Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                Condition = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                Icon = root.GetProperty("weather")[0].GetProperty("icon").GetString(),
                Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                Visibility = root.TryGetProperty("visibility", out var visibilityProp) ? visibilityProp.GetInt32() : 0,
                AQI = 50,
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(root.GetProperty("sys").GetProperty("sunrise").GetInt64()).LocalDateTime,
                Sunset = DateTimeOffset.FromUnixTimeSeconds(root.GetProperty("sys").GetProperty("sunset").GetInt64()).LocalDateTime,

                DailyForecast = new List<ForecastDay>
                {
                    new ForecastDay { Day = "Mon", Temperature = 28, Icon = "01d" },
                    new ForecastDay { Day = "Tue", Temperature = 26, Icon = "02d" },
                    new ForecastDay { Day = "Wed", Temperature = 27, Icon = "10d" },
                    new ForecastDay { Day = "Thu", Temperature = 29, Icon = "01d" },
                    new ForecastDay { Day = "Fri", Temperature = 30, Icon = "01d" },
                }
            };

            return weatherInfo;
        }
    }
}
