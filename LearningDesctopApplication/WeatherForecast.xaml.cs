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
            datePickerFromDate.SelectedDate = DateTime.Today;
            datePickerToDate.SelectedDate = DateTime.Today.AddDays(5);
        }

        private async void GetWeatherBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!datePickerFromDate.SelectedDate.HasValue || !datePickerToDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select both dates");
                    return;
                }

                DateRange dateRange = new DateRange
                {
                    StartDate = DateOnly.FromDateTime(datePickerFromDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(datePickerToDate.SelectedDate.Value)
                };

                JsonContent content = JsonContent.Create(dateRange);
                HttpResponseMessage response = await _httpClient.PostAsync("weather-for-range", content);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                IEnumerable<WeatherForecast1>? forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast1>>(jsonResponse);

                wrapPanelWeather.Children.Clear();

                foreach (WeatherForecast1 forecast in forecasts)
                {
                    WeatherTileControl tile = new WeatherTileControl();
                    tile.SetWeatherData(forecast);
                    wrapPanelWeather.Children.Add(tile);
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
