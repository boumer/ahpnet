using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ahp
{
    public class Hierarchy
    {
        public Hierarchy()
            : this("Goal")
        { }

        public Hierarchy(string goalName)
        {
            _goalNode = new GoalNode(this, goalName);
            _alternatives = new List<Alternative>();
        }

        private GoalNode _goalNode;
        public GoalNode GoalNode
        {
            get { return _goalNode; }
        }

        private List<Alternative> _alternatives;
        public IEnumerable<Alternative> Alternatives
        {
            get { return _alternatives; }
        }

        public AlternativeNode this[Alternative alternative, CriterionNode criterion]
        {
            get
            {
                if (!_alternatives.Contains(alternative))
                {
                    throw new KeyNotFoundException(string.Format("Alternative '{0}' not found in the Altenatives collection of the hierarchy", alternative.Name));
                }

                var nodes = GetLowestCriterionNodes();
                if (!nodes.Contains(criterion))
                {
                    throw new KeyNotFoundException(string.Format("Criterion node '{0}' not found in the criterion nodes which are at lowest level of the hierarchy", criterion.Name));
                }

                RefreshAlternativeNodes();

                return criterion.AlternativeNodes.Single(x => x.Alternative == alternative);
            }
        }

        public Alternative AddAlternative(string name)
        {
            return AddAlternative(Guid.NewGuid().ToString(), name);
        }

        public Alternative AddAlternative(string id, string name)
        {
            var alternative = new Alternative(id, name);
            AddAlternative(alternative);

            return alternative;
        }

        public void AddAlternative(Alternative alternative)
        {
            if (_alternatives.Contains(alternative))
            {
                throw new ArgumentException("Same alternative can not be added twice.");
            }

            _alternatives.Add(alternative);

            foreach (var lowestCriterionNode in GetLowestCriterionNodes())
            {
                lowestCriterionNode.AddAlternativeNode(new AlternativeNode(alternative));
            }
        }

        public void RemoveAlternative(Alternative alternative)
        {
            _alternatives.Remove(alternative);

            foreach (var lowestCriterionNode in GetLowestCriterionNodes())
            {
                var toBeDeleted = lowestCriterionNode.GetAlternativeNode(alternative);
                lowestCriterionNode.RemoveAlternativeNode(toBeDeleted);
            }
        }

        public void RefreshAlternativeNodes()
        {
            foreach (var criterionNode in GetLowestCriterionNodes())
            {
                foreach (var alternative in Alternatives)
                {
                    if (!criterionNode.ContainsAlternativeNode(alternative))
                    {
                        criterionNode.AddAlternativeNode(new AlternativeNode(alternative));
                    }
                }

                var alternativeNodes = new List<AlternativeNode>(criterionNode.AlternativeNodes);
                foreach (var alternativeNode in alternativeNodes)
                {
                    if (!_alternatives.Contains(alternativeNode.Alternative))
                    {
                        criterionNode.RemoveAlternativeNode(alternativeNode);
                    }
                }
            }
        }

        public ICollection<CriterionNode> GetLowestCriterionNodes()
        {
            return GoalNode.SearchNodes<CriterionNode>((node) => !node.HasSubcriterionNodes);
        }
    }
}
