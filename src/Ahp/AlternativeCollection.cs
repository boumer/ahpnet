using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ahp
{
    public class AlternativeCollection : NotificationCollection<Alternative>
    {
        public AlternativeCollection()
            : this(null, null)
        { }

        public AlternativeCollection(NotificationCollectionCallback<Alternative> itemAddedCallback, NotificationCollectionCallback<Alternative> itemRemovedCallback)
            : base(itemAddedCallback, itemRemovedCallback)
        { }

        public Alternative Add(string name)
        {
            return Add(Guid.NewGuid().ToString(), name);
        }

        public Alternative Add(string id, string name)
        {
            var alternative = new Alternative(id, name);
            this.Add(alternative);

            return alternative;
        }
    }
}
