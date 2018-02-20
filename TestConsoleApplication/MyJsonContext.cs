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
        public DbSet<Test> Tmp = new DbSet<Test>();
        //public List<Test> Tmp2 = new DbSet<Test>();

        public List<Test> _tests;
        public List<Test> Tests
        {
            get { return GetTable(ref _tests); }
            set { SetTable(ref _tests, value); }
        }

        public void xx()
        {
            //Tmp.AddRange
            //Tmp
            var tmp = new List<string>();
        }
    }
}
