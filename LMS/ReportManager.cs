using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMS
{
    public static class ReportManager
    {
        //public static ObservableCollection<Patron> OverduedPatrons;
        public static ObservableCollection<OverdueBook> OverdueBooks { get; set; }
        public static int OverdueBooksPage { get; set; }
        public static ObservableCollection<CheckedOutBook> CheckedOutBooks { get; set; }
        public static int CheckedOutBooksPage {  get; set; }
        public static ObservableCollection<TransactionHistory> TransactionHistory { get; set; }
        public static int TransactionHistoryPage { get; set; }
        public static ObservableCollection<PatronActivity> PatronActivities { get; set; }
        public static int PatronActivitiesPage { get; set; }


        static ReportManager()
        {
            OverdueBooks = new ObservableCollection<OverdueBook>();
            OverdueBooksPage = 0;

            CheckedOutBooks = new ObservableCollection<CheckedOutBook>();
            CheckedOutBooksPage = 0;

            TransactionHistory = new ObservableCollection<TransactionHistory>();
            TransactionHistoryPage = 0;

            PatronActivities = new ObservableCollection<PatronActivity>();
            PatronActivitiesPage = 0;
        }
        public async static Task GetOverdueBooks(MySqlConnection connection)
        {
            string query = @"SELECT t.book_id, b.title AS book_title, t.patron_id, p.name AS patron_name, p.membership_id, p.phone, p.email, t.due_date
                            FROM transaction t
                            JOIN book b ON b.id = t.book_id
                            JOIN patron p ON p.id = t.patron_id
                            WHERE return_date = @date_min AND
                                  due_date < @date_today
                                    LIMIT 15 OFFSET @page_start;";
            ObservableCollection<OverdueBook> overdueBooks = new ObservableCollection<OverdueBook>();
            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@date_min", DateTime.MinValue);
                cmd.Parameters.AddWithValue("@date_today", DateTime.Today);
                cmd.Parameters.AddWithValue("@page_start", OverdueBooksPage * 15);
                
                await cmd.ExecuteNonQueryAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        OverdueBooks.Clear();
                        while (reader.Read())
                        {
                            var overdueBook = new OverdueBook
                            {
                                BookID = Helper.GetSqlCellInt32(reader, "book_id"),
                                BookTitle = Helper.GetSqlCellString(reader, "book_title"),
                                PatronID = Helper.GetSqlCellInt32(reader, "patron_id"),
                                PatronName = Helper.GetSqlCellString(reader, "patron_name"),
                                MemberID = Helper.GetSqlCellString(reader, "membership_id"),
                                Phone = Helper.GetSqlCellString(reader, "phone"),
                                Email = Helper.GetSqlCellString(reader, "email"),
                                DueDate = Helper.GetSqlCellDate(reader, "due_date")
                            };
                            overdueBooks.Add(overdueBook);
                        }

                    }
                    else
                    {
                        OverdueBooksPage--;
                        await connection.CloseAsync();
                        return;
                    }   


                }
            }
            await connection.CloseAsync();
            // Calculate fines for each overdue book in-memory
            foreach (var overdueBook in overdueBooks)
            {
                var patron = new Patron(overdueBook.PatronID);
                var book = new Book(overdueBook.BookID);
                if (overdueBook != null) (overdueBook.OverdueDays, overdueBook.FineAmount) = Transaction.GetDaysFee(patron, book, connection);
            }

            foreach (var overdueBook in overdueBooks)
            {
                OverdueBooks.Add(overdueBook);
            }
        }


        public async static Task GetCheckoutBooks(MySqlConnection connection)
        {
            string query = @"SELECT b.title, b.author, t.due_date
                                FROM transaction t
                                JOIN book b ON b.id=t.book_id
                                WHERE t.return_date = @return_date
                                LIMIT 15 OFFSET @page_start;";
            await connection.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@return_date", DateTime.MinValue);
                cmd.Parameters.AddWithValue("@page_start", CheckedOutBooksPage * 15);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        CheckedOutBooks.Clear();
                        while (reader.Read())
                        {
                            var checkedOutBooks = new CheckedOutBook
                            {
                                BookTitle = Helper.GetSqlCellString(reader, "title"),
                                Author = Helper.GetSqlCellString(reader, "author"),
                                DueDate = Helper.GetSqlCellDate(reader, "due_date")
                            };
                            CheckedOutBooks.Add(checkedOutBooks);
                        }
                    }
                    else
                    {
                        CheckedOutBooksPage--;
                    }
                }
            }
            await connection.CloseAsync();

        }

        public async static Task GetTransactionHistory(DateTime from_date, DateTime to_date, MySqlConnection connection)
        {
            string query = @"SELECT t.transaction_id, t.book_id, b.author, b.title AS book_title, t.patron_id, p.name AS patron_name, t.checkout_date, t.due_date, t.return_date
                            FROM transaction t
                            JOIN book b ON b.id = t.book_id
                            JOIN patron p ON p.id = t.patron_id
                            WHERE t.checkout_date BETWEEN @from_date AND @to_date
                            LIMIT 15 OFFSET @page_start;";

            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@from_date", from_date.Date);
                cmd.Parameters.AddWithValue("@to_date", to_date.Date);
                cmd.Parameters.AddWithValue("@page_start", TransactionHistoryPage * 15);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        TransactionHistory.Clear();
                        while (reader.Read())
                        {
                            var transactHist = new TransactionHistory
                            {
                                TransactionID = Helper.GetSqlCellInt32(reader, "transaction_id"),
                                CheckOutDate = Helper.GetSqlCellDate(reader, "checkout_date"),
                                DueDate = Helper.GetSqlCellDate(reader, "due_date"),
                                ReturnDate = Helper.GetSqlCellDate(reader, "return_date"),
                                PatronName = Helper.GetSqlCellString(reader, "patron_name"),
                                BookTitle = Helper.GetSqlCellString(reader, "book_title"),
                                BookAuthor = Helper.GetSqlCellString(reader, "author")              
                            };
                            TransactionHistory.Add(transactHist);
                        }
                    }
                    else
                    {
                        TransactionHistoryPage--;
                    }
                }
            }
            await connection.CloseAsync();
        }

        public async static Task GetPatronActivities(MySqlConnection connection)
        {
            double charge = 0.50;
            int limit = 15;
            string query = @"SELECT p.id AS patron_id, p.name AS patron_name, p.membership_id, p.membership_type, p.status,
                            COUNT(t.transaction_id) AS num_books_checked_out,
                            SUM(CASE 
                                    WHEN t.return_date = @min_date AND t.due_date < CURDATE() THEN 1
                                    ELSE 0
                                END) AS num_overdue_books,
                            SUM(CASE 
                                    WHEN t.return_date = @min_date AND t.due_date < CURDATE() THEN DATEDIFF(CURDATE(), t.due_date) * @charge
                                    ELSE 0
                                END) AS total_fines 
                            FROM patron p
                            LEFT JOIN transaction t ON t.patron_id = p.id
                            GROUP BY p.id, p.name, p.membership_id, p.membership_type, p.status
                            LIMIT @limit OFFSET @start_page;";
            await connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@min_date", DateTime.MinValue.Date);
                cmd.Parameters.AddWithValue("@charge", charge);
                cmd.Parameters.AddWithValue("@limit", limit);
                cmd.Parameters.AddWithValue("@start_page", PatronActivitiesPage * 15); 
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        PatronActivities.Clear();
                        while (reader.Read())
                        {
                            PatronActivity patronActivities = new PatronActivity
                            {
                                PatronName = Helper.GetSqlCellString(reader, "patron_name"),
                                MemberID = Helper.GetSqlCellString(reader, "membership_id"),
                                MemberType = Helper.GetSqlCellString(reader, "membership_type"),
                                Status = Helper.GetSqlCellString(reader, "status"),
                                NumBooksCheckedOut = Helper.GetSqlCellInt32(reader, "num_books_checked_out"),
                                NumOverdueBooks = Helper.GetSqlCellInt32(reader, "num_overdue_books"),
                                Fines = Helper.GetSqlCellInt32(reader, "total_fines")
                            };
                            PatronActivities.Add(patronActivities);
                        }
                    }
                    else
                    {
                        PatronActivitiesPage--;
                    }
                }
            }
            await connection.CloseAsync();
        }

        public async static Task ExportCollectionAsync<T>(IEnumerable<T> collection, string fileName, string collectionName)
        {
            if (collection == null || !collection.Any())
            {
                MessageBox.Show($"No {collectionName} data to export.", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await ExportUtility.ExportCollectionToCSVAsync(collection, fileName);
                MessageBox.Show($"{collectionName} exported successfully!", "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting {collectionName}: {ex.Message}", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}