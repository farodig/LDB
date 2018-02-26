using LDB.Linq.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LDB.Linq
{
    /// <summary>
    /// Init table
    /// </summary>
    public class DbSet<T> : IList<T>
    {
        #region Private properties

        private List<T> _items;
        private string DbPath;
        private IConverter Converter;

        // Autoload collection
        private List<T> Items
        {
            get
            {
                if (_items == null)
                    _items = Converter.Deserialize<List<T>>(DbPath) ?? new List<T>();
                return _items;
            }
        }
        #endregion

        #region Constructors
        public DbSet(string FilePath, string FileType, IConverter converter, bool IsReadOnly)// )
        {
            SetDbPath(FilePath, FileType);

            SetDbConverter(converter);

            SetIsReadOnly(IsReadOnly);
            //SetAttributes();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Save table
        /// </summary>
        public void Save()
        {
            if (!IsReadOnly)
                Converter.Serialize(DbPath, Items);
            else
                throw new Exception("Data base is read only.");
        }
        #endregion

        #region Private methods

        #region Setters

        // Init storage file
        private void SetDbPath(string path, string extension)
        {
            var name = typeof(T).Name + "." + extension;
            var fullName = Path.Combine(path, name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(fullName))
                File.Create(fullName).Close();
            DbPath = fullName;
        }

        // Init converter type
        private void SetDbConverter(IConverter converter) => Converter = converter;

        // Init IsReadOnly
        private void SetIsReadOnly(bool key) => IsReadOnly = key;
        #endregion

        #endregion

        #region IList

        /// <summary>
        /// Get item by Index from collection
        /// </summary>
        public T this[int index]
        {
            get => Items[index]; 
            set
            {
                if (!IsReadOnly)
                    Items[index] = value;
                else
                    throw new Exception("Data base is read only.");
            }
        }
        
        /// <summary>
        /// Gec count of items in collection
        /// </summary>
        public int Count => Items.Count();

        /// <summary>
        /// Check is read only collection
        /// </summary>
        public bool IsReadOnly { get; private set; }
        
        /// <summary>
        /// Add item to collection
        /// </summary>
        public void Add(T item)
        {
            if (!IsReadOnly)
            {
                Items.Add(item);
                Converter.Serialize(DbPath, Items);
            } else
                throw new Exception("Data base is read only.");
        }
        
        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            if (!IsReadOnly)
            {
                Items.Clear();
                Converter.Serialize(DbPath, Items);
            }
            else
                throw new Exception("Data base is read only.");
        }
        
        /// <summary>
        /// Check existing item in collection
        /// </summary>
        public bool Contains(T item) => Items.Contains(item);

        /// <summary>
        /// Copy list to array
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

        /// <summary>
        /// Get index by item
        /// </summary>
        public int IndexOf(T item) => Items.IndexOf(item);

        /// <summary>
        /// Insert item to collection
        /// </summary>
        public void Insert(int index, T item)
        {
            if (!IsReadOnly)
            {
                Items.Insert(index, item);
                Converter.Serialize(DbPath, Items);
            }
            else
                throw new Exception("Data base is read only.");
        }

        /// <summary>
        /// Delete item from collection 
        /// </summary>
        public bool Remove(T item)
        {
            if (!IsReadOnly)
            {
                bool result = Items.Remove(item);
                Converter.Serialize(DbPath, Items);
                return result;
            }
            else
                throw new Exception("Data base is read only.");
        }

        /// <summary>
        /// Delete item from collection by index
        /// </summary>
        public void RemoveAt(int index)
        {
            if (!IsReadOnly)
            {
                Items.RemoveAt(index);
                Converter.Serialize(DbPath, Items);
            }
            else
                throw new Exception("Data base is read only.");
        }

        /// <summary>
        /// Get enumerator from collection
        /// </summary>
        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        /// <summary>
        /// Get enumerator from collection
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region Alternative way
        //public static void InitSomething(ref DbSet<T> self, string path, string extension)
        //{
        //    var name = typeof(T).Name + "." + extension;
        //    var fullName = Path.Combine(path, name);
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);
        //    if (!File.Exists(fullName))
        //        File.Create(fullName).Close();
        //    self.DbPath = fullName;
        //}
        #endregion

        #region Try

        private void SetAttributes()//Type attributeType)
        {
            var type = typeof(T);
            var aName = new AssemblyName("LDB.Linq");
            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name);
            var tb = mb.DefineType(type.Name + "Proxy", System.Reflection.TypeAttributes.Public, type);

            var attrCtorParams = new Type[] { };// typeof(string) };
            var attrCtorInfo = typeof(SerializableAttribute).GetConstructor(attrCtorParams);
            var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { });// "Some Value" });
            tb.SetCustomAttribute(attrBuilder);

            var newType = tb.CreateType();


            //Type genericType = typeof(DbSet<>).MakeGenericType(new Type[] { newType });
            //var instance = Activator.CreateInstance(genericType);
            //Type myParameterizedSomeClass = GetType().MakeGenericType(newType);
            //ConstructorInfo constr = myParameterizedSomeClass.GetConstructor(new Type[] { });

            //var tmp = constr.Invoke(new object[] { });
            //var constructor = GetType().GetConstructor(new Type[] { });
            //ConstructorInfo generic = constructor.
            //generic

            //MethodInfo method = typeof(Sample).GetMethod("GenericMethod");
            //MethodInfo generic = method.MakeGenericMethod(myType);
            //generic.Invoke(this, null);

            //var instance = (DbSet<T>)Activator.CreateInstance(newType);
        }
        #endregion
    }
}
