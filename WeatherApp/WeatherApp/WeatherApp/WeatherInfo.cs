using System;
using System.Collections.Generic;

namespace WeatherApp
{
    public class WeatherInfo
    {
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; }
        public int Visibility { get; set; }
        public int AQI { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public List<ForecastDay> DailyForecast { get; set; }
    }
}
