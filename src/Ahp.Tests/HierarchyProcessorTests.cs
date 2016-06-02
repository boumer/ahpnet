using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class HierarchyProcessorTests
    {
        [TestMethod]
        public void HierarchyProcessor_Analyse_Calculates()
        {
            //This is sample hierarchy from Wikipedia article about AHP
            //http://en.wikipedia.org/wiki/Analytic_Hierarchy_Process

            //Arrange
            var hierarchy = new Hierarchy("Choose optimal car");

            var costCriterion = hierarchy.GoalNode.AddCriterionNode("Cost", 0.504M);
            var safetyCriterion = hierarchy.GoalNode.AddCriterionNode("Safety", 0.237M);
            var styleCriterion = hierarchy.GoalNode.AddCriterionNode("Style", 0.042M);
            var capacityNode = hierarchy.GoalNode.AddCriterionNode("Capacity", 0.217M);
            
            var purchasePriceCriterion = costCriterion.AddSubcriterionNode("Purchase price", 0.488M);
            var fuelCostsCriterion = costCriterion.AddSubcriterionNode("Fuel costs", 0.251M);
            var maintenanceCostsCriterion = costCriterion.AddSubcriterionNode("Maintenance costs", 0.100M);
            var resaleValueCriterion = costCriterion.AddSubcriterionNode("Resale value", 0.161M);
            
            var cargoCapacityCriterion = capacityNode.AddSubcriterionNode("Cargo capacity", 0.167M);
            var passengerCapacityCriterion = capacityNode.AddSubcriterionNode("Passenger capacity", 0.833M);

            var alternative1 = hierarchy.AddAlternative("Accord sedan");
            var alternative2 = hierarchy.AddAlternative("Accord Hybrid");
            var alternative3 = hierarchy.AddAlternative("Pilot CUV");
            var alternative4 = hierarchy.AddAlternative("CR-V SUV");
            var alternative5 = hierarchy.AddAlternative("Element SUV");
            var alternative6 = hierarchy.AddAlternative("Odyssey Minivan");

            #region Alternative node local priorities

            purchasePriceCriterion.AlternativeNodes[alternative1].LocalPriority = 0.242M;
            purchasePriceCriterion.AlternativeNodes[alternative2].LocalPriority = 0.027M;
            purchasePriceCriterion.AlternativeNodes[alternative3].LocalPriority = 0.027M;
            purchasePriceCriterion.AlternativeNodes[alternative4].LocalPriority = 0.242M;
            purchasePriceCriterion.AlternativeNodes[alternative5].LocalPriority = 0.362M;
            purchasePriceCriterion.AlternativeNodes[alternative6].LocalPriority = 0.100M;

            fuelCostsCriterion.AlternativeNodes[alternative1].LocalPriority = 0.188M;
            fuelCostsCriterion.AlternativeNodes[alternative2].LocalPriority = 0.212M;
            fuelCostsCriterion.AlternativeNodes[alternative3].LocalPriority = 0.133M;
            fuelCostsCriterion.AlternativeNodes[alternative4].LocalPriority = 0.160M;
            fuelCostsCriterion.AlternativeNodes[alternative5].LocalPriority = 0.151M;
            fuelCostsCriterion.AlternativeNodes[alternative6].LocalPriority = 0.156M;

            maintenanceCostsCriterion.AlternativeNodes[alternative1].LocalPriority = 0.357M;
            maintenanceCostsCriterion.AlternativeNodes[alternative2].LocalPriority = 0.312M;
            maintenanceCostsCriterion.AlternativeNodes[alternative3].LocalPriority = 0.084M;
            maintenanceCostsCriterion.AlternativeNodes[alternative4].LocalPriority = 0.100M;
            maintenanceCostsCriterion.AlternativeNodes[alternative5].LocalPriority = 0.089M;
            maintenanceCostsCriterion.AlternativeNodes[alternative6].LocalPriority = 0.058M;

            resaleValueCriterion.AlternativeNodes[alternative1].LocalPriority = 0.225M;
            resaleValueCriterion.AlternativeNodes[alternative2].LocalPriority = 0.095M;
            resaleValueCriterion.AlternativeNodes[alternative3].LocalPriority = 0.055M;
            resaleValueCriterion.AlternativeNodes[alternative4].LocalPriority = 0.415M;
            resaleValueCriterion.AlternativeNodes[alternative5].LocalPriority = 0.105M;
            resaleValueCriterion.AlternativeNodes[alternative6].LocalPriority = 0.105M;

            safetyCriterion.AlternativeNodes[alternative1].LocalPriority = 0.215M;
            safetyCriterion.AlternativeNodes[alternative2].LocalPriority = 0.215M;
            safetyCriterion.AlternativeNodes[alternative3].LocalPriority = 0.083M;
            safetyCriterion.AlternativeNodes[alternative4].LocalPriority = 0.038M;
            safetyCriterion.AlternativeNodes[alternative5].LocalPriority = 0.025M;
            safetyCriterion.AlternativeNodes[alternative6].LocalPriority = 0.424M;

            styleCriterion.AlternativeNodes[alternative1].LocalPriority = 0.346M;
            styleCriterion.AlternativeNodes[alternative2].LocalPriority = 0.346M;
            styleCriterion.AlternativeNodes[alternative3].LocalPriority = 0.045M;
            styleCriterion.AlternativeNodes[alternative4].LocalPriority = 0.160M;
            styleCriterion.AlternativeNodes[alternative5].LocalPriority = 0.025M;
            styleCriterion.AlternativeNodes[alternative6].LocalPriority = 0.078M;

            cargoCapacityCriterion.AlternativeNodes[alternative1].LocalPriority = 0.090M;
            cargoCapacityCriterion.AlternativeNodes[alternative2].LocalPriority = 0.090M;
            cargoCapacityCriterion.AlternativeNodes[alternative3].LocalPriority = 0.170M;
            cargoCapacityCriterion.AlternativeNodes[alternative4].LocalPriority = 0.170M;
            cargoCapacityCriterion.AlternativeNodes[alternative5].LocalPriority = 0.170M;
            cargoCapacityCriterion.AlternativeNodes[alternative6].LocalPriority = 0.310M;

            passengerCapacityCriterion.AlternativeNodes[alternative1].LocalPriority = 0.136M;
            passengerCapacityCriterion.AlternativeNodes[alternative2].LocalPriority = 0.136M;
            passengerCapacityCriterion.AlternativeNodes[alternative3].LocalPriority = 0.273M;
            passengerCapacityCriterion.AlternativeNodes[alternative4].LocalPriority = 0.136M;
            passengerCapacityCriterion.AlternativeNodes[alternative5].LocalPriority = 0.046M;
            passengerCapacityCriterion.AlternativeNodes[alternative6].LocalPriority = 0.273M;

            #endregion

            //Act
            var processor = new HierarchyProcessor();
            var result = processor.Analyse(hierarchy);

            //Assert
            Assert.AreEqual(0.212885342M, result[alternative1]);
            Assert.AreEqual(0.150225038M, result[alternative2]);
            Assert.AreEqual(0.109231639M, result[alternative3]);
            Assert.AreEqual(0.164945910M, result[alternative4]);
            Assert.AreEqual(0.142593084M, result[alternative5]);
            Assert.AreEqual(0.220118987M, result[alternative6]);
        }
    }
}
