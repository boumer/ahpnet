using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Tree = new HierarchyViewModel();
        }

        public HierarchyViewModel Tree { get; private set; }
    }
}
