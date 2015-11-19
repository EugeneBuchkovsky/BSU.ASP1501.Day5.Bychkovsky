using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task1
{
    [Serializable]
    public class Book : IEquatable<Book> , IComparable<Book>
    {
        private int pagesAmount;

        public string Author { get; set; }

        public string Title { get; set; }

        public int PagesAmount
        {
            get { return pagesAmount; }
            set
            {
                  if (value < 1)
                      throw new ArgumentException();
                  pagesAmount = value;
            }
        }

        public string Genre { get; set; }

        public Book(string author, string title, int PA, string genre)
        {
            this.Author = author;
            this.Title = title;
            this.PagesAmount = PA;
            this.Genre = genre;
        }

        public int CompareTo(Book other) 
        {
            if (other == null)
                return 1;

            Book book = other as Book;
            if (book != null)
                return this.Author.CompareTo(book.Author);
            else
                throw new ArgumentException("Object is not Book");
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
                return Equals((Book)obj);
            return false;
        }

        public bool Equals(Book other)
        {
            if (other == null)
                throw new NullReferenceException();
            if (this.Author == other.Author && this.Title == other.Title)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return PagesAmount+Title.Length;
        }

        public override string ToString()
        {
            return String.Format("Book: {0}\nAuthor: {1}\nAmount of pages: {2}\nGenre: {3}\n\n",this.Title,this.Author, this.PagesAmount, this.Genre);
        }
    }
}
