using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class AlternativesNodeViewModel : HierarchyNodeViewModel
    {
        public AlternativesNodeViewModel(HierarchyViewModel hierarchy)
            : base(hierarchy)
        {
            Name = "Alternatives";
        }
    }
}
