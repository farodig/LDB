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
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using LDB.Linq.Converters;

namespace LDB.Linq
{
    public class LContext// : IDisposable
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

        //https://stackoverflow.com/questions/18242429/how-to-deserialize-json-data
        //https://msdn.microsoft.com/en-us/library/system.web.script.serialization.javascriptserializer.aspx
        // System.Runtime.Serialization.Json
        // System.Web.Script.Serialization
        // TODO: Add parameter IsReadOnly
        // TODO: Add parameter SaveAtOnce

        /// <summary>
        /// Default parameters
        /// </summary>
        /// <summary xml:lang="ru">
        /// Параметры по умолчанию
        /// </summary>
        /// <summary xml:lang="ru-RU">
        /// Параметры по умолчанию 1
        /// </summary>
        /// <summary xml:lang="fr">
        /// Obtient ou définit la taille de remplissage de l'opération de chargement.
        /// </summary>
        public LContext()
        {
            SetDefaultValues();
            //https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method
            //ReplaceGetters();
            InitDbSet();
        }

        /// <summary>
        /// Custom parameters
        /// </summary>
        public LContext(string connectionString)
        {
            SetDefaultValues();

            ParseConnectionString(connectionString);

            InitDbSet();
            //https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method
            //ReplaceGetters();
        }

        private void SetDefaultValues()
        {
            Path = "Data";
            Position = PositionTypeEnum.Relative;
            Type = DataTypeEnum.JSON;
        }

        private string FileType => Type.ToString().ToLower();

        private string FilePath {
            get
            {
                switch(Position)
                {
                    case PositionTypeEnum.Absolute: return Path;
                    case PositionTypeEnum.Relative: return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Path); ;
                    default: return Path;
                }
            }
        }

        private void InitDbSet()
        {
            IConverter converter;
            switch(Type)
            {
                case DataTypeEnum.BIN:
                    converter = new BinConverter();
                    break;
                case DataTypeEnum.CSV:
                    converter = new CsvConverter();
                    break;
                case DataTypeEnum.JSON:
                    converter = new JsonConverter();
                    break;
                case DataTypeEnum.XML:
                    converter = new XmlConverter();
                    break;
                default:
                    converter = new XmlConverter();
                    break;
            }
            var props = GetType().GetProperties();//.Select(a => a.GetType()).ToList() ;
            foreach(var prop in props)
            {
                //if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                //{
                //    var value = prop.GetValue(this);
                //    // Коллекция
                    
                //}
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    var value = prop.GetValue(this);
                    value.GetType().GetMethod("SetDbPath").Invoke(value, new object[] { FilePath, FileType });
                    value.GetType().GetMethod("SetDbConverter").Invoke(value, new object[] { converter });
                    //var tmp = Convert.ChangeType(value, typeof(DbSet<LTable>));
                    //.Cast<DbSet<LTable>>();//.ToList();
                    //var items = ((IEnumerable)
                    //paramDictionary.Add(prop.Name, string.Join(", ", items.Select(a => a.GetType().GetProperty("Oid").GetValue(a).ToString()).ToList()));

                }
            }

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


        

        //private void ReplaceGetters()
        //{
        //    //var properties = GetType().GetProperties().ToList();//.Where(a => a.PropertyType is typeof(List<LocalTable>))//BindingFlags.Public | BindingFlags.GetProperty
        //    //foreach (var prop in properties)
        //    //{
        //    //    var tmp1 = prop.PropertyType;
        //    //}
        //}

        //protected void SetTable<T>(ref List<T> table, List<T> value)
        //{
        //    //WriteTable(value);
        //    //WriteJson(value);
        //    //table = value;
        //}

        //protected List<T> GetTable<T>(ref List<T> table)
        //{
        //    //table = ReadTable<T>("");
        //    //return table;
        //    return null;
        //}

        //public virtual void Dispose()
        //{
        //}
    }
}
