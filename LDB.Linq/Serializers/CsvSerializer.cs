using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq.Serializers
{
    public class CsvSerializer
    {
        public T Deserialize<T>(FileStream stream)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string text)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(FileStream fs, T data)
        {
            throw new NotImplementedException();
        }
    }
}
