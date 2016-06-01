using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
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
            _subcriterionNodes = new CriterionNodeCollection(HandleChildCriterionAdded, HandleChildCriterionRemoved);
            _alternativeNodes = new AlternativeNodeCollection(HandleAlternativeAdded, HandleAlternativeRemoved);
        }

        public GoalNode GoalNode
        {
            get { return ParentNode as GoalNode; }
            set { ParentNode = value; }
        }

        public CriterionNode ParentCriterionNode
        {
            get { return ParentNode as CriterionNode; }
            set
            {
                if (ParentCriterionNode != null)
                {
                    ParentCriterionNode.SubcriterionNodes.Remove(this);
                }

                ParentNode = value;

                if (value != null)
                {
                    if (!value.SubcriterionNodes.Contains(this))
                    {
                        value.SubcriterionNodes.Add(this);
                    }                    
                }
            }
        }

        private CriterionNodeCollection _subcriterionNodes;
        public CriterionNodeCollection SubcriterionNodes
        {
            get { return _subcriterionNodes; }
        }

        private AlternativeNodeCollection _alternativeNodes;
        public AlternativeNodeCollection AlternativeNodes
        {
            get { return _alternativeNodes; }
        }

        public bool HasSubcriterionNodes
        {
            get { return _subcriterionNodes.Count > 0; }
        }

        public bool HasAlternativeNodes
        {
            get { return _alternativeNodes.Count > 0; }
        }

        #region Bidirectional associations fixup

        private void HandleChildCriterionAdded(CriterionNode node)
        {
            if (!object.ReferenceEquals(node.ParentCriterionNode, this))
            {
                node.ParentCriterionNode = this;
            }
            this.AlternativeNodes.Clear();
        }

        private void HandleChildCriterionRemoved(CriterionNode node)
        {
            if (object.ReferenceEquals(node.ParentCriterionNode, this))
            {
                node.ParentCriterionNode = null;
            }
        }

        private void HandleAlternativeAdded(AlternativeNode node)
        {
            if (!object.ReferenceEquals(node.CriterionNode, this))
            {
                node.CriterionNode = this;
            }
            this.SubcriterionNodes.Clear();
        }

        private void HandleAlternativeRemoved(AlternativeNode node)
        {
            if (object.ReferenceEquals(node.CriterionNode, this))
            {
                node.CriterionNode = null;
            }
        }

        #endregion
    }
}
