using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Represents AHP alternative
    /// </summary>
    public class Alternative
    {
        /// <summary>
        /// Initializes new instance of Alternative class
        /// </summary>
        public Alternative()
            : this(Guid.NewGuid().ToString(), null)
        { }

        /// <summary>
        /// Initializes new instance of Alternative class with the specified name
        /// </summary>
        /// <param name="name">Alternative name</param>
        public Alternative(string name)
            : this(Guid.NewGuid().ToString(), name)
        { }

        /// <summary>
        /// Initializes new instance of Alternative class with the specified id and name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Alternative(string id, string name)
        {
            ID = id;
            Name = name;
        }
        
        /// <summary>
        /// Alternative unique identifier
        /// </summary>
        public string ID { get; set; }
               
        /// <summary>
        /// Alternative name
        /// </summary>
        public string Name { get; set; }
    }
}
