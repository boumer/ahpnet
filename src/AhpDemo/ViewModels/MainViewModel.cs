using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhpDemo.Models;
using Ahp;

namespace AhpDemo.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Tree = new HierarchyViewModel(new HierarchyManager(new Hierarchy()));
        }

        public HierarchyViewModel Tree { get; private set; }
    }
}
