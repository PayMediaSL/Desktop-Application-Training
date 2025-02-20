using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LearningDesctopApplication
{
    public static class LoggingUtility
    {
        private static readonly string BaseLogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        public static void LogUserActivity(string userName, string windowName, string activity)
        {
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string userLogDirectory = Path.Combine(BaseLogDirectory, dateFolder);

            if (!Directory.Exists(userLogDirectory))
            {
                Directory.CreateDirectory(userLogDirectory);
            }

            string logFilePath = Path.Combine(userLogDirectory, $"{userName}.log");

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {windowName} - {activity}";
                writer.WriteLine(logEntry);
            }
        }

        public static void LogButtonPress(string userName, string buttonName)
        {
            string windowName = GetActiveWindowName();
            LogUserActivity(userName, windowName, $"Pressed({buttonName})");
        }

        public static void LogResult(string userName, string result)
        {
            string windowName = GetActiveWindowName();
            LogUserActivity(userName, windowName, $"Result({result})");
        }

        private static string GetActiveWindowName()
        {
            Window? activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            return activeWindow?.Title ?? "Unknown Window";
        }
    }
}
