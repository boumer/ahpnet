using Ahp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class AlternativeNodeViewModel : HierarchyNodeViewModel
    {
        public Alternative Alternative { get; private set; }

        public AlternativeNodeViewModel(HierarchyViewModel hierarchy, Alternative alternative)
            : base(hierarchy)
        {
            Alternative = alternative;
            Name = alternative.Name;
        }
    }
}
