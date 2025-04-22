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

            weatherData.City = weatherJson["name"].ToString();
            weatherData.Description = weatherJson["weather"][0]["description"].ToString();
            weatherData.Temperature = (double)weatherJson["main"]["temp"];
            weatherData.Humidity = (int)weatherJson["main"]["humidity"];
            weatherData.WindSpeed = (double)weatherJson["wind"]["speed"];
            weatherData.Icon = weatherJson["weather"][0]["icon"].ToString();

            var forecasts = new List<DailyForecast>();
            foreach (var item in forecastJson["list"])
            {
                var date = DateTime.Parse(item["dt_txt"].ToString());
                if (date.Hour == 12)
                {
                    forecasts.Add(new DailyForecast
                    {
                        Date = date,
                        Temperature = (double)item["main"]["temp"],
                        Description = item["weather"][0]["description"].ToString(),
                        Icon = item["weather"][0]["icon"].ToString()
                    });
                }
            }

            weatherData.Forecasts = forecasts;
            return weatherData;
        }
    }
}