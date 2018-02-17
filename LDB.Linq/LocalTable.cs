using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq
{
    public class LocalTable
    {
        public void SetProperty<T>(string name, ref T field, T value)
        {
            field = value;
            
        }
    }
}
