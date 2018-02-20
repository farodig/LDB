using LDB.Linq.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq.Converters
{
    internal class CsvConverter : IConverter
    {
        public T Deserialize<T>(string file)
        {
            CsvSerializer serializer = new CsvSerializer();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                return serializer.Deserialize<T>(fs);
            }
        }

        public void Serialize<T>(string file, T data)
        {
            CsvSerializer serializer = new CsvSerializer();
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                serializer.Serialize<T>(fs, data);
            }
        }
    }
}
