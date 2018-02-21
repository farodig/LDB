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
            // init
            var db = new MyJsonContext();
            
            //db.Tests.Clear();

            // first load collection and get max value
            var maxValue = db.Tests.OrderBy(a => a.Code).LastOrDefault()?.Code ?? 0;//.Max(a => a.Code);

            // add to collection and save file
            db.Tests.Add(new Test
            {
                Code = ++maxValue,
                Name = "Name " + maxValue
            });
        }
    }
}
