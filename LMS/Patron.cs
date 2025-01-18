using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class Patron
    {
        public string FullName { get; set; }
        public string MembershipID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MembershipType { get; set; }
        public string Status { get; set; }
        public int ID { get; set; }
        public string DisplayDate
        {
            get
            {
                return DateOfBirth.ToString("yyyy-MM-dd");
            }
        }
        public Patron() { }
        public Patron(int id)
        {
            ID = id;
        }
        public Patron(string fullName, string membershipID, string email, string phoneNumber, string address, DateTime dateOfBirth, string membershipType, string status)
        {
            FullName = fullName;
            MembershipID = membershipID;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DateOfBirth = dateOfBirth;
            MembershipType = membershipType;
            Status = status;
        }
    }
}
