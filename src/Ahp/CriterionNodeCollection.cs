using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    /// <summary>
    /// Strongly typed collection of AlternativeNode objects.
    /// Notifies clients about changes via callback methods.
    /// </summary>
    public class CriterionNodeCollection : NotificationCollection<CriterionNode>
    {
        public CriterionNodeCollection()
            : this(null, null)
        { }

        public CriterionNodeCollection(NotificationCollectionCallback<CriterionNode> itemAddedCallback, NotificationCollectionCallback<CriterionNode> itemRemovedCallback)
            : base(itemAddedCallback, itemRemovedCallback)
        { }

        public CriterionNode Add(string name)
        {
            return this.Add(name, 0M);
        }

        public CriterionNode Add(string name, decimal weight)
        {
            var node = new CriterionNode(name, weight);
            this.Add(node);

            return node;
        }
    }
}
