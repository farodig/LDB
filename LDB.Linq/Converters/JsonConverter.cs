//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Script.Serialization;

//namespace LDB.Linq.Converters
//{
//    internal class JsonConverter : IConverter
//    {
//        public T Deserialize<T>(string file) where T : new()
//        {
//            var serializer = new JavaScriptSerializer();
//            using (var sr = new StreamReader(file))
//            {
//                return serializer.Deserialize<T>(sr.ReadToEnd());
//            }
//        }

//        public void Serialize<T>(string file, T data) where T : new()
//        {
//            var serializer = new JavaScriptSerializer();
//            using (var sw = new StreamWriter(file))
//            {
//                sw.WriteLine(serializer.Serialize(data));
//            }
//        }
//    }
//}
