using LDB.Linq.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LDB.Linq
{
    public class LocalContext : IDisposable
    {
        /// <summary>
        /// Path to data files (default: LocalDB)
        /// </summary>
        private string Path;

        /// <summary>
        /// Position type relative|absolute (default: relative)
        /// </summary>
        private PositionTypeEnum Position;

        /// <summary>
        /// Data type json|xml|csv (default: json)
        /// </summary>
        private DataTypeEnum Type;

        // TODO: Add parameter IsReadOnly
        // TODO: Add parameter SaveAtOnce

        /// <summary>
        /// Default parameters
        /// </summary>
        public LocalContext()
        {
            SetDefaultValues();
            //https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method
            ReplaceGetters();
        }

        /// <summary>
        /// Custom parameters
        /// </summary>
        public LocalContext(string connectionString)
        {
            SetDefaultValues();

            ParseConnectionString(connectionString);

            //https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method
            ReplaceGetters();
        }

        private void SetDefaultValues()
        {
            Path = "LocalDB";
            Position = PositionTypeEnum.Relative;
            Type = DataTypeEnum.JSON;
        }

        private void ParseConnectionString(string connectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception("Connection string is empty. Use another constructor.");

                var attributes = connectionString.Split(';');

                if (!attributes.Any())
                    throw new Exception("Connection string doesn't contain attributes.");

                foreach (var attr in attributes)
                {
                    try
                    {
                        var data = attr.Split(':');
                        switch (data[0].ToLower())
                        {
                            case "path":
                                Path = data[1];
                                break;
                            case "position":
                                Position = (PositionTypeEnum)Enum.Parse(typeof(PositionTypeEnum), data[1], true);
                                break;
                            case "type":
                                Type = (DataTypeEnum)Enum.Parse(typeof(DataTypeEnum), data[1], true);
                                break;
                        }
                    }
                    catch (Exception parseAttribute)
                    {

                        throw new Exception($"Parse attribute ({attr}): {parseAttribute.Message}");
                    }
                }
            }
            catch (Exception parseError)
            {
                throw new Exception($"Parse connection string: {parseError.Message}");
            }
        }

        private void ReplaceGetters()
        {
            //var properties = GetType().GetProperties().ToList();//.Where(a => a.PropertyType is typeof(List<LocalTable>))//BindingFlags.Public | BindingFlags.GetProperty
            //foreach (var prop in properties)
            //{
            //    var tmp1 = prop.PropertyType;
            //}
        }

        protected void SetTable<T>(ref List<T> table, List<T> value)
        {
            WriteTable(value);
            table = value;
        }

        protected List<T> GetTable<T>(ref List<T> table)
        {
            table = ReadTable<T>("");
            return table;
        }

        public virtual void Dispose()
        {
        }

        private List<T> ReadTable<T>(string name)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            var filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "test.xml");
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {

                using (XmlReader reader = XmlReader.Create(fs))
                {
                    return (List<T>)serializer.Deserialize(reader);//, "utf-8");
                    //serializer.Serialize(reader, table);
                }

                //serializer.Deserialize()
            }
            //XmlSerializer serializer = ;
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(User));
            //throw new NotImplementedException();
        }

        private void WriteTable<T>(List<T> table)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            var filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "test.xml");
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {

                using (XmlWriter writer = XmlWriter.Create(fs))
                {
                    serializer.Serialize(writer, table);
                }
                    
                //serializer.Deserialize()
            }
                
            //if (!=)
            //throw new NotImplementedException();
        }
    }
}
