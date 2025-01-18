using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace LMS
{
    public partial class MainWindow : Window
    {
        private readonly Database database;
        private DispatcherTimer timer;
        private TabItem previousSelectedTab;
        private ReportAndAnalyticsPage reportAnalyticsPage;
        private MainWindow mainWindow;
        private string role;
        private string username;
        private int halfWidth;
        public MainWindow(Database db, string role, string username)
        {
            InitializeComponent();
            //mainWindow = (MainWindow)Application.Current.MainWindow;
            database = db;
            this.role = role;
            this.username = username;
            halfWidth = (int)(Width / 2);

            // Initialize and start the timer for updating date
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Update date immediately
            UpdateDateDisplay();

            // Set Username Display
            UsernameDisplay.Text = $"Hi! {username[0].ToString().ToUpper() + username.Substring(1).ToLower()}";       


            reportAnalyticsPage = new ReportAndAnalyticsPage(database);
            // Navigate to Book Management page on startup
            BookManagementFrame.Navigate(new BookManagementWindow(database, this.role));
            PatronManagementFrame.Navigate(new PatronManagementWindow(database, this.role));
            TransactionManagementFrame.Navigate(new TransactionManagementPage(database, this));
            ReportAndAnalyticsFrame.Navigate(reportAnalyticsPage);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDateDisplay();
        }

        private void UpdateDateDisplay()
        {
            DateTime now = DateTime.Now;
            DateDisplay.Text = now.ToString("MMM. dd, yyyy - dddd");
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl == null) return; // Early return if sender is not a TabControl

            // Check if the selected tab is the same as the previous one
            TabItem selectedTab = (TabItem)tabControl.SelectedItem;
            if (ReportAnalyticsTab.IsSelected && ReportAnalyticsTab != previousSelectedTab)
            {
                //reportAnalyticsPage.LoadFirstData();
                //reportAnalyticsPage.ReportAnalyticsTabControl.SelectedIndex = 0;
                reportAnalyticsPage.LoadDataForTab(reportAnalyticsPage.ReportAnalyticsTabControl.SelectedItem as TabItem);
            }
            previousSelectedTab = selectedTab;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var loginRegisterWindow = new LoginRegisterForm();
                loginRegisterWindow.Show();
                this.Close();
            }
            else
            {
                // User cancelled the logout, do nothing
                // Optionally, show a message indicating cancellation
            }
        }

    }
}
