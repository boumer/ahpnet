using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

using Ahp;
using Ahp.Xml;

namespace Ahp.Tests
{
    [TestClass]
    public class HierarchyXmlSerializerTests
    {
        [TestMethod]
        public void HierarchyXmlSerializer_Serialize_ReturnsCorrectXml()
        {
            //Arrange
            
            #region Hierarchy

            Hierarchy hierarchy = new Hierarchy("Choose optimal car");

            Alternative accord = hierarchy.Alternatives.Add("A", "Accord");
            Alternative accordHybrid = hierarchy.Alternatives.Add("AH", "Accord Hybrid");
            Alternative civic = hierarchy.Alternatives.Add("C", "Civic");

            CriterionNode style = hierarchy.GoalNode.CriterionNodes.Add("Style", 0.4M);
            CriterionNode capacity = hierarchy.GoalNode.CriterionNodes.Add("Capacity", 0.6M);

            CriterionNode cargoCapacity = capacity.SubcriterionNodes.Add("Cargo capacity", 0.2M);
            CriterionNode passengerCapacity = capacity.SubcriterionNodes.Add("Passenger capacity", 0.8M);

            hierarchy[accord, style].LocalPriority = 0.350M;
            hierarchy[accordHybrid, style].LocalPriority = 0.350M;
            hierarchy[civic, style].LocalPriority = 0.300M;

            hierarchy[accord, cargoCapacity].LocalPriority = 0.100M;
            hierarchy[accordHybrid, cargoCapacity].LocalPriority = 0.100M;
            hierarchy[civic, cargoCapacity].LocalPriority = 0.800M;

            hierarchy[accord, passengerCapacity].LocalPriority = 0.150M;
            hierarchy[accordHybrid, passengerCapacity].LocalPriority = 0.150M;
            hierarchy[civic, passengerCapacity].LocalPriority = 0.700M;

            #endregion

            #region Expected XML

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-16\"?>");
            stringBuilder.Append("<hierarchy>");

            stringBuilder.Append("<alternatives>");
            stringBuilder.Append("<alternative>");
            stringBuilder.Append("<ID>A</ID>");
            stringBuilder.Append("<name>Accord</name>");
            stringBuilder.Append("</alternative>");
            stringBuilder.Append("<alternative>");
            stringBuilder.Append("<ID>AH</ID>");
            stringBuilder.Append("<name>Accord Hybrid</name>");
            stringBuilder.Append("</alternative>");
            stringBuilder.Append("<alternative>");
            stringBuilder.Append("<ID>C</ID>");
            stringBuilder.Append("<name>Civic</name>");
            stringBuilder.Append("</alternative>");
            stringBuilder.Append("</alternatives>");

            stringBuilder.Append("<goalNode>");
            stringBuilder.Append("<name>Choose optimal car</name>");
            stringBuilder.Append("<localPriority>1</localPriority>");

            stringBuilder.Append("<criterionNodes>");

            stringBuilder.Append("<criterionNode>");
            stringBuilder.Append("<name>Style</name>");
            stringBuilder.Append("<localPriority>0,4</localPriority>");
            stringBuilder.Append("<alternativeNodes>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>A</alternativeID>");
            stringBuilder.Append("<localPriority>0,350</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>AH</alternativeID>");
            stringBuilder.Append("<localPriority>0,350</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>C</alternativeID>");
            stringBuilder.Append("<localPriority>0,300</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("</alternativeNodes>");
            stringBuilder.Append("</criterionNode>");

            stringBuilder.Append("<criterionNode>");
            stringBuilder.Append("<name>Capacity</name>");
            stringBuilder.Append("<localPriority>0,6</localPriority>");
            stringBuilder.Append("<subcriterionNodes>");

            stringBuilder.Append("<criterionNode>");
            stringBuilder.Append("<name>Cargo capacity</name>");
            stringBuilder.Append("<localPriority>0,2</localPriority>");
            stringBuilder.Append("<alternativeNodes>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>A</alternativeID>");
            stringBuilder.Append("<localPriority>0,100</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>AH</alternativeID>");
            stringBuilder.Append("<localPriority>0,100</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>C</alternativeID>");
            stringBuilder.Append("<localPriority>0,800</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("</alternativeNodes>");
            stringBuilder.Append("</criterionNode>");

            stringBuilder.Append("<criterionNode>");
            stringBuilder.Append("<name>Passenger capacity</name>");
            stringBuilder.Append("<localPriority>0,8</localPriority>");
            stringBuilder.Append("<alternativeNodes>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>A</alternativeID>");
            stringBuilder.Append("<localPriority>0,150</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>AH</alternativeID>");
            stringBuilder.Append("<localPriority>0,150</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("<alternativeNode>");
            stringBuilder.Append("<alternativeID>C</alternativeID>");
            stringBuilder.Append("<localPriority>0,700</localPriority>");
            stringBuilder.Append("</alternativeNode>");
            stringBuilder.Append("</alternativeNodes>");
            stringBuilder.Append("</criterionNode>");
            stringBuilder.Append("</subcriterionNodes>");

            stringBuilder.Append("</criterionNode>");
            stringBuilder.Append("</criterionNodes>");

            stringBuilder.Append("</goalNode>");
            stringBuilder.Append("</hierarchy>");

            #endregion

            //Act
            HierarchyXmlSerializer serializer = new HierarchyXmlSerializer();
            string expectedXml = stringBuilder.ToString();
            string realXml = serializer.Serialize(hierarchy);

            //Assert
            Assert.AreEqual(expectedXml, realXml);
        }
    }
}
