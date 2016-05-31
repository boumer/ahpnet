using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class PairwiseComparison
    {
        private readonly decimal[] RI = new decimal[] { 0, 0, 0.58M, 0.90M, 1.12M, 1.24M, 1.32M, 1.41M, 1.45M, 1.49M };

        private List<Node> _nodes = null;
        private decimal[,] _importances = null;
        private decimal[] _selfVector = null;
        private decimal[] _priorities = null;

        private decimal lMax;
        public decimal LMax
        {
            get { return lMax; }
        }

        public decimal CI
        {
            get { return (lMax - _nodes.Count) / (_nodes.Count - 1M); }
        }

        public decimal CR
        {
            get 
            {
                if (_nodes.Count > 10)
                {
                    throw new InvalidOperationException("Consistency Ratio can not be calculated");
                }

                return CI / RI[_nodes.Count - 1];
            }
        }

        public bool AutoCalculate { get; set; }

        public PairwiseComparison(IEnumerable<CriterionNode> criterions)
        {
            if (criterions == null)
            {
                throw new ArgumentNullException("criterions");
            }

            _nodes = new List<Node>();
            foreach (var criterion in criterions)
            {
                if (!_nodes.Contains(criterion))
                {
                    _nodes.Add(criterion);
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

            _nodes = new List<Node>();
            foreach (var alternative in alternatives)
            {
                if (!_nodes.Contains(alternative))
                {
                    _nodes.Add(alternative);
                }
            }

            CheckNodes();
            Init();
        }

        private void CheckNodes()
        {
            if (_nodes.Count < 2)
            {
                throw new InvalidOperationException("Minimum 2 unique nodes are expected for pairwise comparison.");
            }
        }

        private void Init()
        {
            _importances = new decimal[_nodes.Count, _nodes.Count];
            for (int i = 0; i < _nodes.Count; i++)
            {
                for (int j = 0; j < _nodes.Count; j++)
                {
                    _importances[i, j] = 1M;
                }
            }

            _selfVector = new decimal[_nodes.Count];
            _priorities = new decimal[_nodes.Count];
            for (int i = 0; i < _nodes.Count; i++)
            {
                _selfVector[i] = 1M;
                _priorities[i] = 1M / _nodes.Count;
            }

            lMax = _nodes.Count;

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

                if (!_nodes.Contains(node1))
                {
                    throw new KeyNotFoundException("Parameter node1 is not found in the nodes of the PairwiseComparison object.");
                }

                if (!_nodes.Contains(node2))
                {
                    throw new KeyNotFoundException("Parameter node2 is not found in the nodes of the PairwiseComparison object.");
                }

                int index1 = _nodes.IndexOf(node1);
                int index2 = _nodes.IndexOf(node2);

                return _importances[index1, index2];
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

                if (!_nodes.Contains(node1))
                {
                    throw new KeyNotFoundException("Parameter node1 is not found in the nodes of the PairwiseComparison object.");
                }

                if (!_nodes.Contains(node2))
                {
                    throw new KeyNotFoundException("Parameter node2 is not found in the nodes of the PairwiseComparison object.");
                }

                if (value < (1M / 9M) || value > 9M)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                int index1 = _nodes.IndexOf(node1);
                int index2 = _nodes.IndexOf(node2);
                if (index1 == index2)
                {
                    value = 1M;
                }

                _importances[index1, index2] = value;
                _importances[index2, index1] = 1 / value;

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

                if (!_nodes.Contains(node))
                {
                    throw new KeyNotFoundException("Parameter node is not found in the nodes of the PairwiseComparison object.");
                }

                int index = _nodes.IndexOf(node);

                return _priorities[index];
            }
        }

        public void Calculate()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                _selfVector[i] = 1M;
            }

            for (int i = 0; i < _nodes.Count; i++)
            {
                for (int j = 0; j < _nodes.Count; j++)
                {
                    _selfVector[i] *= _importances[i, j];
                }
            }

            decimal sum = 0M;
            for (int i = 0; i < _nodes.Count; i++)
            {
                _selfVector[i] = (decimal)Math.Pow((double)_selfVector[i], (double)(1M / _nodes.Count));
                sum += _selfVector[i];
            }

            for (int i = 0; i < _nodes.Count; i++)
            {
                _priorities[i] = _selfVector[i] / sum;
            }

            lMax = 0M;
            for (int i = 0; i < _nodes.Count; i++)
            {
                for (int j = 0; j < _nodes.Count; j++)
                {
                    lMax += _importances[i, j] * _priorities[j];
                }
            }
        }
    }
}
