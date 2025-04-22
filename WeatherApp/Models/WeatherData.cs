using System;
using System.Collections.Generic;

namespace WeatherForecastApp.Models
{
    public class WeatherData
    {
        public string City { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; }
        public List<DailyForecast> Forecasts { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public string Icon { get; set; }
    }
}