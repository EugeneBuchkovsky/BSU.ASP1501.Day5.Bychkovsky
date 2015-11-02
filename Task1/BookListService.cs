using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    public class BookListService : IBookListService
    {
        /// <summary>
        /// Temporary storage of books
        /// </summary>
        private List<Book> bookList;

        /// <summary>
        /// Path to the file on the computer.
        /// </summary>
        //private readonly string path;

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            foreach (Book b in bookList)
            {
                if (b.Equals(book))
                    throw new InvalidDataException("This is book already recorded!");
            }
            bookList.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            foreach (Book b in bookList)
            {
                if (!b.Equals(book))
                    throw new InvalidDataException("This is book isn't founds!");
            }
            bookList.Remove(book);
        }

        public void SortBooksByTag()
        {
            bookList.Sort(Comparer<Book>.Default);
        }

        public void SortBooksByTag(IComparer<Book> compare)
        {
            bookList.Sort(compare);
        }

        public Book FindBookByTag(Func<Book, bool> func)
        {
            if (func == null)
                throw new ArgumentNullException("Criterion is null!");

            Book book = bookList.First(func);
            return book;
        }

        public void ReadFromFile(string FileName)
        {
            if (FileName.Equals(string.Empty) || FileName == null)
                throw new ArgumentNullException("File name not found!");
            bookList.Clear();
            try
            { 
                BinaryReader read = new BinaryReader(File.Open(FileName, FileMode.OpenOrCreate));
                while (read.PeekChar() > -1)
                {
                    string author = read.ReadString();
                    string title = read.ReadString();
                    int PA = read.ReadInt32();
                    string genre = read.ReadString();
                    bookList.Add(new Book(author, title, PA, genre));
                }
                read.Close();
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("File not found!");
            }
        }

        public void WriteToFile(string FileName)
        {
            if (FileName.Equals(string.Empty) || FileName == null)
                throw new ArgumentNullException("File name not found!");
            string path = String.Format(AppDomain.CurrentDomain.BaseDirectory + FileName);
            DeleteFile(path);
            try
            {
                BinaryWriter write = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                foreach (Book b in bookList)
                {
                    write.Write(b.Author);
                    write.Write(b.Title);
                    write.Write(b.PagesAmount);
                    write.Write(b.Genre);
                }
                write.Close();
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException();
            }
        }

        public override string ToString()
        {
            if (bookList.Count == 0)
                return null;
            StringBuilder result = new StringBuilder();
            foreach (Book book in bookList)
                result.Append(String.Format("Book: {0}\nAuthor:{1}\nAmount of pages: {2}\nGenre :{3}\n\n", book.Title, book.Author, book.PagesAmount, book.Genre));
            return result.ToString();
        }

        private void DeleteFile(string path)
        {
            FileInfo FI = new FileInfo(path);
            if (FI.Exists)
                FI.Delete();
        }
    }
}
