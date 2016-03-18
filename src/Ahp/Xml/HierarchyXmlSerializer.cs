using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Ahp.Xml
{
    /// <summary>
    /// Serializes Hierarchy object to XML
    /// </summary>
    public class HierarchyXmlSerializer
    {
        public string Serialize(Hierarchy hierarchy)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("hierarchy");

                    SerializeAlternatives(hierarchy.Alternatives, xmlWriter);
                    SerializeGoalNode(hierarchy.GoalNode, xmlWriter);

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }
            }

            return stringBuilder.ToString();
        }

        private static void SerializeAlternatives(AlternativeCollection alternatives, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("alternatives");
            foreach (Alternative alternative in alternatives)
            {
                xmlWriter.WriteStartElement("alternative");
                xmlWriter.WriteElementString("ID", alternative.ID);
                xmlWriter.WriteElementString("name", alternative.Name);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        private void SerializeGoalNode(GoalNode goalNode, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("goalNode");

            xmlWriter.WriteElementString("name", goalNode.Name);
            xmlWriter.WriteElementString("localPriority", goalNode.LocalPriority.ToString());
            SerializeCriterionNodes(goalNode.CriterionNodes, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void SerializeCriterionNodes(CriterionNodeCollection criterionNodes, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("criterionNodes");
            foreach (CriterionNode criterionNode in criterionNodes)
            {
                xmlWriter.WriteStartElement("criterionNode");

                xmlWriter.WriteElementString("name", criterionNode.Name);
                xmlWriter.WriteElementString("localPriority", criterionNode.LocalPriority.ToString());
                if (criterionNode.HasSubcriterionNodes)
                {
                    SerializeSubcriterionNodes(criterionNode.SubcriterionNodes, xmlWriter);
                }
                else if (criterionNode.HasAlternativeNodes)
                {
                    SerializeAlternativeNodes(criterionNode.AlternativeNodes, xmlWriter);
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        private void SerializeSubcriterionNodes(CriterionNodeCollection subcriterionNodes, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("subcriterionNodes");
            foreach (CriterionNode subcriterionNode in subcriterionNodes)
            {
                xmlWriter.WriteStartElement("criterionNode");

                xmlWriter.WriteElementString("name", subcriterionNode.Name);
                xmlWriter.WriteElementString("localPriority", subcriterionNode.LocalPriority.ToString());
                if (subcriterionNode.HasSubcriterionNodes)
                {
                    SerializeSubcriterionNodes(subcriterionNode.SubcriterionNodes, xmlWriter);
                }
                else if (subcriterionNode.HasAlternativeNodes)
                {
                    SerializeAlternativeNodes(subcriterionNode.AlternativeNodes, xmlWriter);
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        private void SerializeAlternativeNodes(AlternativeNodeCollection alternativeNodes, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("alternativeNodes");
            foreach (AlternativeNode alternativeNode in alternativeNodes)
            {
                xmlWriter.WriteStartElement("alternativeNode");

                xmlWriter.WriteElementString("alternativeID", alternativeNode.Alternative.ID);
                xmlWriter.WriteElementString("localPriority", alternativeNode.LocalPriority.ToString());

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }
    }
}
