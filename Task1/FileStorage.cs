using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    public class FileStorage : IRepository
    {
        public void SaveTo(string FileName, List<Book> bookList)
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

        public List<Book> ReadFrom(string FileName)
        {
            if (FileName.Equals(string.Empty) || FileName == null)
                throw new ArgumentNullException("File name not found!");
            //bookList.Clear();
            try
            {
                List<Book> bookList = new List<Book>();
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
                return bookList;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidDataException("File not found!");
            }
        }

        private void DeleteFile(string path)
        {
            FileInfo FI = new FileInfo(path);
            if (FI.Exists)
                FI.Delete();
        }

    }
}
