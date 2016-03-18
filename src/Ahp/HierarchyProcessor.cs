using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Processes hierarchy analysis
    /// </summary>
    public class HierarchyProcessor
    {
        public AnalysisResult Analyse(Hierarchy hierarchy)
        {
            AnalysisResult result = new AnalysisResult();
            foreach (Alternative alternative in hierarchy.Alternatives)
            {
                result.Add(alternative, 0M);
            }

            foreach (CriterionNode node in hierarchy.GoalNode.GetLowestCriterionNodes())
            {
                foreach (Alternative alternative in hierarchy.Alternatives)
                {
                    result[alternative] += hierarchy[alternative, node].GlobalPriority;
                }
            }

            return result;
        }
    }
}
