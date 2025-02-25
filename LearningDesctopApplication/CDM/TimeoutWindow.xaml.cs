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
using System.Windows.Shapes;

namespace LearningDesctopApplication.CDM
{
    /// <summary>
    /// Interaction logic for TimeoutWindow.xaml
    /// </summary>
    /// 
    public partial class TimeoutWindow : Window
    {
        public bool ExtendTime { get; private set; }
        private BlurOverlay _blurOverlay;
        public TimeoutWindow()
        {
            InitializeComponent();
            this.Loaded += TimeoutWindow_Loaded;
            this.Closed += TimeoutWindow_Closed;
        }
        private void TimeoutWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            _blurOverlay = new BlurOverlay();
            _blurOverlay.Owner = this.Owner; 
            _blurOverlay.Show();
        }

        private void TimeoutWindow_Closed(object sender, System.EventArgs e)
        {
           
            if (_blurOverlay != null)
            {
                _blurOverlay.Close();
            }
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            ExtendTime = true;
            this.Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            ExtendTime = false;
            this.Close();
        }
    }
}
