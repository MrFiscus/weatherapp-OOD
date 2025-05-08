namespace WeatherApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblHumidity;
        private System.Windows.Forms.Label lblWind;
        private System.Windows.Forms.Label lblVisibility;
        private System.Windows.Forms.Label lblAQI;
        private System.Windows.Forms.Label lblSunrise;
        private System.Windows.Forms.Label lblSunset;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.FlowLayoutPanel hourlyPanel;
        private System.Windows.Forms.FlowLayoutPanel dailyPanel;

        private void InitializeComponent()
        {
            topPanel = new Panel();
            btnAdd = new Button();
            txtCity = new TextBox();
            btnSearch = new Button();
            lblHumidity = new Label();
            lblWind = new Label();
            lblVisibility = new Label();
            lblAQI = new Label();
            lblSunrise = new Label();
            lblSunset = new Label();
            lblTemperature = new Label();
            lblCondition = new Label();
            hourlyPanel = new FlowLayoutPanel();
            dailyPanel = new FlowLayoutPanel();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.Transparent;
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(txtCity);
            topPanel.Controls.Add(btnSearch);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(500, 50);
            topPanel.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 15);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(30, 34);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "+";
            // 
            // txtCity
            // 
            txtCity.Font = new Font("Lucida Bright", 9F);
            txtCity.Location = new Point(68, 15);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(312, 29);
            txtCity.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Lucida Bright", 9F);
            btnSearch.Location = new Point(390, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 37);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.Click += btnGetWeather_Click;
            // 
            // lblHumidity
            // 
            lblHumidity.Font = new Font("Lucida Bright", 9F);
            lblHumidity.Location = new Point(30, 70);
            lblHumidity.Name = "lblHumidity";
            lblHumidity.Size = new Size(200, 30);
            lblHumidity.TabIndex = 1;
            lblHumidity.Text = "Humidity:";
            // 
            // lblWind
            // 
            lblWind.Font = new Font("Lucida Bright", 9F);
            lblWind.Location = new Point(30, 100);
            lblWind.Name = "lblWind";
            lblWind.Size = new Size(200, 20);
            lblWind.TabIndex = 2;
            lblWind.Text = "Wind:";
            // 
            // lblVisibility
            // 
            lblVisibility.Font = new Font("Lucida Bright", 9F);
            lblVisibility.Location = new Point(30, 130);
            lblVisibility.Name = "lblVisibility";
            lblVisibility.Size = new Size(200, 30);
            lblVisibility.TabIndex = 3;
            lblVisibility.Text = "Visibility:";
            // 
            // lblAQI
            // 
            lblAQI.Font = new Font("Lucida Bright", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAQI.Location = new Point(30, 160);
            lblAQI.Name = "lblAQI";
            lblAQI.Size = new Size(200, 33);
            lblAQI.TabIndex = 4;
            lblAQI.Text = "AQI:";
            // 
            // lblSunrise
            // 
            lblSunrise.Font = new Font("Lucida Bright", 9F);
            lblSunrise.Location = new Point(251, 100);
            lblSunrise.Name = "lblSunrise";
            lblSunrise.Size = new Size(200, 20);
            lblSunrise.TabIndex = 5;
            lblSunrise.Text = "Sunrise:";
            lblSunrise.Click += lblSunrise_Click;
            // 
            // lblSunset
            // 
            lblSunset.Font = new Font("Lucida Bright", 9F);
            lblSunset.Location = new Point(251, 130);
            lblSunset.Name = "lblSunset";
            lblSunset.Size = new Size(200, 20);
            lblSunset.TabIndex = 6;
            lblSunset.Text = "Sunset:";
            // 
            // lblTemperature
            // 
            lblTemperature.BackColor = Color.FromArgb(30, 30, 30);
            lblTemperature.Font = new Font("Lucida Bright", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTemperature.ForeColor = Color.Silver;
            lblTemperature.Location = new Point(20, 213);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(450, 107);
            lblTemperature.TabIndex = 7;
            lblTemperature.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCondition
            // 
            lblCondition.BackColor = Color.FromArgb(30, 30, 30);
            lblCondition.Font = new Font("Lucida Bright", 14F);
            lblCondition.ForeColor = Color.Silver;
            lblCondition.Location = new Point(20, 302);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(450, 64);
            lblCondition.TabIndex = 8;
            lblCondition.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hourlyPanel
            // 
            hourlyPanel.BackColor = Color.Black;
            hourlyPanel.Location = new Point(20, 372);
            hourlyPanel.Name = "hourlyPanel";
            hourlyPanel.Size = new Size(450, 28);
            hourlyPanel.TabIndex = 9;
            // 
            // dailyPanel
            // 
            dailyPanel.BackColor = Color.FromArgb(30, 30, 30);
            dailyPanel.Font = new Font("Lucida Bright", 9F);
            dailyPanel.Location = new Point(20, 407);
            dailyPanel.Name = "dailyPanel";
            dailyPanel.Size = new Size(450, 120);
            dailyPanel.TabIndex = 10;
            // 
            // MainForm
            // 
            ClientSize = new Size(500, 550);
            Controls.Add(topPanel);
            Controls.Add(lblHumidity);
            Controls.Add(lblWind);
            Controls.Add(lblVisibility);
            Controls.Add(lblAQI);
            Controls.Add(lblSunrise);
            Controls.Add(lblSunset);
            Controls.Add(lblTemperature);
            Controls.Add(lblCondition);
            Controls.Add(hourlyPanel);
            Controls.Add(dailyPanel);
            ForeColor = SystemColors.ControlText;
            Name = "MainForm";
            Text = "Weather App";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}