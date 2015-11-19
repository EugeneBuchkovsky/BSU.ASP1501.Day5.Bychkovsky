using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IRepository
    {
        void SaveTo(string path, List<Book> books);

        List<Book> ReadFrom(string path);
    }
}
