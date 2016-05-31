using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
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

        public bool Contains(Alternative alternative)
        {
            foreach (var alternativeNode in this)
            {
                if (alternativeNode.Alternative.Equals(alternative))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void ValidateBeforeAdd(AlternativeNode item)
        {
            base.ValidateBeforeAdd(item);

            if (this[item.Alternative] != null)
            {
                throw new ArgumentException("AlternativeNode with the same value of Alternative property has already been added");
            }
        }
    }
}
