using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class Alternative
    {
        public Alternative()
            : this(Guid.NewGuid().ToString(), null)
        { }

        public Alternative(string name)
            : this(Guid.NewGuid().ToString(), name)
        { }

        public Alternative(string id, string name)
        {
            ID = id;
            Name = name;
        }
        
        public string ID { get; set; }
               
        public string Name { get; set; }
    }
}
