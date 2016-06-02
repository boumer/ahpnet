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
            Hierarchy.AddAlternative(name);
            OnHierarchyChanged();
        }

        public void RemoveAlternative(Alternative alternative)
        {
            Hierarchy.RemoveAlternative(alternative);
            OnHierarchyChanged();
        }

        internal void AddCriterion(string name)
        {
            Hierarchy.GoalNode.AddCriterionNode(name);
            OnHierarchyChanged();
        }

        public void AddSubcriterion(CriterionNode parent, string name)
        {
            parent.AddSubcriterionNode(name);
            OnHierarchyChanged();
        }

        public void RemoveCriterion(CriterionNode criterion)
        {
            Hierarchy.RemoveCriterionNode(criterion);
            OnHierarchyChanged();
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
