using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class PairwiseComparison
    {
        private readonly decimal[] RI = new decimal[] { 0, 0, 0.58M, 0.90M, 1.12M, 1.24M, 1.32M, 1.41M, 1.45M, 1.49M };

        private List<Node> nodes = null;
        private decimal[,] importances = null;
        private decimal[] selfVector = null;
        private decimal[] priorities = null;

        private decimal lMax;
        public decimal LMax
        {
            get { return lMax; }
        }

        public decimal CI
        {
            get { return (lMax - nodes.Count) / (nodes.Count - 1M); }
        }

        public decimal CR
        {
            get 
            {
                if (nodes.Count > 10)
                {
                    throw new InvalidOperationException("Consistency Ratio can not be calculated");
                }

                return CI / RI[nodes.Count - 1];
            }
        }

        public bool AutoCalculate { get; set; }

        public PairwiseComparison(IEnumerable<CriterionNode> criterions)
        {
            if (criterions == null)
            {
                throw new ArgumentNullException("criterions");
            }

            nodes = new List<Node>();
            foreach (var criterion in criterions)
            {
                if (!nodes.Contains(criterion))
                {
                    nodes.Add(criterion);
                }
            }

            CheckNodes();
            Init();
        }

        public PairwiseComparison(IEnumerable<AlternativeNode> alternatives)
        {
            if (alternatives == null)
            {
                throw new ArgumentNullException("alternatives");
            }

            nodes = new List<Node>();
            foreach (var alternative in alternatives)
            {
                if (!nodes.Contains(alternative))
                {
                    nodes.Add(alternative);
                }
            }

            CheckNodes();
            Init();
        }

        private void CheckNodes()
        {
            if (nodes.Count < 2)
            {
                throw new InvalidOperationException("Minimum 2 unique nodes are expected for pairwise comparison.");
            }
        }

        private void Init()
        {
            importances = new decimal[nodes.Count, nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    importances[i, j] = 1M;
                }
            }

            selfVector = new decimal[nodes.Count];
            priorities = new decimal[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                selfVector[i] = 1M;
                priorities[i] = 1M / nodes.Count;
            }

            lMax = nodes.Count;

            AutoCalculate = true;
        }

        public decimal this[Node node1, Node node2]
        {
            get
            {
                if (node1 == null)
                {
                    throw new ArgumentNullException("node1");
                }

                if (node2 == null)
                {
                    throw new ArgumentNullException("node2");
                }

                if (!nodes.Contains(node1))
                {
                    throw new KeyNotFoundException("Parameter node1 is not found in the nodes of the PairwiseComparison object.");
                }

                if (!nodes.Contains(node2))
                {
                    throw new KeyNotFoundException("Parameter node2 is not found in the nodes of the PairwiseComparison object.");
                }

                int index1 = nodes.IndexOf(node1);
                int index2 = nodes.IndexOf(node2);

                return importances[index1, index2];
            }
            set
            {
                if (node1 == null)
                {
                    throw new ArgumentNullException("node1");
                }

                if (node2 == null)
                {
                    throw new ArgumentNullException("node2");
                }

                if (!nodes.Contains(node1))
                {
                    throw new KeyNotFoundException("Parameter node1 is not found in the nodes of the PairwiseComparison object.");
                }

                if (!nodes.Contains(node2))
                {
                    throw new KeyNotFoundException("Parameter node2 is not found in the nodes of the PairwiseComparison object.");
                }

                if (value < (1M / 9M) || value > 9M)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                int index1 = nodes.IndexOf(node1);
                int index2 = nodes.IndexOf(node2);
                if (index1 == index2)
                {
                    value = 1M;
                }

                importances[index1, index2] = value;
                importances[index2, index1] = 1 / value;

                if (AutoCalculate)
                {
                    Calculate();
                }
            }
        }

        public decimal this[Node node]
        {
            get
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }

                if (!nodes.Contains(node))
                {
                    throw new KeyNotFoundException("Parameter node is not found in the nodes of the PairwiseComparison object.");
                }

                int index = nodes.IndexOf(node);

                return priorities[index];
            }
        }

        public void Calculate()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                selfVector[i] = 1M;
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    selfVector[i] *= importances[i, j];
                }
            }

            decimal sum = 0M;
            for (int i = 0; i < nodes.Count; i++)
            {
                selfVector[i] = (decimal)Math.Pow((double)selfVector[i], (double)(1M / nodes.Count));
                sum += selfVector[i];
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                priorities[i] = selfVector[i] / sum;
            }

            lMax = 0M;
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    lMax += importances[i, j] * priorities[j];
                }
            }
        }
    }
}
