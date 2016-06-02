using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ahp
{
    public class ReadOnlyNodeCollection : IEnumerable<Node>
    {
        private ICollection<Node> _list;

        public ReadOnlyNodeCollection(ICollection<Node> list)
        {
            _list = list;
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool Contains(Node node)
        {
            return _list.Contains(node);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }
    }

    public class AlternativeNodeCollection : IEnumerable<AlternativeNode>
    {
        private ReadOnlyNodeCollection _nodes;

        public AlternativeNodeCollection(ReadOnlyNodeCollection nodes)
        {
            _nodes = nodes;
        }

        public int Count
        {
            get { return _nodes.OfType<AlternativeNode>().Count(); }
        }

        public AlternativeNode this[Alternative alternative]
        {
            get { return _nodes.OfType<AlternativeNode>().SingleOrDefault(x => x.Alternative == alternative); }
        }

        public bool Contains(Alternative alternative)
        {
            return _nodes.OfType<AlternativeNode>().SingleOrDefault(x => x.Alternative == alternative) != null;
        }

        public IEnumerator<AlternativeNode> GetEnumerator()
        {
            return _nodes.OfType<AlternativeNode>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_nodes.OfType<AlternativeNode>()).GetEnumerator();
        }
    }

    public class CriterionNodeCollection : IEnumerable<CriterionNode>
    {
        private ReadOnlyNodeCollection _nodes;

        public CriterionNodeCollection(ReadOnlyNodeCollection nodes)
        {
            _nodes = nodes;
        }

        public int Count
        {
            get { return _nodes.OfType<CriterionNode>().Count(); }
        }

        public CriterionNode this[int index]
        {
            get { return _nodes.OfType<CriterionNode>().ElementAt(index); }
        }

        public IEnumerator<CriterionNode> GetEnumerator()
        {
            return _nodes.OfType<CriterionNode>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_nodes.OfType<CriterionNode>()).GetEnumerator();
        }
    }
}
