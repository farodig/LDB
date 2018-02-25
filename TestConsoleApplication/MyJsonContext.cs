using LDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleApplication.Model;

namespace TestConsoleApplication
{
    public class MyJsonContext : LContext
    {
        public DbSet<Test> Tests { get; set; }
    }
}
