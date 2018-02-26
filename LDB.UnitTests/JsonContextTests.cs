using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleApplication;
using TestConsoleApplication.Model;

namespace LDB.UnitTests
{
    [TestClass]
    public class JsonContextTests
    {
        public MyJsonContext db = new MyJsonContext();

        [TestMethod]
        public void GetMaxCodeTest()
        {
            var maxValue = db.Tests.Max(a => (int?)a.Code) ?? 0; // what's wrong with current directory? Test should be help!!! But they waste time.
        }

        [TestMethod]
        public void AddToCollectionTest()
        {
            var maxValue = db.Tests.Max(a => (int?)a.Code) ?? 0;
            db.Tests.Add(new Test
            {
                Code = ++maxValue,
                Name = "Name " + maxValue
            });
        }

        [TestMethod]
        public void DeleteFromCollectionIfCountMoreThan1Test()
        {
            if (db.Tests.Count > 1)
            {
                db.Tests.Remove(db.Tests.OrderBy(a => a.Code).Last());
            }
        }
    }
}
