using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class CriterionNodeViewModel : HierarchyNodeViewModel
    {
        public CriterionNodeViewModel(HierarchyViewModel hierarchy)
            : base(hierarchy)
        {
            Name = "Criterion";
        }
    }
}
