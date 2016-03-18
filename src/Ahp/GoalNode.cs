using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Represents AHP goal node. This is root node of the hierarchy.
    /// </summary>
    public class GoalNode : Node
    {
        public GoalNode()
            : this(null)
        { }

        public GoalNode(string name)
            : base(name, 1M)
        {
            criterionNodes = new CriterionNodeCollection(HandleChildAdded, HandleChildRemoved);
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

        private CriterionNodeCollection criterionNodes;
        /// <summary>
        /// Colletion of child criterion nodes
        /// </summary>
        public CriterionNodeCollection CriterionNodes
        {
            get { return criterionNodes; }
        }

        /// <summary>
        /// Returns array of criterion nodes, which are at lowest level in the hierarchy
        /// </summary>
        /// <returns></returns>
        public ICollection<CriterionNode> GetLowestCriterionNodes()
        {
            List<CriterionNode> nodes = new List<CriterionNode>();

            foreach (CriterionNode criterionNode in criterionNodes)
            {
                LookForLowestCriterionNodes(criterionNode, nodes);
            }

            return nodes;
        }

        private void LookForLowestCriterionNodes(CriterionNode node, ICollection<CriterionNode> nodes)
        {
            if (node.HasSubcriterionNodes)
            {
                foreach (CriterionNode subcriterionNode in node.SubcriterionNodes)
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
            if (!Object.ReferenceEquals(node.GoalNode, this))
            {
                node.GoalNode = this;
            }
        }

        private void HandleChildRemoved(CriterionNode node)
        {
            if (Object.ReferenceEquals(node.GoalNode, this))
            {
                node.GoalNode = null;
            }
        }
    }
}
