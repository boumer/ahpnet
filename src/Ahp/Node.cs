using System;
using System.Collections.Generic;
using System.Text;

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

            _childNodes = new NotificationCollection<Node>(HandleChildAdded, HandleChildRemoved);
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

                if (_parentNode != null)
                {
                    _parentNode.ChildNodes.Remove(this);
                }

                _parentNode = value;

                if (_parentNode != null)
                {
                    if (!_parentNode.ChildNodes.Contains(this))
                    {
                        _parentNode.ChildNodes.Add(this);
                    }
                }
            }
        }

        private NotificationCollection<Node> _childNodes;
        protected NotificationCollection<Node> ChildNodes
        {
            get { return _childNodes; }
        }

        private void HandleChildAdded(Node node)
        {
            if (node.ParentNode != this)
            {
                node.ParentNode = this;
            }
        }

        private void HandleChildRemoved(Node node)
        {
            if (node.ParentNode == this)
            {
                node.ParentNode = null;
            }
        }
    }
}
