using System;
using System.Windows;
using LearningDesctopApplication.Model;

namespace LearningDesctopApplication
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var context = new ApplicationContext())
            {
                try
                {
                    if (context.Database.EnsureCreated())
                    {
                        LoggingUtility.LogResult("System", "Database not found! Created database.");
                    }
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogException(ex);
                    throw;
                }
            }
        }
    }
}
