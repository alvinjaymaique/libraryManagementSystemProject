using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int YearPublished { get; set; }
        public int NumberOfCopies { get; set; }
        public int ID { get; set; }
        public Book() { }
        public Book(int id)
        {
            ID = id;
        }
        public Book(string title, string author, string isbn, string genre, string publisher, int yearPublished, int numberOfCopies, int id)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Genre = genre;
            Publisher = publisher;
            YearPublished = yearPublished;
            NumberOfCopies = numberOfCopies;
            ID = id;
        }


        //public override string ToString()
        //{
        //    return Title;
        //}
    }
}
