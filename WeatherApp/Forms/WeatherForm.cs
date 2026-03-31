using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WeatherForecastApp.Models;
using WeatherForecastApp.Services;

namespace WeatherForecastApp
{
    public class WeatherForm : Form
    {
        private readonly TextBox cityTextBox;
        private readonly Button searchButton;
        private readonly CheckBox unitToggle;
        private readonly Label statusLabel;
        private readonly Label currentTempLabel;
        private readonly Label weatherLabel;
        private readonly Label locationLabel;
        private readonly Label highLowLabel;
        private readonly Label humidityValueLabel;
        private readonly Label windValueLabel;
        private readonly PictureBox weatherIcon;
        private readonly FlowLayoutPanel forecastPanel;

        private readonly IWeatherService weatherService = new OpenWeatherService();

        public WeatherForm()
        {
            Text = "Weather Forecast";
            Width = 920;
            Height = 720;
            MinimumSize = new Size(820, 640);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(11, 24, 38);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            DoubleBuffered = true;

            var shell = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(28, 24, 28, 24),
                BackColor = Color.FromArgb(11, 24, 38)
            };

            var headerCard = CreateCard(new Size(820, 128), Color.FromArgb(27, 49, 74));
            headerCard.Margin = new Padding(0, 0, 0, 18);
            headerCard.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            var titleLabel = new Label
            {
                Text = "Weather Dashboard",
                Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(24, 16)
            };

            statusLabel = new Label
            {
                Text = "Search for a city to load live weather and forecast details.",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(203, 219, 232),
                AutoSize = true,
                Location = new Point(26, 58)
            };

            cityTextBox = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.FromArgb(241, 246, 251),
                ForeColor = Color.FromArgb(26, 32, 44),
                Location = new Point(26, 84),
                Width = 360
            };

