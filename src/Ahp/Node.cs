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

        public abstract decimal GlobalPriority { get; }
    }
}
