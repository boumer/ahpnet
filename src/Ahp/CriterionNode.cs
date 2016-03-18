using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Represents criterion node in AHP hierarchy
    /// </summary>
    public class CriterionNode : Node
    {
        public CriterionNode()
            : this(null, 0M)
        { }

        public CriterionNode(string name)
            : this(name, 0M)
        { }

        public CriterionNode(string name, decimal localPriority)
            : base(name, localPriority)
        {
            subcriterionNodes = new CriterionNodeCollection(HandleChildCriterionAdded, HandleChildCriterionRemoved);
            alternativeNodes = new AlternativeNodeCollection(HandleAlternativeAdded, HandleAlternativeRemoved);
        }
        
        /// <summary>
        /// Global priority of the node
        /// </summary>
        public override decimal GlobalPriority
        {
            get
            {
                if (!Object.ReferenceEquals(null, parentCriterionNode))
                {
                    return this.LocalPriority * parentCriterionNode.GlobalPriority;
                }
                else
                {
                    return this.LocalPriority;
                }
            }
        }

        private GoalNode goalNode;
        /// <summary>
        /// Parent goal node
        /// </summary>
        public GoalNode GoalNode
        {
            get { return goalNode; }
            set
            {
                if (goalNode != null)
                {
                    this.GoalNode.CriterionNodes.Remove(this);
                }

                goalNode = value;
                if (value != null)
                {
                    if (!value.CriterionNodes.Contains(this))
                    {
                        value.CriterionNodes.Add(this);
                    }
                    ParentCriterionNode = null;
                }
            }
        }

        private CriterionNode parentCriterionNode;
        /// <summary>
        /// Parent criterion node
        /// </summary>
        public CriterionNode ParentCriterionNode
        {
            get { return parentCriterionNode; }
            set
            {
                if (parentCriterionNode != null)
                {
                    parentCriterionNode.SubcriterionNodes.Remove(this);
                }

                parentCriterionNode = value;
                if (value != null)
                {
                    if (!value.SubcriterionNodes.Contains(this))
                    {
                        value.SubcriterionNodes.Add(this);
                    }
                    GoalNode = null;
                }
            }
        }

        private CriterionNodeCollection subcriterionNodes;
        /// <summary>
        /// Collection of child subcriterion nodes
        /// </summary>
        public CriterionNodeCollection SubcriterionNodes
        {
            get { return subcriterionNodes; }
        }

        private AlternativeNodeCollection alternativeNodes;
        /// <summary>
        /// Collection of child alternative nodes
        /// </summary>
        public AlternativeNodeCollection AlternativeNodes
        {
            get { return alternativeNodes; }
        }

        public bool HasSubcriterionNodes
        {
            get { return subcriterionNodes.Count > 0; }
        }

        public bool HasAlternativeNodes
        {
            get { return alternativeNodes.Count > 0; }
        }

        #region Bidirectional associations fix-up

        private void HandleChildCriterionAdded(CriterionNode node)
        {
            if (!Object.ReferenceEquals(node.ParentCriterionNode, this))
            {
                node.ParentCriterionNode = this;
            }
            this.AlternativeNodes.Clear();
        }

        private void HandleChildCriterionRemoved(CriterionNode node)
        {
            if (Object.ReferenceEquals(node.ParentCriterionNode, this))
            {
                node.ParentCriterionNode = null;
            }
        }

        private void HandleAlternativeAdded(AlternativeNode node)
        {
            if (!Object.ReferenceEquals(node.CriterionNode, this))
            {
                node.CriterionNode = this;
            }
            this.SubcriterionNodes.Clear();
        }

        private void HandleAlternativeRemoved(AlternativeNode node)
        {
            if (Object.ReferenceEquals(node.CriterionNode, this))
            {
                node.CriterionNode = null;
            }
        }

        #endregion
    }
}
