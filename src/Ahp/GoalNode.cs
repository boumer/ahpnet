using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ahp
{
    public class GoalNode : Node
    {
        public GoalNode(Hierarchy hierarchy)
            : this(hierarchy, null)
        { }

        public GoalNode(Hierarchy hierarchy, string name)
            : base(name, 1M)
        {

            if (hierarchy == null)
            {
                throw new ArgumentNullException("hierarchy");
            }

            if (hierarchy.GoalNode != null)
            {
                throw new ArgumentException("Uninitialized hierarchy with null GoalNode expected.");
            }

            Hierarchy = hierarchy;
            _criterionNodes = new CriterionNodeCollection(ChildNodes);
        }

        public Hierarchy Hierarchy { get; private set; }

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

        private CriterionNodeCollection _criterionNodes;
        public CriterionNodeCollection CriterionNodes
        {
            get { return _criterionNodes; }
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
            AddChildNode(node);
            node.RefreshAlternativeNodes();
        }

        public void RemoveCriterionNode(CriterionNode node)
        {
            RemoveChildNode(node);
        }
    }
}
