using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;


namespace Task1
{
    class LinqToXml : IRepository
    {
        /// <summary>
        /// Add list of books to xml-file with Linq2xml
        /// </summary>
        /// <param name="name">Name of xml-file.</param>
        /// <param name="bookList"></param>
        public void WriteTo(string name, List<Book> bookList)
        {
            if (name.Equals(string.Empty) || name == null)
                throw new ArgumentNullException("File name not found!");
            string path = String.Format(AppDomain.CurrentDomain.BaseDirectory + name);

            //Creatу xml document
            XDocument file = new XDocument();

            //create root element
            XElement books = new XElement("ListOfBooks");

            #region add book to xml
            foreach (Book book in bookList)
            {
                books.Add(new XElement("book",
                    new XElement("author", book.Title),
                    new XElement("title", book.Author),
                    new XElement("genre", book.Genre),
                    new XElement("pagesAmount", book.PagesAmount)));
            }
            #endregion

            file.Add(books);
            file.Save(path);
        }

        /// <summary>
        /// Read list of books from xml-file with Linq2Xml.
        /// </summary>
        /// <param name="path">Path to xml-file.</param>
        /// <returns></returns>
        public List<Book> WtiteTo(string path)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                XDocument document = XDocument.Load(path);
                var books = document.Elements("ListOfBooks").Elements("book");

                foreach (XElement e in books)
                {
                    Book b = new Book(e.Element("author").Value,
                        e.Element("title").Value,
                        int.Parse(e.Element("pagesAmount").Value),
                        e.Element("genre").Value);

                    bookList.Add(b);
                }
                return bookList;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }

    }
}
