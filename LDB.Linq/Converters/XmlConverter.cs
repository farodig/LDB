using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LDB.Linq.Converters
{
    internal class XmlConverter : IConverter
    {
        public T Deserialize<T>(string file) where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    if (fs.Length == 0)
                    {
                        return new T();
                    } else
                    {
                        return (T)serializer.Deserialize(reader);
                    }
                    
                }
            }
        }

        public void Serialize<T>(string file, T data) where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                using (XmlWriter writer = XmlWriter.Create(fs))
                {
                    serializer.Serialize(writer, data);
                }
            }
        }
    }
}
