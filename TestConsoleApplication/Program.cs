using LDB.Linq.Enums;
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
            using (var db = new MyJsonContext(DataTypeEnum.BIN, "DataFolder", PositionTypeEnum.Relative, false))
            {
                // first load collection and get max value
                var maxValue = db.Tests.Max(a => (int?)a.Code) ?? 0;

                // add to collection and save file
                db.Tests.Add(new Test
                {
                    Code = ++maxValue,
                    Name = "Name " + maxValue
                });

                if (db.Tests.FirstOrDefault(a => a.Code == 1) is Test firstTest)
                {
                    firstTest.Name += "+";
                    db.Tests.Save();
                }
                //db.CommitChanges();
            }
        }
    }
}
