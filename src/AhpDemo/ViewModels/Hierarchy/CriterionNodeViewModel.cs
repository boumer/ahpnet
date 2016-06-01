using Ahp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class CriterionNodeViewModel : HierarchyNodeViewModel
    {
        public CriterionNode Criterion { get; private set; }

        public CriterionNodeViewModel(HierarchyViewModel hierarchy, CriterionNode criterion)
            : base(hierarchy)
        {
            Criterion = criterion;
            Name = criterion.Name;
        }
    }
}
