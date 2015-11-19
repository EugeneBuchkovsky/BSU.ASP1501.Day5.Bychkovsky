using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task1
{
    public static class Serializator
    {

        public static void Serialize(string name , BookListService bs)
        {
            if (name.Equals(string.Empty) || name == null)
                throw new ArgumentNullException("File name not found!");
            string path = String.Format(AppDomain.CurrentDomain.BaseDirectory + name);

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, bs);
            }
        }

        public static BookListService DeSerialize(string path)
        {
            BookListService newBLS;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                newBLS = (BookListService)formatter.Deserialize(fs);
            }
            return newBLS;
        }
    }
}
