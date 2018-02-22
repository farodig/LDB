using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq.Converters
{
    internal class BinConverter : IConverter
    {
        public T Deserialize<T>(string file) where T : new()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    return new T();
                } else
                {
                    return (T)formatter.Deserialize(fs);
                }
                
            }
        }

        public void Serialize<T>(string file, T data) where T : new()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
            }
        }
    }
}
