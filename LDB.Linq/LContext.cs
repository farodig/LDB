﻿using LDB.Linq.Enums;
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
    /// <summary>
    /// Storage context
    /// </summary>
    public class LContext : IDisposable
    {
        #region Constructor parameters
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

        /// <summary>
        /// Check is db for read only
        /// </summary>
        private bool IsReadOnly;
        
        // TODO: Add parameter SaveAtOnce
        #endregion

        #region Constructors
        /// <summary>
        /// Default parameters
        /// </summary>
        public LContext()
        {
            SetDefaultValues();

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
            //https://stackoverflow.com/questions/35600329/get-method-name-of-property-setter-in-c-sharp
            //ReplaceGetters();
        }
        #endregion

        #region Private methods

        private void SetDefaultValues()
        {
            Path = "Data";
            Position = PositionTypeEnum.Relative;
            Type = DataTypeEnum.JSON;
            IsReadOnly = false;
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
            // Select converter
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

            var props = GetType().GetProperties();
            foreach(var prop in props)
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    var value = prop.GetValue(this);

                    // Set db path with data for collection
                    value.GetType().GetMethod("SetDbPath").Invoke(value, new object[] { FilePath, FileType });

                    // Set db converter for collection
                    value.GetType().GetMethod("SetDbConverter").Invoke(value, new object[] { converter });

                    // Set db parameter IsReadOnly
                    value.GetType().GetMethod("SetIsReadOnly").Invoke(value, new object[] { IsReadOnly });
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
                            case "isreadonly":
                                IsReadOnly = Boolean.Parse(data[1]);
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
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // TODO write IDisposable of LContext, save?
        }
        #endregion

        #region Alternative way
        //value.GetType().GetMethod(nameof(DbSet<object>.InitSomething)).Invoke(value, new object[] { value, FilePath, FileType });
        #endregion
    }
}
