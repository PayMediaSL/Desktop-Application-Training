using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LearningDesctopApplication.Helper;
using WeatherApp.Enums;


namespace LearningDesctopApplication
{
    public partial class WeatherTileControl : UserControl
    {
        public WeatherTileControl()
        {
            InitializeComponent();
        }

        public void SetWeatherData(WeatherForecast1 forecast)
        {
            DateText.Text = forecast.Date.ToString("MMM dd");
            Temperature.Text = forecast.TemperatureC.ToString();
            SummaryText.Text = forecast.Summary;

            WeatherCondition condition = WeatherHelper.GetWeatherCondition(forecast.Summary, forecast.TemperatureC);
            string imagePath = WeatherHelper.GetImagePath(condition);
           

            WeatherIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            //WeatherIcon.Source = ImageHelper.GetWeatherImage(imagePath);
        }
    }
}
