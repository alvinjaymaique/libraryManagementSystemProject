using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
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
using MySql.Data.MySqlClient;

namespace LMS
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Page
    {
        // Admin account: user=admin; password=admin
        private Database Database { get; set; }
        private readonly MySqlConnection connection;
        public bool IsConnectionSuccessful { get; set; }
        public LoginForm()
        {

        }

        public LoginForm(Database db)
        {
            InitializeComponent();
            Database = db;
            connection = db.GetConnection();
        }

        public void InitializeConnection()
        {
            bool connectionSuccessful = false;

            while (!connectionSuccessful)
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    connectionSuccessful = true;  // If connection is successful, exit the loop
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show(
                        $"Error: {ex.Message}\nWould you like to try again?",
                        "Error Database Connection",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error
                    );

                    if (result == MessageBoxResult.No)
                    {
                        IsConnectionSuccessful = false;
                        Window.GetWindow(this)?.Close(); // This closes the parent window
                        return; // Exit constructor
                    }
                }
            }

            IsConnectionSuccessful = true;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Example login validation (replace with your actual authentication logic)
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            (bool isValid, string role, string name) = await IsValidLogin(username, password);
            LoginBtn.IsEnabled = false;
            if (isValid)
            {
                // Proceed to next screen or main application page
                MessageBox.Show("Login successful!");
                MainWindow mainWindow = new MainWindow(Database, role, name);
                mainWindow.Show();
                Window.GetWindow(this)?.Close();
            }
            else
            {
                // Display error message
                ErrorMessageTextBlock.Text = "Invalid username or password.";
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                LoginBtn.IsEnabled = true;
            }
        }

        private async Task<(bool, string, string)> IsValidLogin(string username, string password)
        {
            LoginBtn.IsEnabled = false;
            return await AccountManager.IsValidAccount(username, password, connection);
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernamePlaceholder.Visibility = Visibility.Collapsed;
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernamePlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        // Handle hyperlink click to navigate to RegisterForm
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var registerForm = new RegisterForm(Database);
            NavigationService.Navigate(registerForm);
            e.Handled = true;
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HelpPage helpPage = new HelpPage();
            helpPage.Show();
            Window.GetWindow(this).Close(); // Close the current window (LoginForm)
        }
    }

   
}
