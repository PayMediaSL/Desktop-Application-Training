using System;
using System.Xml;
using System.Windows;
using System.IO;
using iTextSharp.xmp.impl.xpath;

namespace LearningDesctopApplication.CDM
{
    
    public partial class CategoryWindow : Window
    {
        private string selectedLanguage;

        public CategoryWindow(string language)
        {
            InitializeComponent();
            selectedLanguage = language;
            LoadCategoryTexts();
        }
        private void LoadCategoryTexts()
        {
            try
            {
                string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
                                    .Parent!
                                    .Parent!
                                    .Parent!
                                    .FullName;

               
                string xmlPath = Path.Combine(projectPath, "CDM", "Config", "Language.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);

                
                string langCode = GetLanguageCode(selectedLanguage);
                XmlNode languageNode = doc.SelectSingleNode($"//Language[@code='{langCode}']");

                if (languageNode != null)
                {
                    XmlNodeList? categories = languageNode.SelectNodes(".//Category");
                    if (categories.Count >= 3)
                    {
                        btn1.Content = categories[0].Attributes["text"].Value;
                        btn2.Content = categories[1].Attributes["text"].Value;
                        btn3.Content = categories[2].Attributes["text"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading language settings: " + ex.Message);
            }
        }
        private string GetLanguageCode(string language)
        {
            return language.ToLower() switch
            {
                "සිංහල" => "si",
                "தமிழ்" => "ta",
                "english" => "en",
                _ => "en"  
            };
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Welcome welcomeWindow = new Welcome();
            welcomeWindow.Show();
            this.Close();
        }
    }
}
