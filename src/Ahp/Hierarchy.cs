using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Represents AHP hierarchy
    /// </summary>
    public class Hierarchy
    {
        public Hierarchy()
            : this("Goal")
        { }

        public Hierarchy(string goalName)
        {
            goalNode = new GoalNode(goalName);
            alternatives = new AlternativeCollection(HandleAlternativeAdded, HandleAlternativeRemoved);
        }
        
        private GoalNode goalNode;
        /// <summary>
        /// Root node of the hierarchy
        /// </summary>
        public GoalNode GoalNode
        {
            get { return goalNode; }
        }

        private AlternativeCollection alternatives;
        /// <summary>
        /// Collection of alternatives
        /// </summary>
        public AlternativeCollection Alternatives
        {
            get { return alternatives; }
        }

        /// <summary>
        /// Returns AlternativeNode object by Alternative and CriterionNode objects
        /// </summary>
        /// <param name="alternative"></param>
        /// <param name="criterion"></param>
        /// <returns></returns>
        public AlternativeNode this[Alternative alternative, CriterionNode criterion]
        {
            get
            {
                if (!Alternatives.Contains(alternative))
                {
                    throw new KeyNotFoundException(String.Format("Alternative '{0}' not found in the Altenatives collection of the hierarchy", alternative.Name));
                }

                ICollection<CriterionNode> nodes = GoalNode.GetLowestCriterionNodes();
                if (!nodes.Contains(criterion))
                {
                    throw new KeyNotFoundException(String.Format("Criterion node '{0}' not found in the criterion nodes which are at lowest level of the hierarchy", criterion.Name));
                }

                RefreshAlternativeNodes();

                AlternativeNode result = null;
                foreach (CriterionNode node in nodes)
                {
                    foreach (AlternativeNode alternativeNode in node.AlternativeNodes)
                    {
                        if (Object.ReferenceEquals(node, criterion) &&
                            Object.ReferenceEquals(alternativeNode.Alternative, alternative))
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
            foreach (CriterionNode criterionNode in GoalNode.GetLowestCriterionNodes())
            {
                foreach (Alternative alternative in Alternatives)
                {
                    if (!criterionNode.AlternativeNodes.Contains(alternative))
                    {
                        criterionNode.AlternativeNodes.Add(new AlternativeNode(alternative));
                    }
                }

                AlternativeNodeCollection toBeRemoved = new AlternativeNodeCollection();
                foreach (AlternativeNode alternativeNode in criterionNode.AlternativeNodes)
                {
                    if (!this.Alternatives.Contains(alternativeNode.Alternative))
                    {
                        toBeRemoved.Add(alternativeNode);
                    }
                }

                foreach (AlternativeNode alternativeNode in toBeRemoved)
                {
                    criterionNode.AlternativeNodes.Remove(alternativeNode);
                }
            }
        }

        private void HandleAlternativeAdded(Alternative alternative)
        {
            foreach (CriterionNode lowestCriterionNode in goalNode.GetLowestCriterionNodes())
            {
                lowestCriterionNode.AlternativeNodes.Add(new AlternativeNode(alternative));
            }
        }

        private void HandleAlternativeRemoved(Alternative alternative)
        {
            foreach (CriterionNode lowestCriterionNode in goalNode.GetLowestCriterionNodes())
            {
                AlternativeNode toBeDeleted = lowestCriterionNode.AlternativeNodes[alternative];
                lowestCriterionNode.AlternativeNodes.Remove(toBeDeleted);
            }
        }
    }
}
