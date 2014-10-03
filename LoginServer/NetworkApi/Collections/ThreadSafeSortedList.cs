// Type: Hik.Collections.ThreadSafeSortedList`2
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System.Collections.Generic;
using System.Threading;

namespace NetworkApi.Collections
{
    /// <summary>
    /// This class is used to store key-value based items in a thread safe manner.
    ///             It uses System.Collections.Generic.SortedList internally.
    /// 
    /// </summary>
    /// <typeparam name="TK">Key type</typeparam><typeparam name="TV">Value type</typeparam>
    public class ThreadSafeSortedList<TK, TV>
    {
        /// <summary>
        /// Internal collection to store items.
        /// 
        /// </summary>
        protected readonly SortedList<TK, TV> _items;
        /// <summary>
        /// Used to synchronize access to _items list.
        /// 
        /// </summary>
        protected readonly ReaderWriterLockSlim _lock;

        /// <summary>
        /// Gets/adds/replaces an item by key.
        /// 
        /// </summary>
        /// <param name="key">Key to get/set value</param>
        /// <returns>
        /// Item associated with this key
        /// </returns>
        public TV this[TK key]
        {
            get
            {
                this._lock.EnterReadLock();
                try
                {
                    return this._items.ContainsKey(key) ? this._items[key] : default(TV);
                }
                finally
                {
                    this._lock.ExitReadLock();
                }
            }
            set
            {
                this._lock.EnterWriteLock();
                try
                {
                    this._items[key] = value;
                }
                finally
                {
                    this._lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Gets count of items in the collection.
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                this._lock.EnterReadLock();
                try
                {
                    return this._items.Count;
                }
                finally
                {
                    this._lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Creates a new ThreadSafeSortedList object.
        /// 
        /// </summary>
        public ThreadSafeSortedList()
        {
            this._items = new SortedList<TK, TV>();
            this._lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Checks if collection contains spesified key.
        /// 
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>
        /// True; if collection contains given key
        /// </returns>
        public bool ContainsKey(TK key)
        {
            this._lock.EnterReadLock();
            try
            {
                return this._items.ContainsKey(key);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Checks if collection contains spesified item.
        /// 
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>
        /// True; if collection contains given item
        /// </returns>
        public bool ContainsValue(TV item)
        {
            this._lock.EnterReadLock();
            try
            {
                return this._items.ContainsValue(item);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes an item from collection.
        /// 
        /// </summary>
        /// <param name="key">Key of item to remove</param>
        public bool Remove(TK key)
        {
            this._lock.EnterWriteLock();
            try
            {
                if (!this._items.ContainsKey(key))
                    return false;
                this._items.Remove(key);
                return true;
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Gets all items in collection.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Item list
        /// </returns>
        public List<TV> GetAllItems()
        {
            this._lock.EnterReadLock();
            try
            {
                return new List<TV>((IEnumerable<TV>)this._items.Values);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes all items from list.
        /// 
        /// </summary>
        public void ClearAll()
        {
            this._lock.EnterWriteLock();
            try
            {
                this._items.Clear();
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Gets then removes all items in collection.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Item list
        /// </returns>
        public List<TV> GetAndClearAllItems()
        {
            this._lock.EnterWriteLock();
            try
            {
                List<TV> list = new List<TV>((IEnumerable<TV>)this._items.Values);
                this._items.Clear();
                return list;
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }
    }
}
