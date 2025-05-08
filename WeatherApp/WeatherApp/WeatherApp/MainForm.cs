using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class MainForm : Form
    {
        private readonly IWeatherService weatherService;

        public MainForm()
        {
            InitializeComponent();
            weatherService = new WeatherService();
        }

        private async void btnGetWeather_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text.Trim();
            if (string.IsNullOrWhiteSpace(city)) return;

            try
            {
                var data = await weatherService.GetWeatherAsync(city);

                lblTemperature.Text = $"{Math.Round(data.Temperature)}°C";
                lblCondition.Text = data.Condition;
                lblHumidity.Text = $"Humidity: {data.Humidity}%";
                lblWind.Text = $"Wind: {data.WindSpeed} km/h";
                lblVisibility.Text = $"Visibility: {data.Visibility} m";
                lblAQI.Text = $"AQI: {data.AQI}";
                lblSunrise.Text = $"Sunrise: {data.Sunrise:HH:mm}";
                lblSunset.Text = $"Sunset: {data.Sunset:HH:mm}";

                dailyPanel.Controls.Clear();
                foreach (var forecast in data.DailyForecast)
                {
                    Panel dayCard = new Panel
                    {
                        Width = 80,
                        Height = 100,
                        BackColor = Color.Transparent
                    };

                    var lblDay = new Label
                    {
                        Text = forecast.Day,
                        ForeColor = Color.White,
                        AutoSize = false,
                        Width = 80,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    var icon = new PictureBox
                    {
                        Size = new Size(40, 40),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        ImageLocation = $"https://openweathermap.org/img/wn/{forecast.Icon}@2x.png"
                    };

                    var lblTemp = new Label
                    {
                        Text = $"{Math.Round(forecast.Temperature)}°",
                        ForeColor = Color.White,
                        AutoSize = false,
                        Width = 80,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    dayCard.Controls.Add(lblDay);
                    dayCard.Controls.Add(icon);
                    dayCard.Controls.Add(lblTemp);

                    lblDay.Top = 0;
                    icon.Top = lblDay.Bottom;
                    lblTemp.Top = icon.Bottom;

                    dailyPanel.Controls.Add(dayCard);
                }

                string condition = data.Condition.ToLower();
                if (condition.Contains("cloud"))
                    this.BackColor = Color.SteelBlue;
                else if (condition.Contains("rain"))
                    this.BackColor = Color.DarkSlateGray;
                else if (condition.Contains("clear"))
                    this.BackColor = Color.MidnightBlue;
                else
                    this.BackColor = Color.DimGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve weather: {ex.Message}");
            }
        }

        private void lblSunrise_Click(object sender, EventArgs e)
        {

        }
    }
}
