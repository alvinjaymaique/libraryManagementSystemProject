using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class CheckedOutBook
    {
        public string BookTitle {  get; set; }
        public string Author { get; set; }
        [JsonIgnore]
        public DateTime DueDate {  get; set; } 
        public CheckedOutBook() { }
        public string DisplayDate
        {
            get
            {
                return DueDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
