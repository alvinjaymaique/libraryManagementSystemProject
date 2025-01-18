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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LMS
{
    /// <summary>
    /// Interaction logic for HelpPageNavigation.xaml
    /// </summary>
    public partial class HelpPageNavigation : Window
    {
        public HelpPageNavigation()
        {
            InitializeComponent();
        }

        private void PreviousButton2_Click(object sender, RoutedEventArgs e)
        {
            HelpPage helpPage = new HelpPage(); 
            helpPage.Show();
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            About about = new About();  
            about.Show();
            this.Close();

        }
    }
}
