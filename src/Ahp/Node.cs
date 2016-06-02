using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ahp
{
    public abstract class Node
    {
        public Node()
            : this(null, 0M)
        { }

        public Node(string name)
            : this(name, 0M)
        { }

        public Node(string name, decimal localPriority)
        {
            _name = name;
            _localPriority = localPriority;

            _childNodes = new List<Node>();
            _childNodesReadOnly = new ReadOnlyNodeCollection(_childNodes);
        }

        private string _name;
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private decimal _localPriority;
        public virtual decimal LocalPriority
        {
            get { return _localPriority; }
            set { _localPriority = value; }
        }

        public decimal GlobalPriority
        {
            get
            {
                if (ParentNode != null)
                {
                    return LocalPriority * ParentNode.GlobalPriority;
                }
                else
                {
                    return LocalPriority;
                }
            }
        }

        private Node _parentNode;
        protected virtual Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                if (value == _parentNode)
                {
                    return;
                }

                if (_parentNode != null && _parentNode.ChildNodes.Contains(this))
                {
                    _parentNode.RemoveChildNode(this);
                }

                _parentNode = value;

                if (_parentNode != null)
                {
                    if (!_parentNode.ChildNodes.Contains(this))
                    {
                        _parentNode.AddChildNode(this);
                    }
                }
            }
        }

        private List<Node> _childNodes;
        private ReadOnlyNodeCollection _childNodesReadOnly;
        protected ReadOnlyNodeCollection ChildNodes
        {
            get { return _childNodesReadOnly; }
        }

        protected void AddChildNode(Node node)
        {
            if (_childNodes.Contains(node))
            {
                throw new ArgumentException("Same node can not be added twice.");
            }

            if (_childNodes.Count > 0 &&
                _childNodes[0].GetType() != node.GetType())
            {
                throw new ArgumentException("Only nodes of the same type can be added.");
            }

            _childNodes.Add(node);
            HandleChildAdded(node);
        }

        protected void RemoveChildNode(Node node)
        {
            _childNodes.Remove(node);
            HandleChildRemoved(node);
        }

        protected void ClearChildNodes()
        {
            while (_childNodes.Count > 0)
            {
                RemoveChildNode(_childNodes[0]);
            }
        }

        public ICollection<T> SearchChildNodes<T>(Func<T, bool> condition) where T : Node
        {
            var result = new List<T>();
            SearchChildNodesRecursive(condition, ChildNodes.OfType<T>(), result);

            return result;
        }

        private void SearchChildNodesRecursive<T>(Func<T, bool> condition, IEnumerable<T> nodes, ICollection<T> result) where T : Node
        {
            foreach (var node in nodes)
            {
                if (condition(node))
                {
                    result.Add(node);
                }
            }

            foreach (var node in nodes)
            {
                SearchChildNodesRecursive(condition, node.ChildNodes.OfType<T>(), result);
            }
        }

        protected virtual void HandleChildAdded(Node node)
        {
            if (node.ParentNode != this)
            {
                node.ParentNode = this;
            }
        }

        protected virtual void HandleChildRemoved(Node node)
        {
            if (node.ParentNode == this)
            {
                node.ParentNode = null;
            }
        }
    }
}
