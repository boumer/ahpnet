using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class Hierarchy
    {
        public Hierarchy()
            : this("Goal")
        { }

        public Hierarchy(string goalName)
        {
            _goalNode = new GoalNode(goalName);
            _alternatives = new AlternativeCollection(HandleAlternativeAdded, HandleAlternativeRemoved);
        }

        private GoalNode _goalNode;
        public GoalNode GoalNode
        {
            get { return _goalNode; }
        }

        private AlternativeCollection _alternatives;
        public AlternativeCollection Alternatives
        {
            get { return _alternatives; }
        }

        public AlternativeNode this[Alternative alternative, CriterionNode criterion]
        {
            get
            {
                if (!Alternatives.Contains(alternative))
                {
                    throw new KeyNotFoundException(String.Format("Alternative '{0}' not found in the Altenatives collection of the hierarchy", alternative.Name));
                }

                var nodes = GetLowestCriterionNodes();
                if (!nodes.Contains(criterion))
                {
                    throw new KeyNotFoundException(String.Format("Criterion node '{0}' not found in the criterion nodes which are at lowest level of the hierarchy", criterion.Name));
                }

                RefreshAlternativeNodes();

                AlternativeNode result = null;
                foreach (var node in nodes)
                {
                    foreach (var alternativeNode in node.AlternativeNodes)
                    {
                        if (object.ReferenceEquals(node, criterion) &&
                            object.ReferenceEquals(alternativeNode.Alternative, alternative))
                        {
                            result = alternativeNode;
                        }
                    }
                }

                return result;
            }
        }

        public void RefreshAlternativeNodes()
        {
            foreach (var criterionNode in GetLowestCriterionNodes())
            {
                foreach (var alternative in Alternatives)
                {
                    if (!criterionNode.AlternativeNodes.Contains(alternative))
                    {
                        criterionNode.AlternativeNodes.Add(new AlternativeNode(alternative));
                    }
                }

                var toBeRemoved = new AlternativeNodeCollection();
                foreach (var alternativeNode in criterionNode.AlternativeNodes)
                {
                    if (!this.Alternatives.Contains(alternativeNode.Alternative))
                    {
                        toBeRemoved.Add(alternativeNode);
                    }
                }

                foreach (var alternativeNode in toBeRemoved)
                {
                    criterionNode.AlternativeNodes.Remove(alternativeNode);
                }
            }
        }

        public ICollection<CriterionNode> GetLowestCriterionNodes()
        {
            var nodes = new List<CriterionNode>();

            foreach (var criterionNode in GoalNode.CriterionNodes)
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

        private void HandleAlternativeAdded(Alternative alternative)
        {
            foreach (var lowestCriterionNode in GetLowestCriterionNodes())
            {
                lowestCriterionNode.AlternativeNodes.Add(new AlternativeNode(alternative));
            }
        }

        private void HandleAlternativeRemoved(Alternative alternative)
        {
            foreach (var lowestCriterionNode in GetLowestCriterionNodes())
            {
                var toBeDeleted = lowestCriterionNode.AlternativeNodes[alternative];
                lowestCriterionNode.AlternativeNodes.Remove(toBeDeleted);
            }
        }
    }
}
