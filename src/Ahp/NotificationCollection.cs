using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public delegate void NotificationCollectionCallback<T>(T item);

    public class NotificationCollection<T> : IList<T>
    {
        private List<T> _list = new List<T>();

        private NotificationCollectionCallback<T> _itemAddedCallback;
        private NotificationCollectionCallback<T> _itemRemovedCallback;

        public NotificationCollection()
            : this(null, null)
        { }

        public NotificationCollection(NotificationCollectionCallback<T> itemAddedCallback, NotificationCollectionCallback<T> itemRemovedCallback)
        {
            _itemAddedCallback = itemAddedCallback;
            _itemRemovedCallback = itemRemovedCallback;
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<T>)_list).IsReadOnly; }
        }

        public void Add(T item)
        {
            ValidateBeforeAdd(item);

            _list.Add(item);
            if (_itemAddedCallback != null)
            {
                _itemAddedCallback(item);
            }
        }

        public void Clear()
        {
            while (_list.Count > 0)
            {
                Remove(_list[0]);
            }
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ValidateBeforeAdd(item);

            _list.Insert(index, item);
            if (_itemAddedCallback != null)
            {
                _itemAddedCallback(item);
            }
        }

        public bool Remove(T item)
        {
            var removed = _list.Remove(item);
            if (removed)
            {
                if (_itemRemovedCallback != null)
                {
                    _itemRemovedCallback(item);
                }
            }

            return removed;
        }

        public void RemoveAt(int index)
        {
            var item = this[index];
            Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        protected virtual void ValidateBeforeAdd(T item)
        {
            if (Contains(item))
            {
                throw new ArgumentException("Same item can not be added twice.");
            }
        }
    }
}
