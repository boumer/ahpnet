using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    /// <summary>
    /// Delegate for handling AlternativeNodeCollection changes
    /// </summary>
    /// <param name="node">Changed AlternativeNode object</param>
    public delegate void AlternativeNodeCollectionCallback(AlternativeNode node);

    /// <summary>
    /// Strongly typed collection of AlternativeNode objects.
    /// Notifies clients about changes via callback methods.
    /// Does not allow to add AlternativeNode objects with the same Alternative instances.
    /// </summary>
    public class AlternativeNodeCollection : CollectionBase
    {
        private AlternativeNodeCollectionCallback alternativeAddedCallback;
        private AlternativeNodeCollectionCallback alternativeRemovedCallback;

        public AlternativeNodeCollection()
            : this(null, null)
        { }

        public AlternativeNodeCollection(AlternativeNodeCollectionCallback alternativeAddedCallback, AlternativeNodeCollectionCallback alternativeRemovedCallback)
        {
            this.alternativeAddedCallback = alternativeAddedCallback;
            this.alternativeRemovedCallback = alternativeRemovedCallback;
        }

        public AlternativeNode this[int index]
        {
            get { return (AlternativeNode)List[index]; }
        }

        public AlternativeNode this[Alternative alternative]
        {
            get
            {
                foreach (AlternativeNode alternativeNode in this.List)
                {
                    if (alternativeNode.Alternative.Equals(alternative))
                    {
                        return alternativeNode;
                    }
                }

                return null;
            }
        }

        public int Add(AlternativeNode node)
        {
            if (!List.Contains(node))
            {
                if (this[node.Alternative] != null)
                {
                    throw new ArgumentException("AlternativeNode with the same value of Alternative property has already been added");
                }

                return List.Add(node);
            }
            else
            {
                return List.IndexOf(node);
            }
        }

        public bool Contains(AlternativeNode node)
        {
            return List.Contains(node);
        }

        public bool Contains(Alternative alternative)
        {
            foreach (AlternativeNode alternativeNode in List)
            {
                if (alternativeNode.Alternative.Equals(alternative))
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(AlternativeNode node)
        {
            if (List.Contains(node))
            {
                List.Remove(node);
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            NotifyAddition((AlternativeNode)value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            NotifyRemoval((AlternativeNode)value);
        }

        protected override void OnClear()
        {
            AlternativeNode[] toBeCleared = new AlternativeNode[List.Count];
            List.CopyTo(toBeCleared, 0);

            foreach (AlternativeNode node in toBeCleared)
            {
                NotifyRemoval(node);
            }
        }

        private void NotifyAddition(AlternativeNode node)
        {
            if (alternativeAddedCallback != null)
            {
                alternativeAddedCallback(node);
            }
        }

        private void NotifyRemoval(AlternativeNode node)
        {
            if (alternativeRemovedCallback != null)
            {
                alternativeRemovedCallback(node);
            }
        }
    }
}
