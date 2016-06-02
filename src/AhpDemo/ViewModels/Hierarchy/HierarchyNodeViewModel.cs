using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AhpDemo.ViewModels
{
    public abstract class HierarchyNodeViewModel : ViewModelBase, IComparable<HierarchyNodeViewModel>
    {
        private ObservableCollection<HierarchyNodeViewModel> _children = new ObservableCollection<HierarchyNodeViewModel>();

        public HierarchyNodeViewModel(HierarchyViewModel hierarchy)
        {
            Hierarchy = hierarchy;
        }

        private string _name;
        public virtual string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        public HierarchyViewModel Hierarchy { get; private set; }

        public HierarchyNodeViewModel Parent { get; protected set; }

        public IEnumerable<HierarchyNodeViewModel> Children
        {
            get { return _children; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(() => IsSelected);

                    if (_isSelected)
                    {
                        Hierarchy.SelectedNode = this;
                    }
                }
            }
        }

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged(() => IsExpanded);
                }

                // Expand all the way up to the root.
                if (_isExpanded && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

        private UIElement _actionControl;
        public UIElement ActionControl
        {
            get
            {
                if (_actionControl == null)
                {
                    _actionControl = CreateActionControl();
                }

                return _actionControl;
            }
        }

        public ICommand AddCriterion
        {
            get
            {
                return new RelayCommand
                (
                    (x) =>
                    {
                        var goal = x as GoalNodeViewModel;
                        if (goal != null)
                        {
                            Hierarchy.Manager.AddCriterion("Criterion");
                            return;
                        }

                        var criterion = x as CriterionNodeViewModel;
                        if (criterion != null)
                        {
                            Hierarchy.Manager.AddSubcriterion(criterion.Criterion, "Subcriterion");
                            return;
                        }
                    },
                    (x) => { return true; }
                );
            }
        }

        public ICommand AddAlternative
        {
            get
            {
                return new RelayCommand
                (
                    (x) => { Hierarchy.Manager.AddAlternative("Alternative"); },
                    (x) => { return true; }
                );
            }
        }

        public ICommand RemoveNode
        {
            get
            {
                return new RelayCommand(
                    (x) =>
                    {
                        var criterionNode = x as CriterionNodeViewModel;
                        if (criterionNode != null)
                        {
                            Hierarchy.Manager.RemoveCriterion(criterionNode.Criterion);
                        }

                        var alternativeNode = x as AlternativeNodeViewModel;
                        if (alternativeNode != null)
                        {
                            Hierarchy.Manager.RemoveAlternative(alternativeNode.Alternative);
                        }
                    },
                    (x) => { return true; }
                );
            }
        }

        public void AddChild(HierarchyNodeViewModel node)
        {
            _children.Add(node);
            node.Parent = this;

            SortChildren();
        }

        public void RemoveChild(HierarchyNodeViewModel node)
        {
            if (_children.Contains(node))
            {
                _children.Remove(node);
                node.Parent = null;

                SortChildren();
            }
        }

        protected abstract UIElement CreateActionControl();

        private void SortChildren()
        {
            var selectedNode = Hierarchy.SelectedNode;
            _children.Sort();
            Hierarchy.SelectedNode = selectedNode;
        }

        #region IComparable

        public virtual int CompareTo(HierarchyNodeViewModel other)
        {
            return 0;
        }

        #endregion
    }
}
