using LDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication.Model
{
    public class Test : LTable, ITable
    {
        public int Code { get; set; }

        public string Name { get; set; }

        //private string _name;
        //private int _code;

        //public string Name
        //{
        //    get { return _name; }
        //    set { SetProperty(nameof(Name), ref _name, value); }
        //}

        //public int Code
        //{
        //    get { return _code; }
        //    set { SetProperty(nameof(Code), ref _code, value); }
        //}
    }
}
