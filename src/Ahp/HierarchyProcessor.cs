using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class HierarchyProcessor
    {
        public AnalysisResult Analyse(Hierarchy hierarchy)
        {
            var result = new AnalysisResult();
            foreach (var alternative in hierarchy.Alternatives)
            {
                result.Add(alternative, 0M);
            }

            foreach (var node in hierarchy.GetLowestCriterionNodes())
            {
                foreach (var alternative in hierarchy.Alternatives)
                {
                    result[alternative] += node.AlternativeNodes[alternative].GlobalPriority;
                }
            }

            return result;
        }
    }
}
