using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Task1
{
    class XMLStorage : IRepository
    {
        /// <summary>
        /// Add book list to xml file.
        /// </summary>
        /// <param name="name">Name of xml-file.</param>
        /// <param name="bookList"></param>
        public void SaveTo(string name, List<Book> bookList)
        {
            if (name.Equals(string.Empty) || name == null)
                throw new ArgumentNullException("File name not found!");
            string path = String.Format(AppDomain.CurrentDomain.BaseDirectory + name);
            try
            {
                using (var writer = XmlWriter.Create(path))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("ListOfBooks");
                    foreach (Book book in bookList)
                    {
                        writer.WriteStartElement("book");

                        #region Write Book
                        writer.WriteElementString("author", book.Author);
                        writer.WriteElementString("title", book.Title);
                        writer.WriteElementString("genre", book.Genre);

                        writer.WriteStartElement("pagesAmount");
                        writer.WriteValue(book.PagesAmount);
                        writer.WriteEndElement();
                        #endregion

                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (XmlException)
            {
                throw new InvalidDataException("Invalid xml document");
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }

        /// <summary>
        /// Reed list of books from xml-file.
        /// </summary>
        /// <param name="path">Path to mxl-file.</param>
        /// <returns></returns>
        public List<Book> ReadFrom(string path)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                using (var reader = XmlReader.Create(path))
                {
                    while (reader.Read())
                    {
                        string author, title, genre;
                        int pa;

                        #region read books
                        if (!reader.ReadToFollowing("author"))
                            break;
                        else
                        {
                            author = reader.ReadElementContentAsString();
                            reader.ReadToFollowing("title");
                            title = reader.ReadElementContentAsString();
                            reader.ReadToFollowing("genre");
                            genre = reader.ReadElementContentAsString();
                            reader.ReadToFollowing("pafesAmount");
                            pa = reader.ReadElementContentAsInt();
                            bookList.Add(new Book(author, title, pa, genre));
                        }
                        #endregion
                    }
                }
                return bookList;
            }
            catch (XmlException)
            {
                throw new InvalidDataException("Invalid xml document");
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("Error while saving");
            }
        }
    }
}
