using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LMS
{
    public class Transaction
    {
        public Book Book {  get; set; }
        public Patron Patron { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate {  get; set; }
        public DateTime ReturnDate { get; set; }
        //public float FineAmount { get; set; } 
        public OverdueFee OverdueFee { get; set; }
        public Transaction() { }
        public Transaction(Book book, Patron patron, MySqlConnection connection) : this(book, patron, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue)
        {
            var (checkOutDate, dueDate) = GetBookCheckDueDates(patron, book, connection);
            CheckOutDate = checkOutDate;
            DueDate = dueDate;
            ReturnDate = DateTime.Now;
        }
        public Transaction(Book book, Patron patron, DateTime checkOutDate, DateTime dueDate, DateTime returnDate) //, float fineAmount
        {
            this.Book = book;
            this.Patron = patron;
            this.CheckOutDate = checkOutDate;
            this.DueDate = dueDate;
            this.ReturnDate = returnDate;
            //this.FineAmount = fineAmount;
        }

        // Calculate overdue fee
        public static (int,double) GetDaysFee(Patron patron, Book selectedBook, MySqlConnection connection, string closeConnection = "close")
        {
            // Example logic for overdue fee calculation (you can customize this)
            //var (_, dueDate) = null;
            var dueDate = DateTime.MinValue;
            closeConnection = closeConnection.ToLower();
            if (closeConnection == "close")
            {
               (_, dueDate) = GetBookCheckDueDates(patron, selectedBook, connection); //?? DateTime.Now;
            }
            else if (closeConnection == "open")
            {
                (_, dueDate) = GetBookCheckDueDates(patron, selectedBook, connection, "open");
                //MessageBox.Show($"Inside closeConnection == \"open\" {dueDate} this is due date");
            }
            
            if (DateTime.Now > dueDate)
            {
                //MessageBox.Show($"{DateTime.Now} now. {dueDate} due date");
                int overdueDays = (DateTime.Now - dueDate).Days;
                return (overdueDays, (double)overdueDays * 0.50f);  // Charge 50 cents per overdue day
            }
            return (0, 0);
        }

        //public static int CalculateOverdueDays()

        public static (DateTime, DateTime) GetBookCheckDueDates(Patron patron, Book selectedBook, MySqlConnection connection, string closeConnection = "close")
        {
            string query = @"SELECT checkout_date, due_date FROM transaction
                            WHERE book_id = @book_id AND
                                  patron_id = @patron_id";
            DateTime dueDate = DateTime.MinValue;
            DateTime checkoutDate = DateTime.MinValue;
            if (connection.State != ConnectionState.Open) connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@book_id", selectedBook.ID);
                command.Parameters.AddWithValue("@patron_id", patron.ID);
  
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dueDate = Helper.GetSqlCellDate(reader, "due_date");
                        checkoutDate = Helper.GetSqlCellDate(reader, "checkout_date");
                    }
                } 
                
            }
            if (closeConnection == "close") connection.Close();
            return (checkoutDate, dueDate);
        }

    }
}
