using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleApplication.Model;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new MyJsonContext())
            {
                var tmp2 = db.Tests;
                //db.Tests = new List<Test>();

                var tmp = new List<Test>();
                
                tmp.Add(new Test
                {
                    Code = 1,
                    Name = "Name 1"
                }); tmp.Add(new Test
                {
                    Code = 2,
                    Name = "Name 2"
                });
                db.Tests = tmp;

            }
        }
    }
}
