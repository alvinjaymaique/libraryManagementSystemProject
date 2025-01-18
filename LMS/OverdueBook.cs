using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LMS
{
    public class OverdueBook
    {
        public string BookTitle {  get; set; }
        public int BookID {  get; set; }
        [JsonIgnore]
        public DateTime CheckOutDate {  get; set; }
        [JsonIgnore]
        public DateTime DueDate { get; set; }
        public string PatronName {  get; set; }
        public int PatronID { get; set; }
        public string MemberID { get; set; }
        public string Phone { get; set; }
        public string Email {  get; set; }
        public int OverdueDays { get; set; }
        public double FineAmount { get; set; }
        public OverdueBook()
        {;
        }
        [JsonIgnore]
        public string DisplayDueDate
        {
            get
            {
                return DueDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
