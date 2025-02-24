using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace LearningDesctopApplication
{
    public partial class WeatherForecast : Window
    {
        private readonly HttpClient _httpClient;
        public WeatherForecast()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7072/api/weather-for-range")
            };
            FromDate.SelectedDate = DateTime.Today;
            ToDate.SelectedDate = DateTime.Today.AddDays(5);
        }

        private async void GetWeatherBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!FromDate.SelectedDate.HasValue || !ToDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select both dates");
                    return;
                }

                var dateRange = new DateRange
                {
                    StartDate = DateOnly.FromDateTime(FromDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(ToDate.SelectedDate.Value)
                };

                var content = JsonContent.Create(dateRange);
                var response = await _httpClient.PostAsync("weather-for-range", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast1>>(jsonResponse);

                WeatherTilesPanel.Children.Clear();

                foreach (WeatherForecast1 forecast in forecasts)
                {
                    WeatherTileControl tile = new WeatherTileControl();
                    tile.SetWeatherData(forecast);
                    WeatherTilesPanel.Children.Add(tile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
    public class DateRange
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
    public class WeatherForecast1
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }

}
