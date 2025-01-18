using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LMS
{
    /// <summary>
    /// Interaction logic for ReportAndAnalyticsPage.xaml
    /// </summary>
    public partial class ReportAndAnalyticsPage : Page
    {
        private Database database;
        private TabItem previousSelectedTab;
        private TabItem currentTab;
        //private bool firstLoadDone = false;
        private bool isOverdueBooksLoaded = false;
        private bool isCheckedOutBooksLoaded = false;
        private bool isTransactionHistoryLoaded = false;
        private bool isPatronActivityLoaded = false;
        public ReportAndAnalyticsPage(Database db)
        {
            InitializeComponent();
            database = db;
            OverdueBooksDataGrid.ItemsSource = ReportManager.OverdueBooks;
            CheckedOutBooksDataGrid.ItemsSource = ReportManager.CheckedOutBooks;
            TransactionHistoryDataGrid.ItemsSource = ReportManager.TransactionHistory;
            PatronActivityDataGrid.ItemsSource = ReportManager.PatronActivities;
        }

        public void LoadDataForTab(TabItem selectedTab)
        {
            // Hide all grids initially
            OverdueBooksDataGrid.Visibility = Visibility.Collapsed;
            CheckedOutBooksDataGrid.Visibility = Visibility.Collapsed;
            TransactionHistoryDataGrid.Visibility = Visibility.Collapsed;
            PatronActivityDataGrid.Visibility = Visibility.Collapsed;

            if (selectedTab == OverDueTab)
            {
                //ReportManager.OverdueBooks.Clear();
                OverdueBooksDataGrid.Visibility = Visibility.Visible;
                RefreshOverdueBooks();
            }
            else if (selectedTab == CheckedOutTab)
            {
                CheckedOutBooksDataGrid.Visibility = Visibility.Visible;
                RefreshCheckedOutBooks();
            }
            else if (selectedTab == TransactionTab)
            {
                TransactionHistoryDataGrid.Visibility = Visibility.Visible;
                RefreshTransactionHistory(DateTime.MinValue, DateTime.MaxValue);
            }
            else if (selectedTab == PatronActivityTab)
            {
                PatronActivityDataGrid.Visibility = Visibility.Visible;
                RefreshPatronActivities();
            }
            currentTab = selectedTab;
        }

        private async void RefreshOverdueBooks()
        {
            await ReportManager.GetOverdueBooks(database.GetConnection());
        }

        private async void RefreshCheckedOutBooks()
        {
            await ReportManager.GetCheckoutBooks(database.GetConnection());
        }
        private async void RefreshTransactionHistory(DateTime from_date, DateTime to_date) 
        {
            await ReportManager.GetTransactionHistory(from_date, to_date, database.GetConnection());
        }
        private async void RefreshPatronActivities()
        {
            await ReportManager.GetPatronActivities(database.GetConnection());
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl == null) return;

            // Load data for the selected tab
            TabItem selectedTab = (TabItem)tabControl.SelectedItem;
            LoadDataForTab(selectedTab);
        }

        private void PrevOverdueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReportManager.OverdueBooksPage > 0) 
            {
                ReportManager.OverdueBooksPage--;
                RefreshOverdueBooks();
            }
        }

        private void NextOverdueBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportManager.OverdueBooksPage++;
            RefreshOverdueBooks();
        }

        private void PrevCheckedOutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReportManager.CheckedOutBooksPage > 0)
            {
                ReportManager.CheckedOutBooksPage--;
                RefreshCheckedOutBooks();
            }
        }

        private void NextCheckedOutBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportManager.CheckedOutBooksPage++;
            RefreshCheckedOutBooks(); 
        }

        private void ApplyTransactionDateFilter_Click(object sender, RoutedEventArgs e)
        {
            if(TransactionFromDatePicker.SelectedDate != null && TransactionToDatePicker.SelectedDate != null)
            {
                RefreshTransactionHistory((DateTime)TransactionFromDatePicker.SelectedDate, (DateTime)TransactionToDatePicker.SelectedDate);
            }
        }

        private void PrevTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReportManager.TransactionHistoryPage > 0)
            {
                ReportManager.TransactionHistoryPage--;
                if (TransactionFromDatePicker.SelectedDate != null && TransactionToDatePicker.SelectedDate != null)
                {
                    RefreshTransactionHistory((DateTime)TransactionFromDatePicker.SelectedDate, (DateTime)TransactionToDatePicker.SelectedDate);
                }
                else
                {
                    RefreshTransactionHistory(DateTime.MinValue, DateTime.MaxValue);
                }
            }
        }

        private void NextTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportManager.TransactionHistoryPage++;
            if (TransactionFromDatePicker.SelectedDate != null && TransactionToDatePicker.SelectedDate != null)
            {
                RefreshTransactionHistory((DateTime)TransactionFromDatePicker.SelectedDate, (DateTime)TransactionToDatePicker.SelectedDate);
            }
            else
            {
                RefreshTransactionHistory(DateTime.MinValue, DateTime.MaxValue);
            }
        }

        private void PrevPatronActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReportManager.PatronActivitiesPage > 0)
            {
                ReportManager.PatronActivitiesPage--;
                RefreshPatronActivities();
            }
        }

        private void NextPatronActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportManager.PatronActivitiesPage++;
            RefreshPatronActivities();
        }     

        private async void ExportOverdueBooksButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = GetFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                if (currentTab == OverDueTab)
                {
                    await ReportManager.ExportCollectionAsync(ReportManager.OverdueBooks, fileName, "Overdue Books");
                }
            }
        }

        private async void ExportCheckedOutBooksButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = GetFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                if (currentTab == CheckedOutTab)
                {
                    await ReportManager.ExportCollectionAsync(ReportManager.CheckedOutBooks, fileName, "Checked-out Books");
                }
            }
        }

        private async void ExportTransactionHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = GetFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                if (currentTab == TransactionTab)
                {
                    await ReportManager.ExportCollectionAsync(ReportManager.TransactionHistory, fileName, "Transaction History");
                }
            }
        }

        private async void ExportPatronActivityButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = GetFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                if (currentTab == PatronActivityTab)
                {
                    await ReportManager.ExportCollectionAsync(ReportManager.PatronActivities, fileName, "Patron Activities");
                }
            }
        }

        private SaveFileDialog GetFileDialog()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                DefaultExt = ".csv",
                FileName = "ExportedData"
            };

            return saveFileDialog;
        }

    }

}
