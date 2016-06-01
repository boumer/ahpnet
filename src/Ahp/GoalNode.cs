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
        { }

        public override decimal LocalPriority
        {
            get { return 1M; }
            set { throw new InvalidOperationException("Setting local priority for the GoalNode is not allowed. Its value always equals 1."); }
        }

        protected override Node ParentNode
        {
            get { return null; }
            set { throw new InvalidOperationException("Setting ParentNode for the GoalNode is not allowed. Its value is always null."); }
        }

        public IEnumerable<CriterionNode> CriterionNodes
        {
            get
            {
                foreach (var child in ChildNodes)
                {
                    yield return (CriterionNode)child;
                }
            }
        }

        public CriterionNode AddCriterionNode(string name)
        {
            return AddCriterionNode(name, 0M);
        }

        public CriterionNode AddCriterionNode(string name, decimal weight)
        {
            var node = new CriterionNode(name, weight);
            AddCriterionNode(node);

            return node;
        }

        public void AddCriterionNode(CriterionNode node)
        {
            ChildNodes.Add(node);
        }

        public void RemoveCriterionNode(CriterionNode node)
        {
            ChildNodes.Remove(node);
        }
    }
}
