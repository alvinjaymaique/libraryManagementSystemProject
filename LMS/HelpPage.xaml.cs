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
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Window
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void PreviousButton1_Click(object sender, RoutedEventArgs e)
        {
            LoginRegisterForm loginRegisterForm = new LoginRegisterForm();
            loginRegisterForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loginRegisterForm.Show();
            this.Close();

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            HelpPageNavigation helpPageNavigation = new HelpPageNavigation();
            helpPageNavigation.Show();
            this.Close();
        }
    }
}
