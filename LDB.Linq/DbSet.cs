using System;
using System.Collections;
using System.Collections.Generic;
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
            return new List<T>();
            //throw new NotImplementedException();
        }

        public void SetDbFile(string name) => DbPath = name;

        private string DbPath;

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
        public int Count
        {
            get
            {
                Items.Count();
                throw new NotImplementedException();
            }
        }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clear all collection
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Is item exist in this collection?
        /// </summary>
        /// <param name="item">searched item</param>
        /// <returns>result</returns>
        public bool Contains(T item)
        {
            Items.Contains(item);
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Items.GetEnumerator();
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            Items.IndexOf(item);
            throw new NotImplementedException();
        }

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
        #endregion
    }
}
