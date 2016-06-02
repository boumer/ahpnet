using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public delegate void NodeCollectionCallback<T>(T item);

    public class NodeCollection<T> : IList<T>
    {
        private List<T> _list = new List<T>();

        private NodeCollectionCallback<T> _nodeAddedCallback;
        private NodeCollectionCallback<T> _nodeRemovedCallback;

        public NodeCollection()
            : this(null, null)
        { }

        public NodeCollection(NodeCollectionCallback<T> nodeAddedCallback, NodeCollectionCallback<T> nodeRemovedCallback)
        {
            _nodeAddedCallback = nodeAddedCallback;
            _nodeRemovedCallback = nodeRemovedCallback;
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
            if (_nodeAddedCallback != null)
            {
                _nodeAddedCallback(item);
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
            if (_nodeAddedCallback != null)
            {
                _nodeAddedCallback(item);
            }
        }

        public bool Remove(T item)
        {
            var removed = _list.Remove(item);
            if (removed)
            {
                if (_nodeRemovedCallback != null)
                {
                    _nodeRemovedCallback(item);
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
                throw new ArgumentException("Same node can not be added twice.");
            }

            if (_list.Count > 0 && 
                _list[0].GetType() != item.GetType())
            {
                throw new ArgumentException("Only nodes of the same type can be added.");
            }
        }
    }
}
