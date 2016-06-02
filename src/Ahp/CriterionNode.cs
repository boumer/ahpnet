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
        { }

        public GoalNode GoalNode
        {
            get { return ParentNode as GoalNode; }
            set
            {
                if (value == null && !(ParentNode is GoalNode))
                {
                    return;
                }

                ParentNode = value;
            }
        }

        public CriterionNode ParentCriterionNode
        {
            get { return ParentNode as CriterionNode; }
            set
            {
                if (value == null && !(ParentNode is CriterionNode))
                {
                    return;
                }

                ParentNode = value;
            }
        }

        public IEnumerable<CriterionNode> SubcriterionNodes
        {
            get
            {
                foreach (var node in ChildNodes)
                {
                    var subcriterionNode = node as CriterionNode;
                    if (subcriterionNode != null)
                    {
                        yield return subcriterionNode;
                    }
                }
            }
        }

        public IEnumerable<AlternativeNode> AlternativeNodes
        {
            get
            {
                foreach (var node in ChildNodes)
                {
                    var alternativeNode = node as AlternativeNode;
                    if (alternativeNode != null)
                    {
                        yield return alternativeNode;
                    }
                }
            }
        }

        public bool HasSubcriterionNodes
        {
            get { return SubcriterionNodes.GetEnumerator().MoveNext(); }
        }

        public bool HasAlternativeNodes
        {
            get { return AlternativeNodes.GetEnumerator().MoveNext(); }
        }

        public CriterionNode AddSubcriterionNode(string name)
        {
            return AddSubcriterionNode(name, 0M);
        }

        public CriterionNode AddSubcriterionNode(string name, decimal weight)
        {
            var node = new CriterionNode(name, weight);
            AddSubcriterionNode(node);

            return node;
        }

        public void AddSubcriterionNode(CriterionNode node)
        {
            if (HasAlternativeNodes)
            {
                ChildNodes.Clear();
            }

            ChildNodes.Add(node);
        }

        public void RemoveSubcriterionNode(CriterionNode node)
        {
            ChildNodes.Remove(node);
        }

        public AlternativeNode GetAlternativeNode(Alternative alternative)
        {
            foreach (var alternativeNode in AlternativeNodes)
            {
                if (alternativeNode.Alternative == alternative)
                {
                    return alternativeNode;
                }
            }

            return null;
        }

        public void AddAlternativeNode(AlternativeNode node)
        {
            if (HasSubcriterionNodes)
            {
                ChildNodes.Clear();
            }

            ChildNodes.Add(node);
        }

        public bool ContainsAlternativeNode(Alternative alternative)
        {
            foreach (var alternativeNode in AlternativeNodes)
            {
                if (alternativeNode.Alternative == alternative)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveAlternativeNode(AlternativeNode node)
        {
            ChildNodes.Remove(node);
        }
    }
}
