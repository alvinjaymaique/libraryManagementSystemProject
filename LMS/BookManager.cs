using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace LMS
{
    public static class BookManager
    {
        public static ObservableCollection<Book> Books { get; set; }
        public static int Page { get; set; }
        //public static bool NoNextPage {  get; set; }
        
        static BookManager()
        {
            // Initialize the collection
            Books = new ObservableCollection<Book>();
            Page = 0;
            //NoNextPage = false;
        }

        public async static Task LoadFromDatabase(MySqlConnection connection)
        { 
            string query = "SELECT title, author, isbn, genre, publisher, published_year, quantity, id FROM book LIMIT 15 OFFSET @page_start;";
            await connection.OpenAsync();
            try
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@page_start", Page * 15);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            Books.Clear();
                            while (reader.Read())
                            {
                                Book book = new Book
                                {
                                    Title = reader.IsDBNull(reader.GetOrdinal("title")) ? string.Empty : reader.GetString(reader.GetOrdinal("title")),
                                    Author = reader.IsDBNull(reader.GetOrdinal("author")) ? string.Empty : reader.GetString(reader.GetOrdinal("author")),
                                    ISBN = reader.IsDBNull(reader.GetOrdinal("isbn")) ? string.Empty : reader.GetString(reader.GetOrdinal("isbn")),
                                    Genre = reader.IsDBNull(reader.GetOrdinal("genre")) ? string.Empty : reader.GetString(reader.GetOrdinal("genre")),
                                    Publisher = reader.IsDBNull(reader.GetOrdinal("publisher")) ? string.Empty : reader.GetString(reader.GetOrdinal("publisher")),
                                    YearPublished = reader.IsDBNull(reader.GetOrdinal("published_year")) ? 0 : reader.GetInt32(reader.GetOrdinal("published_year")),
                                    NumberOfCopies = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("quantity")),
                                    ID = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"))
                                };
                                Books.Add(book);
                            }
                        }
                        else
                        {
                            Page--;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await connection.CloseAsync();
        }

        public async static Task AddBook(Book book, MySqlConnection connection)
        {
            string query = "SELECT quantity FROM book WHERE LOWER(title)=LOWER(@newBookTitle) AND LOWER(author)=LOWER(@newBookAuthor);";
            bool isBookExisted;
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@newBookTitle", book.Title);
                cmd.Parameters.AddWithValue("@newBookAuthor", book.Author);
                var result = cmd.ExecuteScalar();
                isBookExisted = result != null;

                if (isBookExisted == true)
                {
                    int currentQuantity = Convert.ToInt32(result.ToString());
                    int updatedQuantity = book.NumberOfCopies + currentQuantity;
                    query = "UPDATE book SET quantity = @newQuantity WHERE LOWER(title)=LOWER(@newBookTitle) AND LOWER(author)=LOWER(@newBookAuthor);";

                    using (var updateCmd = new MySqlCommand(query, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@newQuantity", updatedQuantity);
                        updateCmd.Parameters.AddWithValue("@newBookTitle", book.Title);
                        updateCmd.Parameters.AddWithValue("@newBookAuthor", book.Author);

                        await updateCmd.ExecuteNonQueryAsync();

                        // Update the Books in BookManager to Reflect the changes into the DataGridView
                        var existingBook = Books.FirstOrDefault(b => 
                            b.Title.ToLower() == book.Title.ToLower() && 
                            b.Author.ToLower() == book.Author.ToLower());

                        existingBook.NumberOfCopies = existingBook != null ? updatedQuantity : existingBook.NumberOfCopies;
                        MessageBox.Show("Quantity is updated.", "Result");
                    }
                    
                }
                else if (isBookExisted == false) 
                {
                    query = "INSERT INTO book (title, author, isbn, genre, publisher, published_year, quantity)" +
                        "VALUES (@title, @author, @isbn, @genre, @publisher, @published_year, @quantity)";
                    using (var addCmd = new MySqlCommand(query, connection))
                    {
                        addCmd.Parameters.AddWithValue("@title", book.Title);
                        addCmd.Parameters.AddWithValue("@author", book.Author);
                        addCmd.Parameters.AddWithValue("@isbn", book.ISBN);
                        addCmd.Parameters.AddWithValue("@genre", book.Genre);
                        addCmd.Parameters.AddWithValue("@publisher", book.Publisher);
                        addCmd.Parameters.AddWithValue("@published_year", book.YearPublished);
                        addCmd.Parameters.AddWithValue("@quantity", book.NumberOfCopies);

                        //addCmd.ExecuteNonQuery();
                        await addCmd.ExecuteNonQueryAsync();
                        Books.Add(book);
                    }
                }
            }          
            connection.Close();
        }

        public async static Task DeleteBook(Book book, MySqlConnection connection)
        {
            string query = "DELETE FROM book " +
                "WHERE title = @title AND " +
                "author = @author AND " +
                "isbn = @isbn AND " +
                "genre = @genre AND " +
                "publisher = @publisher AND " +
                "published_year = @published_year AND " +
                "quantity = @quantity;";
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@isbn", book.ISBN);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@published_year", book.YearPublished);
                cmd.Parameters.AddWithValue("@quantity", book.NumberOfCopies);

                //cmd.ExecuteNonQuery();
                await cmd.ExecuteNonQueryAsync();
                Books.Remove(book);
            }
            connection.Close();
        }

        public async static Task EditBook(Book book, MySqlConnection connection)
        {
            string query = "UPDATE book " +
                "SET title = @title, " +
                "author = @author, " +
                "isbn = @isbn, " +
                "genre = @genre, " +
                "publisher = @publisher, " +
                "published_year = @published_year, " +
                "quantity = @quantity " +
                "WHERE id = @id;";

            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@isbn", book.ISBN);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@published_year", book.YearPublished);
                cmd.Parameters.AddWithValue("@quantity", book.NumberOfCopies);
                cmd.Parameters.AddWithValue("@id", book.ID);

                await cmd.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();

            // Find the book in the list by ID and update it
            var existingBook = Books.FirstOrDefault(b => b.ID == book.ID);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.Genre = book.Genre;
                existingBook.Publisher = book.Publisher;
                existingBook.YearPublished = book.YearPublished;
                existingBook.NumberOfCopies = book.NumberOfCopies;

                // Update it in the TransactionManager as well
                TransactionManager.EditBook(existingBook, connection);
            }
        }


        //public async static Task EditBook(Book book, MySqlConnection connection)
        //{
        //    string query = "UPDATE book " +
        //        "SET title = @title, " +
        //        "author = @author, " +
        //        "isbn = @isbn, " +
        //        "genre = @genre, " +
        //        "publisher = @publisher, " +
        //        "published_year = @published_year, " +
        //        "quantity = @quantity " +
        //        "WHERE id = @id;";

        //    await connection.OpenAsync();
        //    using (var cmd = new MySqlCommand(query, connection))
        //    {
        //        cmd.Parameters.AddWithValue("@title", book.Title);
        //        cmd.Parameters.AddWithValue("@author", book.Author);
        //        cmd.Parameters.AddWithValue("@isbn", book.ISBN);
        //        cmd.Parameters.AddWithValue("@genre", book.Genre);
        //        cmd.Parameters.AddWithValue("@publisher", book.Publisher);
        //        cmd.Parameters.AddWithValue("@published_year", book.YearPublished);
        //        cmd.Parameters.AddWithValue("@quantity", book.NumberOfCopies);
        //        cmd.Parameters.AddWithValue("@id", book.ID);

        //        await cmd.ExecuteNonQueryAsync();
        //        //cmd.ExecuteNonQuery();
        //        int index = Books.IndexOf(book);
        //        Books[index] = book;
        //        TransactionManager.EditBook(Books[index], connection);
        //    }
        //    await connection.CloseAsync();
        //}

        public async static Task<ObservableCollection<Book>> FilterBooks(string searchKey, MySqlConnection connection)
        {
            string query = "SELECT * FROM book " +
                   "WHERE title LIKE @searchKey OR " +
                   "isbn LIKE @searchKey OR " +
                   "genre LIKE @searchKey OR " +
                   "publisher LIKE @searchKey OR " +
                   "published_year LIKE @searchKey OR " +
                   "quantity LIKE @searchKey OR " +
                   "author LIKE @searchKey " +
                   "LIMIT 15 OFFSET @page_start;";

            ObservableCollection<Book> filteredBooks = new ObservableCollection<Book>();
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@searchKey", "%" + searchKey + "%");
                cmd.Parameters.AddWithValue("@page_start", Page * 15);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                Title = reader["title"].ToString(),
                                Author = reader["author"].ToString(),
                                ISBN = reader["isbn"].ToString(),
                                Genre = reader["genre"].ToString(),
                                Publisher = reader["publisher"].ToString(),
                                YearPublished = Convert.ToInt32(reader["published_year"]),
                                NumberOfCopies = Convert.ToInt32(reader["quantity"])
                            };
                            filteredBooks.Add(book);
                        }
                    }
                    else
                    {
                        Page--;
                    }
                    
                }
            }
            connection.Close();
            return filteredBooks;
        }

        public async static Task<ObservableCollection<Book>> FilterTitleBooks(string searchKey, MySqlConnection connection)
        {
            string query = "SELECT * FROM book " +
                   "WHERE title LIKE @searchKey;";

            ObservableCollection<Book> filteredBooks = new ObservableCollection<Book>();
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@searchKey", "%" + searchKey + "%");

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        {
                            Title = reader["title"].ToString()
                        };
                        filteredBooks.Add(book);
                    }
                }
            }
            connection.Close();
            return filteredBooks;
        }

    }
}
