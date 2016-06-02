using Ahp;
using AhpDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AhpDemo.ViewModels
{
    public class HierarchyViewModel : ViewModelBase
    {
        private Dispatcher _dispatcher;

        public HierarchyManager Manager { get; set; }

        public GoalNodeViewModel GoalNode { get; private set; }
        public AlternativesNodeViewModel AlternativesNode { get; private set; }

        public HierarchyViewModel(HierarchyManager manager)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

            Manager = manager;
            Manager.HierarchyChanged += HandleHierarchyChanged;

            InitializeNodes();
        }

        private HierarchyNodeViewModel _selectedNode;
        public HierarchyNodeViewModel SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                if (SetProperty(ref _selectedNode, value))
                {
                    OnPropertyChanged(() => SelectedNodeActionControl);
                }
            }
        }

        public UIElement SelectedNodeActionControl
        {
            get { return _selectedNode != null ? _selectedNode.ActionControl : null; }
        }

        private readonly ObservableCollection<HierarchyNodeViewModel> _nodes = new ObservableCollection<HierarchyNodeViewModel>();
        public ObservableCollection<HierarchyNodeViewModel> Nodes
        {
            get { return _nodes; }
        }

        private void InitializeNodes()
        {
            GoalNode = new GoalNodeViewModel(this, Manager.Hierarchy.GoalNode);
            Nodes.Add(GoalNode);

            AlternativesNode = new AlternativesNodeViewModel(this);
            Nodes.Add(AlternativesNode);
        }

        private void HandleHierarchyChanged()
        {
            UpdateHierarchy();
        }

        private void UpdateHierarchy()
        {
            UpdateCriterionNodes();
            UpdateAlternativeNodes();
        }

        private void UpdateCriterionNodes()
        {
            var criterionNodes = GoalNode.GetAllCriterionNodes().ToList();
            var criterions = Manager.Hierarchy.GoalNode.SearchChildNodes<CriterionNode>(x => x is CriterionNode).OrderBy(x => x.Level).ToList();

            foreach (var criterion in criterions)
            {
                var criterionNode = criterionNodes.SingleOrDefault(x => x.Criterion == criterion);
                if (criterionNode == null)
                {
                    criterionNode = new CriterionNodeViewModel(this, criterion);
                    criterionNodes.Add(criterionNode);
                }

                var parentNode = (criterion.GoalNode != null) ? (HierarchyNodeViewModel)GoalNode : (HierarchyNodeViewModel)criterionNodes.Single(x => x.Criterion == criterion.ParentCriterionNode);
                if (criterionNode.Parent != null && criterionNode.Parent != parentNode)
                {
                    criterionNode.Parent.RemoveChild(criterionNode);
                }

                if (criterionNode.Parent != parentNode)
                {
                    parentNode.AddChild(criterionNode);
                }
            }

            foreach (var criterionNode in criterionNodes.ToArray())
            {
                var criterion = criterions.SingleOrDefault(x => x == criterionNode.Criterion);
                if (criterion == null)
                {
                    criterionNode.Parent.RemoveChild(criterionNode);
                }
            }
        }

        private void UpdateAlternativeNodes()
        {
            foreach (var alternative in Manager.Hierarchy.Alternatives)
            {
                if (AlternativesNode.Children.Cast<AlternativeNodeViewModel>().SingleOrDefault(x => x.Alternative == alternative) == null)
                {
                    var alternativeNode = new AlternativeNodeViewModel(this, alternative);
                    AlternativesNode.AddChild(alternativeNode);
                }
            }

            foreach (var alternativeNode in AlternativesNode.Children.Cast<AlternativeNodeViewModel>().ToArray())
            {
                var alternative = Manager.Hierarchy.Alternatives.SingleOrDefault(x => x == alternativeNode.Alternative);
                if (alternative == null)
                {
                    AlternativesNode.RemoveChild(alternativeNode);
                }
            }
        }
    }
}
