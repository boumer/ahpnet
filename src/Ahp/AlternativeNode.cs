using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    public class AlternativeNode : Node
    {
        public AlternativeNode(Alternative alternative)
            : this(alternative, 0M)
        { }

        public AlternativeNode(Alternative alternative, decimal localPriority)
            : base(null, localPriority)
        {
            if (alternative == null)
            {
                throw new ArgumentNullException("alternative");
            }

            _alternative = alternative;
        }

        public override string Name
        {
            get { return _alternative.Name; }
            set { throw new InvalidOperationException("Changing Name property of AlternativeNode object directly is not allowed. Change Name property of correspondent Alternative object instead."); }
        }

        private Alternative _alternative;
        public Alternative Alternative
        {
            get { return _alternative; }
        }

        public CriterionNode CriterionNode
        {
            get { return ParentNode as CriterionNode; }
            set
            {
                if (CriterionNode != null)
                {
                    CriterionNode.AlternativeNodes.Remove(this);
                }

                ParentNode = value;

                if (value != null && !value.AlternativeNodes.Contains(this))
                {
                    value.AlternativeNodes.Add(this);
                }                
            }
        }
    }
}
