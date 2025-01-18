using LMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class TransactionHistory
    {
        public int TransactionID { get; set; }
        public string PatronName { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        [JsonIgnore]
        public DateTime CheckOutDate { get; set; }
        [JsonIgnore]
        public DateTime DueDate { get; set; }
        [JsonIgnore]
        public DateTime ReturnDate { get; set; }
        public string DisplayCheckOutDate
        {
            get { return CheckOutDate.ToString("yyyy-MM-dd"); }
        }
        public string DisplayDueDate
        {
            get { return DueDate.ToString("yyyy-MM-dd"); }
        }
        public string DisplayReturnDate
        {
            get 
            { 
                if (ReturnDate != DateTime.MinValue)
                {
                    return ReturnDate.ToString("yyyy-MM-dd");
                }
                return "Not Returned";
            }
        }
        public TransactionHistory() { }
    }
}

//public Book Book { get; set; }
//public Patron Patron { get; set; }
//public DateTime CheckOutDate { get; set; }
//public DateTime DueDate { get; set; }
//public DateTime ReturnDate { get; set; }
////public float FineAmount { get; set; } 
//public OverdueFee OverdueFee { get; set; }

//Transaction ID(if applicable)
//Patron Details:
//Patron ID
//Patron Name
//Book Details:
//Book ID
//Book Title
//Book Author
//Transaction Details:
//Check - Out Date
//Due Date
//Return Date (if applicable)