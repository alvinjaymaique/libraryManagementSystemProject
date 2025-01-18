using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class PatronActivity
    {
        public string PatronName { get; set; }
        public string MemberID { get; set; }
        public string MemberType {  get; set; }
        public string Status {  get; set; }
        public int NumBooksCheckedOut { get; set; }
        public int NumOverdueBooks { get; set; }
        public double Fines {  get; set; }
        public PatronActivity() { }
    }
}
