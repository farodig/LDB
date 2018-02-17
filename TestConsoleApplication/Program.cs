using LDB.ExampleDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmp = new MyJsonContext();
            var tmp2 = tmp.Tests;
        }
    }
}
