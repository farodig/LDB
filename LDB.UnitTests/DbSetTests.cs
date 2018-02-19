using LDB.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.UnitTests
{
    [TestClass]
    public class DbSetTests
    {
        DbSet<int> Tests = new DbSet<int>();

        [TestMethod]
        public void IsReadOnly()
        {
            bool success = true;
            if (Tests.IsReadOnly)
            {
                try
                {
                    Tests.Clear();
                    Tests.Add(1);
                    success = false;
                }
                catch
                {
                    success = true;
                }
                if (!success)
                    throw new Exception();

            } else
            {
                Tests.Clear();
                Tests.Add(1);
            }

        }

        [TestMethod]
        public void GetIndex()
        {
            Tests.Clear();
            Tests.Add(1);
            if (Tests[0] != 1)
                throw new Exception();
        }

        [TestMethod]
        public void Count()
        {
            Tests.Clear();
            if (Tests == null || Tests.Count > 0)
                throw new Exception();
        }

        [TestMethod]
        public void Add()
        {
            Tests.Clear();
            Tests.Add(1);
            if (!Tests.Any(a => a == 1))
                throw new Exception();
        }

        [TestMethod]
        public void Remove()
        {
            Tests.Clear();
            Tests.Add(1);
            Tests.Remove(1);
            if (Tests.Any())
                throw new Exception();
        }

        [TestMethod]
        public void Clear()
        {
            Tests.Clear();
            Tests.Add(1);
            Tests.Clear();
            if (Tests.Any())
                throw new Exception();
        }

        [TestMethod]
        public void Contains()
        {
            Tests.Clear();
            Tests.Add(1);
            if (!Tests.Contains(1))
                throw new Exception();
            if (Tests.Contains(2))
                throw new Exception();
        }
    }
}
