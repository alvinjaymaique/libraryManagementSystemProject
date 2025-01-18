using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace LMS
{
    /// <summary>
    /// Interaction logic for LoginRegisterForm.xaml
    /// </summary>
    public partial class LoginRegisterForm : Window
    {
        // Admin account: user=admin; password=admin
        public LoginRegisterForm()
        {
            InitializeComponent();

            // Set the window to start in the center of the screen
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Create and initialize the LoginForm with a Database connection
            var db = new Database("root", "", "library");
            var loginForm = new LoginForm(db);

            // Initialize the connection (this should be a non-blocking operation)
            InitializeDatabaseConnection(loginForm);
        }

        private void InitializeDatabaseConnection(LoginForm loginForm)
        {
            // Attempt to establish a connection
            loginForm.InitializeConnection();

            // If the connection is successful, navigate to the LoginForm
            if (loginForm.IsConnectionSuccessful)
            {
                // Navigate to LoginForm within the LoginFrame
                LoginFrame.Navigate(loginForm);
            }
            else
            {
                // If the connection fails, close the LoginRegisterForm
                this.Close();
            }
        }
    }
}
