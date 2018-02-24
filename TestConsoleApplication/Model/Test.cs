using LDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication.Model
{
    // TODO: get rid of attribute. Need for bin converter. rewrite attribute | reflection
    // https://stackoverflow.com/questions/14663763/how-to-add-an-attribute-to-a-property-at-runtime
    //[Serializable] 
    public class Test : LTable
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
