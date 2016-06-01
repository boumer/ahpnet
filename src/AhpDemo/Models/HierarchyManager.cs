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

        public event Action HierarchyChanged;

        public HierarchyManager(Hierarchy hierarchy)
        {
            Hierarchy = hierarchy;
        }

        public void AddAlternative(string name)
        {
            Hierarchy.Alternatives.Add(name);
            OnHierarchyChanged();
        }

        public void RemoveCriterion(CriterionNode criterion)
        {
            //Hierarchy.RemoveCriterionNode(criterion);
        }

        private void OnHierarchyChanged()
        {
            var handler = HierarchyChanged;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
