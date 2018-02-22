using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq.Converters
{
    public interface IConverter
    {
        void Serialize<T>(string file, T data) where T : new();

        T Deserialize<T>(string file) where T : new();
    }
}
