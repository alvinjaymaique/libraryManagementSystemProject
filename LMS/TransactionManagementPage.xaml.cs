using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Linq;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace LMS
{
    public partial class TransactionManagementPage : Page
    {
        private ObservableCollection<Book> AvailableBooks { get; set; }
        private ObservableCollection<Book> CheckedOutBooks { get; set; }
        private ObservableCollection<OverdueFee> OverdueFees { get; set; }
        private Database database { get; set; }
        private Transaction selectedTransaction { get; set; }
        private Patron selectedPatron { get; set; }
        private MainWindow mainWindow;

        public TransactionManagementPage(Database db, MainWindow mainWindowReference)
        {
            InitializeComponent();
            AvailableBooks = TransactionManager.AvailableBooksProperty;
            CheckedOutBooks = TransactionManager.CheckedOutBooksProperty;
            OverdueFees = TransactionManager.OverdueFeesProperty;
            database = db;
            LoadAvailableBooks();  // Populate available books (this can be dynamic from your data)


            AvailableBooksListBox.ItemsSource = AvailableBooks;
            CheckedOutBooksListBox.ItemsSource = CheckedOutBooks;
            OverdueFeesDataGrid.ItemsSource = OverdueFees;
            mainWindow = mainWindowReference;
        }

        // Load the available books (you can replace this with actual data from your database)
        private async void LoadAvailableBooks()
        {
            //AvailableBooks.Add(new Book { Title = "Book 1", Author = "Author 1", ISBN = "12345" });
            //AvailableBooks.Add(new Book { Title = "Book 2", Author = "Author 2", ISBN = "67890" });
            // Add more books as needed
            await TransactionManager.GetAvailableBooks(database.GetConnection());
        }

        // Checkout Book
        private async void CheckoutBook_Click(object sender, RoutedEventArgs e)
        {
            string patronName = PatronNameTextBox.Text;
            if (IsPatronSearchBarEmpty()) return;

            //PatronManager.Patrons.FirstOrDefault(p => p.FullName.ToLower() == patronName.ToLower());
            var selectedBook = AvailableBooksListBox.SelectedItem as Book;
            bool patronExist = PatronManager.PatronExist(patronName);
            Patron patron = PatronManager.GetPatron(patronName);
            bool isBookCheckedOut = false;

            if (!IsPatronExist(patron)) return;
            await ReloadCheckedOutBooks(patron);
            if (patronExist) isBookCheckedOut = TransactionManager.IsBookCheckedOut(patron, selectedBook, database.GetConnection());

            if (isBookCheckedOut)
            {
                MessageBox.Show($"{selectedBook.Title} is already checked out by {patron.FullName}.", "Book Checked Out", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(CheckoutDatePicker.SelectedDate.ToString()) || string.IsNullOrEmpty(DueDatePicker.SelectedDate.ToString()))
            {
                MessageBox.Show("Complete the details", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isBookCheckedOut)
            {
                // Mark as checked out and update availability
                //AvailableBooks.Remove(selectedBook);
                Transaction transaction = new Transaction(selectedBook, patron, (DateTime)CheckoutDatePicker.SelectedDate, (DateTime)DueDatePicker.SelectedDate, DateTime.MinValue);
                await TransactionManager.CheckOutBook(transaction, database.GetConnection());
                //CheckedOutBooks.Add(selectedBook);
                MessageBox.Show($"Book '{selectedBook.Title}' checked out successfully to {patronName}.");
                selectedTransaction = transaction;
                await ReloadCheckedOutBooks(patron);
                //mainWindow.NotifyAnalyticsPageOfChanges();
            }
            else
            {
                MessageBox.Show("Please select a book to checkout.");
            }
            

        }

        // Return Book
        private async void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = CheckedOutBooksListBox.SelectedItem as Book;
            if (selectedBook == null) return;
            Transaction transaction = new Transaction(selectedBook, selectedPatron, database.GetConnection());
            selectedTransaction = transaction;
            
            MessageBoxResult result = MessageBoxResult.OK;
            if (selectedBook != null)
            {
                // Calculate overdue fee (if applicable)
                (int _, double overdueFee) = Transaction.GetDaysFee(selectedPatron, selectedBook, database.GetConnection());
                //var overdueFee = CalculateOverdueFee(selectedBook);
                if (overdueFee > 0)
                {
                    result = MessageBox.Show($"The book is overdue! The fee is ${overdueFee}.", "Fine Payment", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }

                if(result != MessageBoxResult.OK)
                {
                    return;
                }
                // Mark as returned and update availability
                await TransactionManager.ReturnBook(selectedTransaction, selectedBook, database.GetConnection());
                await TransactionManager.AddSubtractBookQuantity(selectedBook, "add", database.GetConnection());
                //AvailableBooks.Add(selectedBook);
                CheckedOutBooks.Remove(selectedBook);

                MessageBox.Show($"Book '{selectedBook.Title}' returned successfully.");
            }
            else
            {
                MessageBox.Show("Please select a book to return.");
            }
        }   
        

        // View overdue fees
        private void ViewOverdueFees_Click(object sender, RoutedEventArgs e)
        {
            //// Generate overdue fees report (dummy data for demonstration)
            //OverdueFees.Add(new OverdueFee { BookTitle = "Book 1", OverdueDays = 5, FeeAmount = 2.50 });
            //OverdueFees.Add(new OverdueFee { BookTitle = "Book 2", OverdueDays = 3, FeeAmount = 1.50 });
        }

        private void PatronNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernamePlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PatronNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatronNameTextBox.Text))
            {
                UsernamePlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void BookTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            BookPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void BookTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BookTextBox.Text))
            {
                BookPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private async void SearchPatron_Click(object sender, RoutedEventArgs e)
        {
            CheckedOutBooks.Clear();
            OverdueFees.Clear();
            if (IsPatronSearchBarEmpty()) return; 
            Patron patron = PatronManager.GetPatron(PatronNameTextBox.Text);
            if (patron != null) await ReloadCheckedOutBooks(patron);
            else IsPatronExist(patron);
        }

        private bool IsPatronSearchBarEmpty()
        {
            if (string.IsNullOrEmpty(PatronNameTextBox.Text))
            {
                MessageBox.Show("Please enter a patron name.");
                return true;
            }
            return false;
        }

        private bool IsPatronExist(Patron patron)
        {
            if (patron == null)
            {
                MessageBox.Show($"{PatronNameTextBox.Text} does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PatronNameTextBox.Text = "";
                return false;
            }
            return true;
        }

        private async void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            var books = await TransactionManager.FilterBooks(searchKey: BookTextBox.Text, database.GetConnection());
            
            if (books != null && books.Any())
            {    
                AvailableBooks = books;
            }
            else
            {
                AvailableBooks = new ObservableCollection<Book>();
            }
            AvailableBooksListBox.ItemsSource = AvailableBooks;
        }

        private async Task ReloadCheckedOutBooks(Patron patron)
        {
            if (patron != null)
            {
                CheckedOutBooks.Clear();
                OverdueFees.Clear();
                selectedPatron = patron;
                await TransactionManager.GetCheckedOutBooks(patron, database.GetConnection());
            }
        }

    }

   

}
