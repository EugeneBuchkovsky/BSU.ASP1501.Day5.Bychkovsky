using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IBookListService
    {
        /// <summary>
        /// Add book to the list of books.
        /// </summary>
        /// <param name="book"></param>
        void AddBook(Book book);

        /// <summary>
        /// Remove book from the list of books.
        /// </summary>
        /// <param name="book"></param>
        void RemoveBook(Book book);

        /// <summary>
        /// Find book to the list books by tag.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Book FindBookByTag(Func<Book,bool> func);

        /// <summary>
        /// Sort books by tags.
        /// </summary>
        void SortBooksByTag();
    }
}
