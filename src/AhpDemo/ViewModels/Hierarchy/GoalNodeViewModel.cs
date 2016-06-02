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

        public IEnumerable<CriterionNodeViewModel> GetAllCriterionNodes()
        {
            var result = new List<CriterionNodeViewModel>();
            GetAllChildrenRecursive(Children.Cast<CriterionNodeViewModel>(), result);

            return result;
        }

        private void GetAllChildrenRecursive(IEnumerable<CriterionNodeViewModel> nodes, List<CriterionNodeViewModel> result)
        {
            foreach (var node in nodes)
            {
                result.Add(node);
            }

            foreach (var node in nodes)
            {
                GetAllChildrenRecursive(node.Children.Cast<CriterionNodeViewModel>(), result);
            }
        }
    }
}
