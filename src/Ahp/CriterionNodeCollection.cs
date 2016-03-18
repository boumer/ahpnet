using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    /// <summary>
    /// Delegate for handling CriterionNodeCollection changes
    /// </summary>
    /// <param name="node">Changed CriterionNode object</param>
    public delegate void CriterionNodeCollectionCallback(CriterionNode node);

    /// <summary>
    /// Strongly typed collection of AlternativeNode objects.
    /// Notifies clients about changes via callback methods.
    /// </summary>
    public class CriterionNodeCollection : CollectionBase
    {
        public CriterionNodeCollectionCallback criterionAddedCallback;
        public CriterionNodeCollectionCallback criterionRemovedCallback;

        public CriterionNodeCollection()
            : this(null, null)
        { }

        public CriterionNodeCollection(CriterionNodeCollectionCallback criterionAddedCallback, CriterionNodeCollectionCallback criterionRemovedCallback)
        {
            this.criterionAddedCallback = criterionAddedCallback;
            this.criterionRemovedCallback = criterionRemovedCallback;
        }

        public CriterionNode this[int index]
        {
            get { return (CriterionNode)List[index]; }
        }

        public CriterionNode Add(string name)
        {
            return this.Add(name, 0M);
        }

        public CriterionNode Add(string name, decimal weight)
        {
            CriterionNode node = new CriterionNode(name, weight);
            this.Add(node);

            return node;
        }

        public int Add(CriterionNode node)
        {
            if (!List.Contains(node))
            {
                return List.Add(node);
            }
            else
            {
                return List.IndexOf(node);
            }
        }

        public bool Contains(CriterionNode node)
        {
            return List.Contains(node);
        }

        public void Remove(CriterionNode node)
        {
            if (List.Contains(node))
            {
                List.Remove(node);
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            NotifyAddition((CriterionNode)value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            NotifyRemoval((CriterionNode)value);
        }

        protected override void OnClear()
        {
            CriterionNode[] toBeCleared = new CriterionNode[List.Count];
            List.CopyTo(toBeCleared, 0);

            foreach (CriterionNode node in toBeCleared)
            {
                NotifyRemoval(node);
            }
        }

        private void NotifyAddition(CriterionNode node)
        {
            if (criterionAddedCallback != null)
            {
                criterionAddedCallback(node);
            }
        }

        private void NotifyRemoval(CriterionNode node)
        {
            if (criterionRemovedCallback != null)
            {
                criterionRemovedCallback(node);
            }
        }
    }
}
