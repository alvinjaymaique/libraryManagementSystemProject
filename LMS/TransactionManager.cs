using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO.Packaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMS
{
    public static class TransactionManager
    {
        private static ObservableCollection<Book> AvailableBooks { get; set; }
        private static ObservableCollection<Book> CheckedOutBooks { get; set; }
        private static ObservableCollection<OverdueFee> OverdueFees { get; set; }
        public static ObservableCollection<Book> AvailableBooksProperty => AvailableBooks;
        public static ObservableCollection<Book> CheckedOutBooksProperty => CheckedOutBooks;
        public static ObservableCollection<OverdueFee> OverdueFeesProperty => OverdueFees;
        static TransactionManager()
        {
            AvailableBooks = new ObservableCollection<Book>();
            CheckedOutBooks = new ObservableCollection<Book>();
            OverdueFees = new ObservableCollection<OverdueFee>(); 
        }
        public async static Task GetAvailableBooks(MySqlConnection connection)
        {
            string query = @"SELECT title, author, isbn, quantity, id
                    FROM book
                    WHERE quantity > 0;";
            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    Book book;
                    while (reader.Read())
                    {
                        book = new Book();
                        book.Title = reader.IsDBNull(reader.GetOrdinal("title")) ? string.Empty : reader.GetString(reader.GetOrdinal("title"));
                        book.Author = reader.IsDBNull(reader.GetOrdinal("author")) ? string.Empty : reader.GetString(reader.GetOrdinal("author"));
                        book.ISBN = reader.IsDBNull(reader.GetOrdinal("isbn")) ? string.Empty : reader.GetString(reader.GetOrdinal("isbn"));
                        book.NumberOfCopies = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("quantity"));
                        book.ID = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));

                        AvailableBooks.Add(book);
                    }
                }
            }
            await connection.CloseAsync();

        }

        // This function was made to track the quantity of books so that we can know if we should display them to the available books or not
        public static void EditBook(Book book, MySqlConnection connection)
        {
            Book editBook = AvailableBooks.FirstOrDefault(b => b.Title == book.Title && b.Author == book.Author);
            
            if (editBook != null)
            {  
                editBook.Title = book.Title;
                editBook.Author = book.Author;
                editBook.ISBN = book.ISBN;
                editBook.NumberOfCopies = book.NumberOfCopies;
                if (editBook.NumberOfCopies < 1)
                {
                    AvailableBooks.Remove(editBook);
                }
            }
            else
            {
                if (book.NumberOfCopies > 0)
                {
                    AvailableBooks.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        ISBN = book.ISBN,
                        NumberOfCopies = book.NumberOfCopies
                    });
                }
            }
            
        }

        public async static Task CheckOutBook(Transaction transaction, MySqlConnection connection)
        {
            string query = @"INSERT INTO transaction (book_id, patron_id, checkout_date, due_date, return_date)
                            VALUES (@book_id, @patron_id, @checkout_date, @due_date, @return_date)"; //, @fine_amount , fine_amount

            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@book_id", transaction.Book.ID);
                cmd.Parameters.AddWithValue("@patron_id", transaction.Patron.ID);
                cmd.Parameters.AddWithValue("@checkout_date", transaction.CheckOutDate);
                cmd.Parameters.AddWithValue("@due_date", transaction.DueDate);
                cmd.Parameters.AddWithValue("@return_date", transaction.ReturnDate);
                //cmd.Parameters.AddWithValue("@fine_amount", transaction.FineAmount);

                await cmd.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
            await AddSubtractBookQuantity(transaction.Book, "subtract", connection);
        }

        public static bool IsBookCheckedOut(Patron patron, Book book, MySqlConnection connection)
        {
            string query = @"SELECT * 
                            FROM transaction
                            WHERE book_id = @book_id AND
                                    patron_id = @patron_id AND
                                    return_date <= @min_date";

            connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@book_id", book.ID);
                cmd.Parameters.AddWithValue("@patron_id", patron.ID);
                cmd.Parameters.AddWithValue("@min_date", DateTime.MinValue.Date);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            connection.Close();
            return false;
        }

        public async static Task AddSubtractBookQuantity(Book book, string operatorr, MySqlConnection connection)
        {
            string query = @"UPDATE book 
                            SET quantity = @quantity
                            WHERE id = @book_id";

            operatorr = operatorr.ToLower();
            if (operatorr == "add") book.NumberOfCopies++;
            else if (operatorr == "subtract") book.NumberOfCopies--;

            await connection.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@quantity", book.NumberOfCopies);
                cmd.Parameters.AddWithValue("@book_id", book.ID);

                await cmd.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
        }

        public async static Task GetCheckedOutBooks(Patron patron, MySqlConnection connection)
        {   
            string query = @"SELECT * 
                            FROM transaction
                            RIGHT JOIN book ON book.id = transaction.book_id
                            RIGHT JOIN patron ON @patron_id = transaction.patron_id
                            WHERE return_date <= @min_date;"; 
            List<Book> tempBooks = new List<Book>();
            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@patron_id", patron.ID);
                cmd.Parameters.AddWithValue("@min_date", DateTime.MinValue.Date);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    string patronName;
                    int patronID;
                    while (reader.Read())
                    {
                        patronName = Helper.GetSqlCellString(reader, "name", "lower");
                        patronID = Helper.GetSqlCellInt32(reader, "patron_id");

                        if (patron.FullName.ToLower().Equals(patronName) && patron.ID == patronID)
                        {
                            Book book = new Book();
                            book.Title = Helper.GetSqlCellString(reader, "title");
                            book.Author = Helper.GetSqlCellString(reader, "author");
                            book.ISBN = Helper.GetSqlCellString(reader, "isbn");
                            book.Genre = Helper.GetSqlCellString(reader, "genre");
                            book.Publisher = Helper.GetSqlCellString(reader, "publisher");
                            book.YearPublished = Helper.GetSqlCellInt32(reader, "published_year");
                            book.NumberOfCopies = Helper.GetSqlCellInt32(reader, "quantity");
                            book.ID = Helper.GetSqlCellInt32(reader, "book_id");

                            tempBooks.Add(book);
                            CheckedOutBooks.Add(book);
                            //connection.Close();

                        }
                    }
                }
            }
            await connection.CloseAsync();

            foreach (Book book in tempBooks)
            {
                (int overDueDays, double feeAmount) = Transaction.GetDaysFee(patron, book, connection);
                OverdueFee overdueFee = new OverdueFee(book.Title, overDueDays, feeAmount);
                OverdueFees.Add(overdueFee);
            }
        }

        public async static Task<ObservableCollection<Book>> FilterBooks(string searchKey, MySqlConnection connection)
        {
            return await BookManager.FilterTitleBooks(searchKey, connection);
        }

        public async static Task ReturnBook(Transaction transaction, Book selectedBook, MySqlConnection connection)
        {
            string query = @"UPDATE transaction
                            SET return_date = @return_date
                            WHERE book_id = @book_id AND
                                  patron_id = @patron_id";

            await connection.OpenAsync();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@return_date", DateTime.Now);
                command.Parameters.AddWithValue("@book_id", selectedBook.ID);
                command.Parameters.AddWithValue("@patron_id", transaction.Patron.ID);

                await command.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
        }

        public async static Task<int> GetBookQuantity(Book selectedBook, MySqlConnection connection)
        {
            string query = @"SELECT quantity FROM book WHERE id=@book_id;";
            int quantity = -1;
            await connection.OpenAsync();
            using (MySqlCommand cmd =  new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@book_id", selectedBook.ID);
                await cmd.ExecuteReaderAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        quantity = Helper.GetSqlCellInt32(reader, "quantity");
                    }
                }
            }
            await connection.CloseAsync();
            return quantity;
        }

    }

}
