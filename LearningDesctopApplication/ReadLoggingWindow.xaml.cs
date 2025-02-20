using System;
using System.IO;
using System.Linq;
using System.Windows;
using LearningDesctopApplication.Model;

namespace LearningDesctopApplication
{
    public partial class ReadLoggingWindow : Window
    {
        public ReadLoggingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateUserNames();
        }

        private void PopulateUserNames()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                List<string> userNames = context.Users
                                       .Select(u => u.Name)
                                       .Distinct()
                                       .ToList();

                cmbUserNames.ItemsSource = userNames;
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

            using (ApplicationContext context = new ApplicationContext())
            {
                DbContext? user = context.Users.FirstOrDefault(u => u.Name == userName);
                if (user == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", selectedDate);
                string logFilePath = Path.Combine(logDirectory, $"{user.NIC}.log");

                if (File.Exists(logFilePath))
                {
                    string[] logEntries = File.ReadAllLines(logFilePath);
                    foreach (string logEntry in logEntries)
                    {
                        lstLogs.Items.Add(logEntry);
                    }
                }
                else
                {
                    MessageBox.Show("No logs found for the selected date and user name.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
