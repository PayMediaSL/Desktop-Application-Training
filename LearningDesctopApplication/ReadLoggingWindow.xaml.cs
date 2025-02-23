using System;
using System.IO;
using System.Linq;
using System.Windows;
using LearningDesctopApplication.Model;

namespace LearningDesctopApplication
{
    public partial class ReadLoggingWindow : Window
    {
        private string nic;

        public ReadLoggingWindow(string userNic)
        {
            InitializeComponent();
            nic = userNic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateUserNames();
                LoggingUtility.LogUserActivity(nic, this.GetType().Name, "Window loaded successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogResult(nic, "Error loading window");
                LoggingUtility.LogException(ex);
                MessageBox.Show("An error occurred while loading the window.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateUserNames()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var userNames = context.Users
                                           .Select(u => u.Name)
                                           .Distinct()
                                           .ToList();

                    cmbUserNames.ItemsSource = userNames;
                }
                LoggingUtility.LogUserActivity(nic, this.GetType().Name, "User names populated successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogResult(nic, "Error populating user names");
                LoggingUtility.LogException(ex);
                MessageBox.Show("An error occurred while populating user names.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            lstLogs.Items.Clear();

            string selectedDate = datePicker.SelectedDate?.ToString("yyyy-MM-dd");
            string userName = cmbUserNames.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedDate) || string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please select a date and a user name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Name == userName);
                    if (user == null)
                    {
                        MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"{selectedDate}.log");

                    if (File.Exists(logFilePath))
                    {
                        var userLogs = new Dictionary<string, List<string>>();

                        string currentUser = null;
                        foreach (string line in File.ReadLines(logFilePath))
                        {
                            if (line.StartsWith("[user - ")) // Detect user section
                            {
                                currentUser = line.Replace("[user - ", "").Replace("]", "").Trim();
                                if (!userLogs.ContainsKey(currentUser))
                                {
                                    userLogs[currentUser] = new List<string>();
                                }
                            }
                            else if (currentUser != null)
                            {
                                userLogs[currentUser].Add(line);
                            }
                        }

                        if (userLogs.TryGetValue(user.NIC, out var logs))
                        {
                            lstLogs.Items.Add($"User: {user.Name} ({user.NIC})");
                            foreach (string log in logs)
                            {
                                lstLogs.Items.Add($"   {log}"); // Indent logs for readability
                            }
                        }
                        else
                        {
                            lstLogs.Items.Add($"No logs found for {user.Name}.");
                        }

                        LoggingUtility.LogUserActivity(nic, this.GetType().Name, "Logs filtered and grouped successfully.");
                    }
                    else
                    {
                        LoggingUtility.LogUserActivity(nic, this.GetType().Name, $"No logs found for {userName} on {selectedDate}.");
                        MessageBox.Show("No logs found for the selected date and user name.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogResult(nic, $"Error filtering logs for {userName} on {selectedDate}");
                LoggingUtility.LogException(ex);
                MessageBox.Show("An error occurred while filtering logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