            searchButton = new Button
            {
                Text = "Search",
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 180, 71),
                ForeColor = Color.FromArgb(42, 30, 9),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(398, 82),
                Width = 110,
                Height = 32,
                Cursor = Cursors.Hand
            };
            searchButton.FlatAppearance.BorderSize = 0;

            unitToggle = new CheckBox
            {
                Text = "Use Celsius",
                Checked = true,
                AutoSize = true,
                Location = new Point(530, 88),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Regular),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(233, 240, 247)
            };

            headerCard.Controls.Add(titleLabel);
            headerCard.Controls.Add(statusLabel);
            headerCard.Controls.Add(cityTextBox);
            headerCard.Controls.Add(searchButton);
            headerCard.Controls.Add(unitToggle);

            var currentCard = CreateCard(new Size(820, 220), Color.FromArgb(20, 37, 58));
            currentCard.Margin = new Padding(0, 0, 0, 18);

            var currentHeader = new Label
            {
                Text = "Current Conditions",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(149, 181, 213),
                AutoSize = true,
                Location = new Point(24, 18)
            };

            locationLabel = new Label
            {
                Text = "No city loaded yet",
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(24, 48)
            };

            weatherLabel = new Label
            {
                Text = "Weather details will appear here.",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(214, 225, 237),
                AutoSize = true,
                Location = new Point(28, 92)
            };

            currentTempLabel = new Label
            {
                Text = "--",
                Font = new Font("Segoe UI Semibold", 44F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 198, 105),
                AutoSize = true,
                Location = new Point(22, 116)
            };

            highLowLabel = new Label
            {
                Text = "High --   Low --",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(196, 214, 229),
                AutoSize = true,
                Location = new Point(30, 175)
            };

            weatherIcon = new PictureBox
            {
                Size = new Size(160, 160),
                Location = new Point(620, 34),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            currentCard.Controls.Add(currentHeader);
            currentCard.Controls.Add(locationLabel);
            currentCard.Controls.Add(weatherLabel);
            currentCard.Controls.Add(currentTempLabel);
            currentCard.Controls.Add(highLowLabel);
            currentCard.Controls.Add(weatherIcon);

            var metricsRow = new Panel
            {
                Size = new Size(820, 110),
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 18)
            };

            var humidityCard = CreateMetricCard("Humidity", "0%", Color.FromArgb(27, 57, 74), out humidityValueLabel);
            humidityCard.Location = new Point(0, 0);
            humidityCard.Size = new Size(398, 104);

            var windCard = CreateMetricCard("Wind", "0 km/h", Color.FromArgb(45, 64, 77), out windValueLabel);
            windCard.Location = new Point(422, 0);
            windCard.Size = new Size(398, 104);

            metricsRow.Controls.Add(humidityCard);
            metricsRow.Controls.Add(windCard);

            var forecastCard = CreateCard(new Size(820, 270), Color.FromArgb(18, 33, 51));
            forecastCard.Margin = new Padding(0, 0, 0, 8);

            var forecastHeader = new Label
            {
                Text = "5-Day Outlook",
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(24, 18)
            };

            var forecastHint = new Label
            {
                Text = "Midday forecast snapshots for the upcoming days.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(161, 188, 212),
                AutoSize = true,
                Location = new Point(26, 47)
            };

            forecastPanel = new FlowLayoutPanel
            {
                Location = new Point(24, 78),
                Size = new Size(772, 168),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            forecastCard.Controls.Add(forecastHeader);
            forecastCard.Controls.Add(forecastHint);
            forecastCard.Controls.Add(forecastPanel);

            shell.Controls.Add(headerCard);
            shell.Controls.Add(currentCard);
            shell.Controls.Add(metricsRow);
            shell.Controls.Add(forecastCard);
            Controls.Add(shell);

            searchButton.Click += async (sender, e) => await LoadWeatherAsync();
            cityTextBox.KeyDown += async (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await LoadWeatherAsync();
                }
            };

            ShowForecastPlaceholder();
        }

        private async System.Threading.Tasks.Task LoadWeatherAsync()
        {
            var city = cityTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(city))
            {
                statusLabel.Text = "Enter a city name before searching.";
                cityTextBox.Focus();
                return;
            }

            try
            {
                searchButton.Enabled = false;
                statusLabel.Text = $"Loading weather for {city}...";

                var data = await weatherService.GetWeatherAsync(city, unitToggle.Checked);
                var unitSymbol = unitToggle.Checked ? "C" : "F";
                var windUnit = unitToggle.Checked ? "km/h" : "mph";

                locationLabel.Text = data.City;
                weatherLabel.Text = ToTitleCase(data.Description);
                currentTempLabel.Text = $"{data.Temperature:0.#} {unitSymbol}";
                highLowLabel.Text = $"High {data.HighTemperature:0.#} {unitSymbol}   Low {data.LowTemperature:0.#} {unitSymbol}";
                humidityValueLabel.Text = $"{data.Humidity}%";
                windValueLabel.Text = $"{data.WindSpeed:0.#} {windUnit}";
                statusLabel.Text = $"Updated just now for {data.City}.";

                weatherIcon.Image = null;
                weatherIcon.LoadAsync(BuildIconUrl(data.Icon, true));

                RenderForecast(data, unitSymbol);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Unable to load weather right now.";
                MessageBox.Show("Error: " + ex.Message, "Weather Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                searchButton.Enabled = true;
            }
        }

        private void RenderForecast(WeatherData data, string unitSymbol)
        {
            forecastPanel.SuspendLayout();
            forecastPanel.Controls.Clear();

            if (data.Forecasts == null || data.Forecasts.Count == 0)
            {
                ShowForecastPlaceholder();
                forecastPanel.ResumeLayout();
                return;
            }

            foreach (var forecast in data.Forecasts)
            {
                var row = CreateCard(new Size(744, 64), Color.FromArgb(27, 45, 67));
                row.Margin = new Padding(0, 0, 0, 10);

                var dayLabel = new Label
                {
                    Text = forecast.Date.ToString("dddd"),
                    Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(18, 20)
                };

                var icon = new PictureBox
                {
                    ImageLocation = BuildIconUrl(forecast.Icon, false),
                    Size = new Size(40, 40),
                    Location = new Point(135, 12),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent
                };

                var descriptionLabel = new Label
                {
                    Text = ToTitleCase(forecast.Description),
                    Font = new Font("Segoe UI", 10.5F),
                    ForeColor = Color.FromArgb(214, 225, 237),
                    AutoSize = true,
                    Location = new Point(188, 21)
                };

                var tempLabel = new Label
                {
                    Text = $"{forecast.Temperature:0.#} {unitSymbol}",
                    Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 198, 105),
                    AutoSize = true,
                    Location = new Point(640, 20)
                };

                row.Controls.Add(dayLabel);
                row.Controls.Add(icon);
                row.Controls.Add(descriptionLabel);
                row.Controls.Add(tempLabel);
                forecastPanel.Controls.Add(row);
            }

            forecastPanel.ResumeLayout();
        }

        private void ShowForecastPlaceholder()
        {
            forecastPanel.Controls.Clear();

            var placeholder = CreateCard(new Size(744, 90), Color.FromArgb(24, 40, 60));
            placeholder.Margin = new Padding(0, 0, 0, 10);

            var title = new Label
            {
                Text = "Forecast will appear here",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(18, 18)
            };

            var subtitle = new Label
            {
                Text = "Search for any city to load the current weather, highs and lows, humidity, wind, and daily outlook.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(184, 204, 222),
                AutoSize = true,
                MaximumSize = new Size(690, 0),
                Location = new Point(18, 42)
            };

            placeholder.Controls.Add(title);
            placeholder.Controls.Add(subtitle);
            forecastPanel.Controls.Add(placeholder);
        }

        private static Panel CreateMetricCard(string title, string value, Color backColor, out Label valueLabel)
        {
            var card = CreateCard(new Size(380, 104), backColor);

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(184, 205, 223),
                AutoSize = true,
                Location = new Point(18, 16)
            };

            valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 38)
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            return card;
        }

        private static Panel CreateCard(Size size, Color backColor)
        {
            var panel = new Panel
            {
                Size = size,
                BackColor = backColor
            };

            ApplyRoundedCorners(panel, 24);
            panel.Resize += (_, _) => ApplyRoundedCorners(panel, 24);
            return panel;
        }

        private static void ApplyRoundedCorners(Control control, int radius)
        {
            var path = new GraphicsPath();
            var diameter = radius * 2;
            var bounds = new Rectangle(0, 0, control.Width, control.Height);

            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }

        private static string BuildIconUrl(string iconCode, bool largeIcon)
        {
            if (string.IsNullOrWhiteSpace(iconCode))
            {
                return string.Empty;
            }

            var sizeSuffix = largeIcon ? "@2x" : string.Empty;
            return $"https://openweathermap.org/img/wn/{iconCode}{sizeSuffix}.png";
        }

        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "Unavailable";
            }

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
    }
}
