using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Task1;

namespace Task1.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            BookListService bl = new BookListService("booklist.txt");

            bl.AddBook(new Book("Braun","Inferno",500,"Scince. Adventure"));
            bl.AddBook(new Book("Zamyatin","We",100,"Dystopia"));
            bl.AddBook(new Book("Tolkien", "The Lord Of The Rings", 1000, "Fantasy"));

            System.Console.WriteLine(bl.ToString());
            //bl.AddBook(new Book("Zamyatin", "Hronic", 134, "Fantasy"));
            System.Console.WriteLine("__________________");
            bl.SortBooksByTag();
            System.Console.WriteLine(bl.ToString());

            Book bok = bl.FindBookByTag(x => x.PagesAmount == 100);
            System.Console.WriteLine(bok.ToString());

            System.Console.ReadLine();
        }
    }
}
