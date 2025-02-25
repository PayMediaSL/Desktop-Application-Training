using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
namespace LearningDesctopApplication.CDM
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        private int countdown = 30;
        public Welcome()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            countdown--;

            if (countdown == 0)
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    TimeoutWindow timeoutWindow = new TimeoutWindow();
                    timeoutWindow.Owner = this;
                    timeoutWindow.ShowDialog();

                    if (!timeoutWindow.ExtendTime)
                    {
                        this.Close();
                    }
                    else
                    {
                        countdown = 30;
                        timer.Start();
                    }
                });
            }
        }

        private void Language_Selected(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Button button = (Button)sender;
            string selectedLanguage = button.Content.ToString();

            CategoryWindow categoryWindow = new CategoryWindow(selectedLanguage);
            categoryWindow.ShowDialog();
            this.Close();
        }

    }
}
