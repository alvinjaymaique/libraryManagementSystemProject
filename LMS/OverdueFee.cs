using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class OverdueFee
    {
        //public string PatronName { get; set; }
        //public Book Book { get; set; }
        public string BookTitle { get; set; }
        public int OverdueDays { get; set; }
        public double FeeAmount { get; set; }
        public OverdueFee()
        {

        }
        public OverdueFee(string bookTitle, int overdueDays, double feeAmount)
        {
            //Book = book;
            BookTitle = bookTitle;
            OverdueDays = overdueDays;
            FeeAmount = feeAmount;
        }
    }
}
