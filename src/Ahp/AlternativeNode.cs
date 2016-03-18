using System;
using System.Collections.Generic;
using System.Text;

namespace Ahp
{
    /// <summary>
    /// Represents alternative node in AHP hierarchy
    /// </summary>
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

            this.alternative = alternative;
        }
        
        /// <summary>
        /// Node name
        /// </summary>
        public override string Name
        {
            get { return alternative.Name; }
            set { throw new InvalidOperationException("Changing Name property of AlternativeNode object directly is not allowed. Change Name property of correspondent Alternative object instead."); }
        }

        /// <summary>
        /// Global priority of the node
        /// </summary>
        public override decimal GlobalPriority
        {
            get
            {
                if (!Object.ReferenceEquals(null, criterionNode))
                {
                    return this.LocalPriority * criterionNode.GlobalPriority;
                }
                else
                {
                    return this.LocalPriority;
                }
            }
        }

        private Alternative alternative;
        /// <summary>
        /// Correspondent alternative object
        /// </summary>        
        public Alternative Alternative
        {
            get { return alternative; }
        }

        private CriterionNode criterionNode;
        /// <summary>
        /// Parent criterion Node
        /// </summary>
        public CriterionNode CriterionNode
        {
            get { return criterionNode; }
            set
            {
                if (criterionNode != null)
                {
                    criterionNode.AlternativeNodes.Remove(this);
                }

                criterionNode = value;
                if (value != null && !value.AlternativeNodes.Contains(this))
                {
                    value.AlternativeNodes.Add(this);
                }
            }
        }
    }
}
