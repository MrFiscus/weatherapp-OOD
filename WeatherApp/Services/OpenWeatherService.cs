using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecastApp.Models;
using System.Collections.Generic;

namespace WeatherForecastApp.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly string apiKey = "1e8d5c7cc41ef6807530ea00968e568a";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<WeatherData> GetWeatherAsync(string city, bool useCelsius = true)
        {
            string units = useCelsius ? "metric" : "imperial";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units={units}";
            string forecastUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units={units}";

            var weatherData = new WeatherData();
            var response = await httpClient.GetAsync(url);
            var forecastResponse = await httpClient.GetAsync(forecastUrl);

            if (!response.IsSuccessStatusCode || !forecastResponse.IsSuccessStatusCode)
                throw new Exception("API call failed.");

            var weatherJson = JObject.Parse(await response.Content.ReadAsStringAsync());
            var forecastJson = JObject.Parse(await forecastResponse.Content.ReadAsStringAsync());

            weatherData.City = weatherJson["name"]?.ToString() ?? city;
            weatherData.Description = weatherJson["weather"]?[0]?["description"]?.ToString() ?? "Unavailable";
            weatherData.Temperature = (double?)weatherJson["main"]?["temp"] ?? 0;
            weatherData.HighTemperature = (double?)weatherJson["main"]?["temp_max"] ?? weatherData.Temperature;
            weatherData.LowTemperature = (double?)weatherJson["main"]?["temp_min"] ?? weatherData.Temperature;
            weatherData.Humidity = (int?)weatherJson["main"]?["humidity"] ?? 0;
            weatherData.WindSpeed = (double?)weatherJson["wind"]?["speed"] ?? 0;
            weatherData.Icon = weatherJson["weather"]?[0]?["icon"]?.ToString() ?? string.Empty;

            var forecasts = new List<DailyForecast>();
            foreach (var item in forecastJson["list"] ?? new JArray())
            {
                var dateText = item["dt_txt"]?.ToString();
                if (string.IsNullOrWhiteSpace(dateText))
                {
                    continue;
                }

                var date = DateTime.Parse(dateText);
                if (date.Hour == 12)
                {
                    forecasts.Add(new DailyForecast
                    {
                        Date = date,
                        Temperature = (double?)item["main"]?["temp"] ?? 0,
                        Description = item["weather"]?[0]?["description"]?.ToString() ?? "Unavailable",
                        Icon = item["weather"]?[0]?["icon"]?.ToString() ?? string.Empty
                    });
                }
            }

            weatherData.Forecasts = forecasts;
            return weatherData;
        }
    }
}
