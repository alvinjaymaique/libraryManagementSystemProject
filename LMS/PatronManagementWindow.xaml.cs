using LMS;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LMS
{
    public partial class PatronManagementWindow : Page
    {
        public ObservableCollection<Patron> Patrons { get; set; }
        private readonly Database database;
        private readonly string role;

        public PatronManagementWindow(Database db, string role)
        {
            InitializeComponent();
            Patrons = PatronManager.Patrons;
            PatronDataGrid.ItemsSource = PatronManager.Patrons;

            database = db;
            _ = LoadData();
            this.role = role;
            SetPriviliges();
        }

        public void SetPriviliges()
        {
            if (this.role.Equals("user"))
            {
                DeletePatronBtn.IsEnabled = false;
                DeletePatronBtn.Visibility = Visibility.Collapsed;
            }
        }

        private async Task LoadData()
        {
            await PatronManager.LoadFromDatabase(database.GetConnection());
            PatronDataGrid.Items.Refresh();
        }

        private async void AddPatron_Click(object sender, RoutedEventArgs e)
        {
            var addPatronDialog = new AddPatronWindow();
            if (addPatronDialog.ShowDialog() == true)
            {
                await PatronManager.AddPatron(addPatronDialog.NewPatron, database.GetConnection());
            }
            PatronDataGrid.Items.Refresh();
        }

        private async void EditPatron_Click(object sender, RoutedEventArgs e)
        {
            if (PatronDataGrid.SelectedItem is Patron selectedPatron)
            {
                var editPatronDialog = new EditPatronWindow(selectedPatron);
                if (editPatronDialog.ShowDialog() == true)
                {
                    await PatronManager.EditPatron(selectedPatron, database.GetConnection());
                    PatronDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a patron to edit.", "Edit Patron", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeletePatron_Click(object sender, RoutedEventArgs e)
        {
            if (PatronDataGrid.SelectedItem is Patron selectedPatron)
            {
                if (MessageBox.Show($"Are you sure you want to delete \"{selectedPatron.FullName}\"?", "Delete Patron", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Patrons.Remove(selectedPatron);
                    await PatronManager.DeletePatron(selectedPatron, database.GetConnection());
                    PatronDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a patron to delete.", "Delete Patron", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void SearchPatron_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                await LoadData();
                PatronDataGrid.ItemsSource = PatronManager.Patrons;
                //return;
            }
            else
            {
                var filteredPatrons = await PatronManager.FilterPatron(query, database.GetConnection());
                if (filteredPatrons.Count > 0) PatronDataGrid.ItemsSource = filteredPatrons;
            }
            //var filteredPatrons = Patrons.Where(patron =>
            //    patron.FullName.ToLower().Contains(query) ||
            //    patron.MembershipID.ToLower().Contains(query)).ToList();
            
        }

        private async void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchKey = SearchTextBox.Text.ToLower();
            if (PatronManager.Page > 0)
            {
                PatronManager.Page--;
                
                if (string.IsNullOrEmpty(searchKey))
                {
                    // No search, load all patrons
                    //BookManager.Page = 0;
                    await LoadData();
                }
                else
                {
                    // Search is active, load filtered patrons
                    PatronDataGrid.ItemsSource = await PatronManager.FilterPatron(searchKey, database.GetConnection());
                }
            }
                
                
        }

        private async void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            PatronManager.Page++;
            string searchKey = SearchTextBox.Text.ToLower();
            
            if (string.IsNullOrEmpty(searchKey))
            {
                //BookManager.Page = 0;
                await LoadData();
            }
            else
            {
                var patrons = await PatronManager.FilterPatron(searchKey, database.GetConnection());
                //BookDataGrid.ItemsSource = await BookManager.FilterBooks(searchKey, connection);
                if (patrons.Count > 0)
                {
                    PatronDataGrid.ItemsSource = patrons;
                }
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchPlaceholder.Visibility = Visibility.Visible;
            }
        }


    }
}
