using System;
using System.Drawing;
using System.Windows.Forms;
using WeatherForecastApp.Models;
using WeatherForecastApp.Services;

namespace WeatherForecastApp
{
    public class WeatherForm : Form
    {
        private TextBox cityTextBox;
        private Button searchButton;
        private Label weatherLabel;
        private PictureBox weatherIcon;
        private FlowLayoutPanel forecastPanel;
        private CheckBox unitToggle;
        private Label humidityLabel, windLabel;

        private IWeatherService weatherService = new OpenWeatherService();

        private void InitializeComponent()
        {

        }

        public WeatherForm()
        {
            Text = "Weather Forecast";
            Width = 600;
            Height = 650;
            BackColor = Color.FromArgb(18, 32, 47);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 11);

            cityTextBox = new TextBox { Left = 20, Top = 20, Width = 300 };
            searchButton = new Button { Text = "Search", Left = 330, Top = 18, Width = 80 };
            unitToggle = new CheckBox { Text = "°C / °F", Left = 420, Top = 22, Checked = true, BackColor = Color.Transparent, ForeColor = Color.White };

            weatherLabel = new Label { Left = 20, Top = 70, Width = 400, Height = 40 };
            weatherIcon = new PictureBox { Left = 440, Top = 60, Width = 60, Height = 60, SizeMode = PictureBoxSizeMode.Zoom };

            humidityLabel = new Label { Left = 20, Top = 120, Width = 250 };
            windLabel = new Label { Left = 280, Top = 120, Width = 250 };

            forecastPanel = new FlowLayoutPanel
            {
                Left = 20,
                Top = 180,
                Width = 540,
                Height = 380,
                AutoScroll = true,
                BackColor = Color.FromArgb(25, 40, 60),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(cityTextBox);
            Controls.Add(searchButton);
            Controls.Add(unitToggle);
            Controls.Add(weatherLabel);
            Controls.Add(weatherIcon);
            Controls.Add(humidityLabel);
            Controls.Add(windLabel);
            Controls.Add(forecastPanel);

            searchButton.Click += async (sender, e) =>
            {
                try
                {
                    var data = await weatherService.GetWeatherAsync(cityTextBox.Text, unitToggle.Checked);
                    weatherLabel.Text = $"{data.City}: {data.Description}, {data.Temperature}°";
                    weatherIcon.Load($"http://openweathermap.org/img/w/{data.Icon}.png");
                    humidityLabel.Text = $"Humidity: {data.Humidity}%";
                    windLabel.Text = $"Wind: {data.WindSpeed} km/h";

                    forecastPanel.Controls.Clear();
                    foreach (var forecast in data.Forecasts)
                    {
                        var container = new Panel
                        {
                            Width = 500,
                            Height = 60,
                            Margin = new Padding(5),
                            BackColor = Color.FromArgb(30, 45, 65)
                        };

                        var icon = new PictureBox
                        {
                            ImageLocation = $"http://openweathermap.org/img/w/{forecast.Icon}.png",
                            Width = 50,
                            Height = 50,
                            Location = new Point(10, 5),
                            SizeMode = PictureBoxSizeMode.Zoom
                        };

                        var label = new Label
                        {
                            Text = $"{forecast.Date:ddd}: {forecast.Description}, {forecast.Temperature}°",
                            Location = new Point(70, 20),
                            AutoSize = true
                        };

                        container.Controls.Add(icon);
                        container.Controls.Add(label);
                        forecastPanel.Controls.Add(container);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            };
        }
    }
}