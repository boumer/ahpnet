using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Base class for all node classes
    /// </summary>
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
            this.name = name;
            this.localPriority = localPriority;
        }
        
        private string name;
        /// <summary>
        /// Name of the node
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        private decimal localPriority;
        /// <summary>
        /// Local priority of the node
        /// </summary>
        public virtual decimal LocalPriority
        {
            get { return localPriority; }
            set { localPriority = value; }
        }

        /// <summary>
        /// Global priority of the node
        /// </summary>
        public abstract decimal GlobalPriority { get; }
    }
}
