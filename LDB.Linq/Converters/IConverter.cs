using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq.Converters
{
    public interface IConverter
    {
        void Serialize<T>(string file, T data);

        T Deserialize<T>(string file);
    }
}
