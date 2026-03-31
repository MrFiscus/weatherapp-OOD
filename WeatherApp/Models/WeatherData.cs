using System;
using System.Collections.Generic;

namespace WeatherForecastApp.Models
{
    public class WeatherData
    {
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double HighTemperature { get; set; }
        public double LowTemperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; } = string.Empty;
        public List<DailyForecast> Forecasts { get; set; } = new List<DailyForecast>();
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string Icon { get; set; } = string.Empty;
    }
}
