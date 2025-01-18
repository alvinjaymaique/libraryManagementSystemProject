using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LMS
{
    public partial class BookManagementWindow : Page
    {
        //public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Book> Books = BookManager.Books;
        private readonly Database database;
        private readonly MySqlConnection connection;
        private readonly string role;

        public BookManagementWindow(Database db, string role)
        {
            InitializeComponent();
            //Books = new ObservableCollection<Book>();
            BookDataGrid.ItemsSource = Books;
            database = db;
            connection = database.GetConnection();
            LoadBooks();
            this.role = role;
            SetPriviliges();
        }

        public void SetPriviliges()
        {
            if (this.role.Equals("user"))
            {
                DeleteBookBtn.IsEnabled = false;
                DeleteBookBtn.Visibility = Visibility.Collapsed;
            }
        }

        public async void LoadBooks()
        {
            await BookManager.LoadFromDatabase(connection);
            BookDataGrid.Items.Refresh();
        }

        private async void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var addBookDialog = new AddBookWindow();
            if (addBookDialog.ShowDialog() == true)
            {
                //Books.Add(addBookDialog.NewBook);
                await BookManager.AddBook(addBookDialog.NewBook, connection);
            }
            BookDataGrid.Items.Refresh();
        }

        private async void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (BookDataGrid.SelectedItem is Book selectedBook)
            {
                var editBookDialog = new EditBookWindow(selectedBook);
                if (editBookDialog.ShowDialog() == true)
                {
                    await BookManager.EditBook(editBookDialog.EditedBook, connection);
                    BookDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a book to edit.", "Edit Book", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (BookDataGrid.SelectedItem is Book selectedBook)
            {
                if (MessageBox.Show($"Are you sure you want to delete \"{selectedBook.Title}\"?", "Delete Book", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    await BookManager.DeleteBook(selectedBook, connection);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "Delete Book", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void SearchBook_Click(object sender, RoutedEventArgs e)
        {   
            string searchKey = SearchTextBox.Text.ToLower();
            BookManager.Page = 0;
            if (string.IsNullOrEmpty(searchKey))
            {
                LoadBooks();
                BookDataGrid.ItemsSource = BookManager.Books;
            }
            else
            {
                var books = await BookManager.FilterBooks(searchKey, connection);
                if (books.Count > 0) BookDataGrid.ItemsSource = books;  
            }
            //string query = SearchTextBox.Text.ToLower();
            //var filteredBooks = Books.Where(book =>
            //    book.Title.ToLower().Contains(query) ||
            //    book.Author.ToLower().Contains(query) ||
            //    book.ISBN.ToLower().Contains(query)).ToList();
        }

        private async void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchKey = SearchTextBox.Text.ToLower();
            //NextBtn.IsEnabled = true;
            if (BookManager.Page > 0)
            {
                BookManager.Page--;
                if (string.IsNullOrEmpty(searchKey))
                {
                    // No search, load all patrons
                    //BookManager.Page = 0;
                    LoadBooks();
                }
                else
                {
                    // Search is active, load filtered patrons
                    Books = await BookManager.FilterBooks(searchKey, connection);
                    BookDataGrid.ItemsSource = Books;
                    //BookDataGrid.ItemsSource = await BookManager.FilterBooks(searchKey, connection);
                }
            }

        }

        private async void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            BookManager.Page++;
            //LoadBooks();
            string searchKey = SearchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(searchKey))
            {
                //BookManager.Page = 0;
                LoadBooks();
            }
            else
            {
                var books = await BookManager.FilterBooks(searchKey, connection);
                //BookDataGrid.ItemsSource = await BookManager.FilterBooks(searchKey, connection);
                if (books.Count > 0)
                {
                    BookDataGrid.ItemsSource = books;
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
                                