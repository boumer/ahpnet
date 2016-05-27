using Ahp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.Models
{
    public class HierarchyManager
    {
        public Hierarchy Hierarchy { get; private set; }

        public HierarchyManager(Hierarchy hierarchy)
        {
            Hierarchy = hierarchy;
        }
    }
}
