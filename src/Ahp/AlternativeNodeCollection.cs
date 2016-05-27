using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    /// <summary>
    /// Strongly typed collection of AlternativeNode objects.
    /// Notifies clients about changes via callback methods.
    /// Does not allow to add AlternativeNode objects with the same Alternative instances.
    /// </summary>
    public class AlternativeNodeCollection : NotificationCollection<AlternativeNode>
    {
        public AlternativeNodeCollection()
            : this(null, null)
        { }

        public AlternativeNodeCollection(NotificationCollectionCallback<AlternativeNode> itemAddedCallback, NotificationCollectionCallback<AlternativeNode> itemRemovedCallback)
            : base(itemAddedCallback, itemRemovedCallback)
        { }

        public AlternativeNode this[Alternative alternative]
        {
            get
            {
                foreach (var alternativeNode in this)
                {
                    if (alternativeNode.Alternative == alternative)
                    {
                        return alternativeNode;
                    }
                }

                return null;
            }
        }

        protected override void ValidateBeforeAdd(AlternativeNode item)
        {
            base.ValidateBeforeAdd(item);

            if (this[item.Alternative] != null)
            {
                throw new ArgumentException("AlternativeNode with the same value of Alternative property has already been added");
            }
        }

        public bool Contains(Alternative alternative)
        {
            foreach (AlternativeNode alternativeNode in this)
            {
                if (alternativeNode.Alternative.Equals(alternative))
                {
                    return true;
                }
            }

            return false;
        }        
    }
}
