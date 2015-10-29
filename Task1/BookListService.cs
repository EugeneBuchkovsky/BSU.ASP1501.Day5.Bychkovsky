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
        private readonly string path;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="fileName">File name (with extention)</param>
        public BookListService(string fileName)
        {
            if (fileName == null || fileName.Equals(String.Empty))
                throw new ArgumentNullException("File name not found!");
            this.path = String.Format(AppDomain.CurrentDomain.BaseDirectory + fileName); 
            bookList = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            ReadBooks();

            foreach (Book b in bookList)
            {
                if (b.Equals(book))
                    throw new ArgumentException("This is book already recorded!");
            }
            bookList.Add(book);
            DeleteFile();
            WriteBooks(bookList);
            bookList.Clear();
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            bookList.Clear();
            ReadBooks();
            foreach (Book Rbook in bookList)
            {
                if (!Rbook.Equals(book))
                    throw new ArgumentException("This book doesn't exist!");
            }
            bookList.Remove(book);

            DeleteFile();

            WriteBooks(bookList);
        }
        public void SortBooksByTag()
        {
            bookList.Clear();
            ReadBooks();

            bookList.Sort(Comparer<Book>.Default);
            DeleteFile();
            WriteBooks(bookList);
        }

        public void SortBooksByTag(IComparer<Book> compare)
        {
            ReadBooks();

            bookList.Sort(compare);

            DeleteFile();
            foreach (Book book in bookList)
                AddBook(book);
        }

        public Book FindBookByTag(Func<Book, bool> func)
        {
            if (func == null)
                throw new ArgumentNullException("Criterion is null!");

            ReadBooks();
            Book book = bookList.First(func);
            return book;
        }

        public override string ToString()
        {
            bookList.Clear();
            ReadBooks();
            if (bookList.Count == 0)
                return null;
            StringBuilder result = new StringBuilder();
            foreach (Book book in bookList)
                result.Append(String.Format("Book: {0}\nAuthor:{1}\nAmount of pages: {2}\nGenre :{3}\n\n", book.Title, book.Author, book.PagesAmount, book.Genre));
            return result.ToString();
        }

        private void ReadBooks()
        {
            try
            {
                BinaryReader read = new BinaryReader(File.Open(path, FileMode.OpenOrCreate));
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

        private void WriteBooks(List<Book> books)
        {
            //ReadBooks();
            try
            {
                BinaryWriter write = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                foreach(Book b in bookList)
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

        private void DeleteFile()
        {
            FileInfo FI = new FileInfo(path);
            FI.Delete();
        } 
    }
}
