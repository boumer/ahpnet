using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class AlternativeNodeViewModel : HierarchyNodeViewModel
    {
        public AlternativeNodeViewModel(HierarchyViewModel hierarchy)
            : base(hierarchy)
        {
            Name = "Alternative";
        }
    }
}
