using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq
{
    [Serializable]
    public class LTable
    {
        public Guid ID { get; } = Guid.NewGuid();

        public void SetProperty<T>(string name, ref T field, T value)
        {
            field = value;
        }
    }
}
