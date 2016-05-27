using Ahp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class GoalNodeViewModel : HierarchyNodeViewModel
    {
        public GoalNodeViewModel(HierarchyViewModel hierarchy)
            : base(hierarchy)
        {
            Name = "Goal";
        }
    }
}
