using LDB.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.UnitTests
{
    [TestClass]
    public class LContextTests
    {
        [TestMethod]
        public void ParseConnectionString()
        {
            LContext target = new LContext();
            PrivateObject obj = new PrivateObject(target);
            var retVal = obj.Invoke("ParseConnectionString", "path:Data;position:relative;type:json");
        }
    }
}
