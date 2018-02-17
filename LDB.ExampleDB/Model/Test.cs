using LDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDB.ExampleDB.Model
{
    public class Test : LocalTable
    {

        private string _name;
        private int _code;

        public string Name
        {
            get { return _name; }
            set { SetProperty(nameof(Name), ref _name, value); }
        }
        
        public int Code
        {
            get { return _code; }
            set { SetProperty(nameof(Code), ref _code, value); }
        }
    }
}
