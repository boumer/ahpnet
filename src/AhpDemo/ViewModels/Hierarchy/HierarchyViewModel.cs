using Ahp;
using AhpDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            set { SetProperty(ref _selectedNode, value); }
        }

        private readonly ObservableCollection<HierarchyNodeViewModel> _nodes = new ObservableCollection<HierarchyNodeViewModel>();
        public ObservableCollection<HierarchyNodeViewModel> Nodes
        {
            get { return _nodes; }
        }

        private void InitializeNodes()
        {
            GoalNode = new GoalNodeViewModel(this);
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
