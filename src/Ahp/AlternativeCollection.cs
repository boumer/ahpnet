using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    /// <summary>
    /// Delegate for handling AlternativeCollection changes
    /// </summary>
    /// <param name="node">Changed Alternative object</param>
    public delegate void AlternativeCollectionCallback(Alternative node);

    /// <summary>
    /// Strongly typed collection of Alternative objects.
    /// Notifies clients about changes via callback methods.
    /// </summary>
    public class AlternativeCollection : CollectionBase
    {
        private AlternativeCollectionCallback alternativeAddedCallback;
        private AlternativeCollectionCallback alternativeRemovedCallback;

        public AlternativeCollection()
            : this(null, null)
        { }

        public AlternativeCollection(AlternativeCollectionCallback alternativeAddedCallback, AlternativeCollectionCallback alternativeRemovedCallback)
        {
            this.alternativeAddedCallback = alternativeAddedCallback;
            this.alternativeRemovedCallback = alternativeRemovedCallback;
        }

        public Alternative this[int index]
        {
            get { return (Alternative)List[index]; }
        }

        public Alternative Add(string name)
        {
            return Add(Guid.NewGuid().ToString(), name);
        }

        public Alternative Add(string id, string name)
        {
            Alternative alternative = new Alternative(id, name);
            this.Add(alternative);

            return alternative;
        }

        public int Add(Alternative node)
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

        public bool Contains(Alternative node)
        {
            return List.Contains(node);
        }

        public void Remove(Alternative node)
        {
            if (List.Contains(node))
            {
                List.Remove(node);
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            NotifyAddition((Alternative)value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            NotifyRemoval((Alternative)value);
        }

        protected override void OnClear()
        {
            Alternative[] toBeCleared = new Alternative[List.Count];
            List.CopyTo(toBeCleared, 0);

            foreach (Alternative node in toBeCleared)
            {
                NotifyRemoval(node);
            }
        }

        private void NotifyAddition(Alternative node)
        {
            if (alternativeAddedCallback != null)
            {
                alternativeAddedCallback(node);
            }
        }

        private void NotifyRemoval(Alternative node)
        {
            if (alternativeRemovedCallback != null)
            {
                alternativeRemovedCallback(node);
            }
        }
    }
}
