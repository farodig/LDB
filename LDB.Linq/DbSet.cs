using LDB.Linq.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // TODO: Add IsReadOnly logic this[int index], 
        private bool _isReadOnly = false;
        
        // Autoload collection
        private List<T> Items
        {
            get
            {
                if (_items == null)
                    _items = Converter.Deserialize<List<T>>(DbPath);
                return _items;
            }
        }
        #endregion

        #region Setters

        // Init storage file
        public void SetDbPath(string path, string extension)
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
        public void SetDbConverter(IConverter converter) => Converter = converter;

        // Init IsReadOnly
        public void SetIsReadOnly(bool key) => _isReadOnly = key; 
        #endregion

        #region IList

        /// <summary>
        /// Get item by Index from collection
        /// </summary>
        public T this[int index]
        {
            get => Items[index]; 
            set => Items[index] = value;
        }
        
        /// <summary>
        /// Gec count of items in collection
        /// </summary>
        public int Count => Items.Count();

        /// <summary>
        /// Check is read only collection
        /// </summary>
        public bool IsReadOnly => _isReadOnly;
        
        /// <summary>
        /// Add item to collection
        /// </summary>
        public void Add(T item)
        {
            Items.Add(item);
            Converter.Serialize(DbPath, Items);
        }
        
        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            Converter.Serialize(DbPath, Items);
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
            Items.Insert(index, item);
            Converter.Serialize(DbPath, Items);
        }

        /// <summary>
        /// Delete item from collection 
        /// </summary>
        public bool Remove(T item)
        {
            bool result = Items.Remove(item);
            Converter.Serialize(DbPath, Items);
            return result;
        }

        /// <summary>
        /// Delete item from collection by index
        /// </summary>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            Converter.Serialize(DbPath, Items);
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
    }
}
