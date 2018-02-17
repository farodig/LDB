using LDB.ExampleDB.Model;
using LDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.ExampleDB
{
    public class MyJsonContext : LocalContext
    {
        public MyJsonContext() : base() { }
        public MyJsonContext(string connectionString) : base(connectionString) => throw new NotImplementedException();

        public List<Test> Tests { get; set; }
    }
}
