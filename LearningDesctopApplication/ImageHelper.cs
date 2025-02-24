using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LearningDesctopApplication.Helper
{
    public static class ImageHelper
    {
        public static BitmapImage GetWeatherImage(string summary)
        {
            string imagePath = GetImagePathForSummary(summary);
            return new BitmapImage(new Uri($"pack://application:,,,/WeatherApp;component/Images/{imagePath}"));
        }
        private static string GetImagePathForSummary(string summary)
        {
            return summary?.ToLower() switch
            {
                "freezing" => "freezing.png",
                "bracing" => "bracing.png",
                "chilly" => "chilly.png",
                "cool" => "cool.png",
                "mild" => "mild.png",
                "warm" => "warm.png",
                "balmy" => "balmy.png",
                "hot" => "hot.png",
                "sweltering" => "sweltering.png",
                "scorching" => "scorching.png",
                _ => "default.png"
            };
        }
    }
}
