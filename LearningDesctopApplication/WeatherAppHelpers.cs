using WeatherApp.Enums;

namespace LearningDesctopApplication.Helper
{
    public static class WeatherHelper
    {
        public static WeatherCondition GetWeatherCondition(string summary, int TemperatureC)
        {
            if (string.IsNullOrEmpty(summary))
                return WeatherCondition.Unknown;

            return summary switch
            {
                "Freezing" => WeatherCondition.Freezing,
                "Bracing" => WeatherCondition.Bracing,
                "Chilly" => WeatherCondition.Chilly,
                "Cool" => WeatherCondition.Cool,
                "Mild" => WeatherCondition.Mild,
                "Warm" => WeatherCondition.Warm,
                "Balmy" => WeatherCondition.Balmy,
                "Hot" => WeatherCondition.Hot,
                "Sweltering" => WeatherCondition.Sweltering,
                "Scorching" => WeatherCondition.Scorching,
                _ => WeatherCondition.Unknown
            };
        }

        public static string GetImagePath(WeatherCondition condition)
        {
            return condition switch
            {
                WeatherCondition.Freezing => "Images/unknown.png",
                WeatherCondition.Bracing => "Images/unknown.png",
                WeatherCondition.Chilly => "Images/unknown.png",
                WeatherCondition.Cool => "Images/unknown.png",
                WeatherCondition.Mild => "Images/unknown.png",
                WeatherCondition.Warm => "Images/unknown.png",
                WeatherCondition.Balmy => "Images/unknown.png",
                WeatherCondition.Hot => "Images/unknown.png",
                WeatherCondition.Sweltering => "Images/unknown.png",
                WeatherCondition.Scorching => "Images/unknown.png",
                _ => "Images/unknown.png"
            };
        }
    }
}
