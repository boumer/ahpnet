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
        
        public override decimal GlobalPriority
        {
            get
            {
                if (!object.ReferenceEquals(null, _parentCriterionNode))
                {
                    return this.LocalPriority * _parentCriterionNode.GlobalPriority;
                }
                else
                {
                    return this.LocalPriority;
                }
            }
        }

        private GoalNode _goalNode;
        public GoalNode GoalNode
        {
            get { return _goalNode; }
            set
            {
                if (_goalNode != null)
                {
                    this.GoalNode.CriterionNodes.Remove(this);
                }

                _goalNode = value;
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

        private CriterionNode _parentCriterionNode;
        public CriterionNode ParentCriterionNode
        {
            get { return _parentCriterionNode; }
            set
            {
                if (_parentCriterionNode != null)
                {
                    _parentCriterionNode.SubcriterionNodes.Remove(this);
                }

                _parentCriterionNode = value;

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
