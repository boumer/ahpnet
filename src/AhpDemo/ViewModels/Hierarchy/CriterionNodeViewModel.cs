using Ahp;
using AhpDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public override string Name
        {
            get { return Criterion.Name; }
            set
            {
                Criterion.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        protected override UIElement CreateActionControl()
        {
            return new CriterionView(this);
        }
    }
}
