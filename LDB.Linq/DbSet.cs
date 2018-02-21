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
    /// <typeparam name="T">Table</typeparam>
    public class DbSet<T> : IList<T> 
        //where T : LTable, new()
    {
        #region Local data base
        private List<T> _items;

        private List<T> Items
        {
            get
            {
                if (_items == null)
                    _items = LoadData();
                return _items;
            }
        }

        private List<T> LoadData()
        {
            return Converter.Deserialize<List<T>>(DbPath);
        }

        public void SetDbPath(string path, string extension)
        {
            
            var name = typeof(T).Name + "." + extension;// 
            var fullName = Path.Combine(path, name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(fullName))
                File.Create(fullName).Close();
                DbPath = fullName;
            //else
            //    throw new Exception($"File {fullName} not found");
        }

        public void SetDbConverter(IConverter converter)
        {
            Converter = converter;
        }

        private string DbPath;
        private IConverter Converter;

        #endregion



        #region IList

        /// <summary>
        /// Get item by index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Item</returns>
        public T this[int index]
        {
            get
            {
                //return Items[index];
                throw new NotImplementedException();
            }
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Get count of items collection
        /// </summary>
        public int Count => Items.Count();

        /// <summary>
        /// Is this collection not writable?
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Add object to the end of collection
        /// </summary>
        /// <param name="item">inserted item</param>
        public void Add(T item)
        {
            Items.Add(item);
            Converter.Serialize(DbPath, Items);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Clear all collection
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            Converter.Serialize(DbPath, Items);
        }

        /// <summary>
        /// Is item exist in this collection?
        /// </summary>
        /// <param name="item">searched item</param>
        /// <returns>result</returns>
        public bool Contains(T item) => Items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        //{
        //    foreach(var item in Items)
        //    {
        //        yield return item;
        //    }
        //}
        //=> Items.GetEnumerator();

        public int IndexOf(T item) => Items.IndexOf(item);

        public void Insert(int index, T item)
        {
            Items.Insert(index, item);
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            Items.Remove(item);
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //Items.geten
            throw new NotImplementedException();
        }

        //public static implicit operator List<T>(DbSet<T> items)
        //{
        //    //items.ToList();
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
