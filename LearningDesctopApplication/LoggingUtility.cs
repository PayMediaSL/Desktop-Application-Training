using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace LearningDesctopApplication
{
    public static class LoggingUtility
    {
        private static readonly string BaseLogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly HashSet<string> LoggedUsers = new HashSet<string>();

        public static void LogUserActivity(string userName, string windowName, string activity)
        {
            try
            {
                string dateFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string logFilePath = Path.Combine(BaseLogDirectory, dateFileName);

                if (!Directory.Exists(BaseLogDirectory))
                {
                    Directory.CreateDirectory(BaseLogDirectory);
                }

                bool writeEndMarker = true;
                if (File.Exists(logFilePath))
                {
                    string lastLine = File.ReadLines(logFilePath).LastOrDefault() ?? string.Empty;
                    writeEndMarker = lastLine != "##END##";
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    if (!writeEndMarker)
                    {
                        writer.WriteLine("##END##");
                    }

                    if (!LoggedUsers.Contains(userName))
                    {
                        writer.WriteLine($"[user - {userName}]");
                        LoggedUsers.Add(userName);
                    }

                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {windowName} - {activity}";
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
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

        public static void LogException(Exception ex)
        {
            try
            {
                string dateFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string logFilePath = Path.Combine(BaseLogDirectory, dateFileName);

                if (!Directory.Exists(BaseLogDirectory))
                {
                    Directory.CreateDirectory(BaseLogDirectory);
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    if (!(new FileInfo(logFilePath).Length > 0))
                    {
                        writer.WriteLine("##END##");
                    }

                    writer.WriteLine($"[user - Exception]");
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Exception: {ex.Message}\n{ex.StackTrace}";
                    writer.WriteLine(logEntry);
                }
            }
            catch
            {
                // no not much can do here if logging fails
            }
        }
    }
}
