﻿using System;
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

        public GoalNodeViewModel GoalNode { get; private set; }
        public AlternativesNodeViewModel AlternativesNode { get; private set; }

        public HierarchyViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

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
    }
}
