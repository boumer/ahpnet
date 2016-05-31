using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class GoalNode : Node
    {
        public GoalNode()
            : this(null)
        { }

        public GoalNode(string name)
            : base(name, 1M)
        {
            _criterionNodes = new CriterionNodeCollection(HandleChildAdded, HandleChildRemoved);
        }

        public override decimal LocalPriority
        {
            get { return 1M; }
            set { throw new InvalidOperationException("Changing local priority of the GoalNode is not allowed. Its value always equals 1."); }
        }

        public override decimal GlobalPriority
        {
            get { return 1M; }
        }

        private CriterionNodeCollection _criterionNodes;
        public CriterionNodeCollection CriterionNodes
        {
            get { return _criterionNodes; }
        }

        public ICollection<CriterionNode> GetLowestCriterionNodes()
        {
            var nodes = new List<CriterionNode>();

            foreach (var criterionNode in _criterionNodes)
            {
                LookForLowestCriterionNodes(criterionNode, nodes);
            }

            return nodes;
        }

        private void LookForLowestCriterionNodes(CriterionNode node, ICollection<CriterionNode> nodes)
        {
            if (node.HasSubcriterionNodes)
            {
                foreach (var subcriterionNode in node.SubcriterionNodes)
                {
                    LookForLowestCriterionNodes(subcriterionNode, nodes);
                }
            }
            else
            {
                nodes.Add(node);
            }
        }

        private void HandleChildAdded(CriterionNode node)
        {
            if (!object.ReferenceEquals(node.GoalNode, this))
            {
                node.GoalNode = this;
            }
        }

        private void HandleChildRemoved(CriterionNode node)
        {
            if (object.ReferenceEquals(node.GoalNode, this))
            {
                node.GoalNode = null;
            }
        }
    }
}
