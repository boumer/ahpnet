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

            var costCriterion = hierarchy.GoalNode.CriterionNodes.Add("Cost", 0.504M);
            var safetyCriterion = hierarchy.GoalNode.CriterionNodes.Add("Safety", 0.237M);
            var styleCriterion = hierarchy.GoalNode.CriterionNodes.Add("Style", 0.042M);
            var capacityNode = hierarchy.GoalNode.CriterionNodes.Add("Capacity", 0.217M);
            
            var purchasePriceCriterion = costCriterion.SubcriterionNodes.Add("Purchase price", 0.488M);
            var fuelCostsCriterion = costCriterion.SubcriterionNodes.Add("Fuel costs", 0.251M);
            var maintenanceCostsCriterion = costCriterion.SubcriterionNodes.Add("Maintenance costs", 0.100M);
            var resaleValueCriterion = costCriterion.SubcriterionNodes.Add("Resale value", 0.161M);
            
            var cargoCapacityCriterion = capacityNode.SubcriterionNodes.Add("Cargo capacity", 0.167M);
            var passengerCapacityCriterion = capacityNode.SubcriterionNodes.Add("Passenger capacity", 0.833M);

            var alternative1 = hierarchy.Alternatives.Add("Accord sedan");
            var alternative2 = hierarchy.Alternatives.Add("Accord Hybrid");
            var alternative3 = hierarchy.Alternatives.Add("Pilot CUV");
            var alternative4 = hierarchy.Alternatives.Add("CR-V SUV");
            var alternative5 = hierarchy.Alternatives.Add("Element SUV");
            var alternative6 = hierarchy.Alternatives.Add("Odyssey Minivan");

            #region Alternative node local priorities

            hierarchy[alternative1, purchasePriceCriterion].LocalPriority = 0.242M;
            hierarchy[alternative2, purchasePriceCriterion].LocalPriority = 0.027M;
            hierarchy[alternative3, purchasePriceCriterion].LocalPriority = 0.027M;
            hierarchy[alternative4, purchasePriceCriterion].LocalPriority = 0.242M;
            hierarchy[alternative5, purchasePriceCriterion].LocalPriority = 0.362M;
            hierarchy[alternative6, purchasePriceCriterion].LocalPriority = 0.100M;

            hierarchy[alternative1, fuelCostsCriterion].LocalPriority = 0.188M;
            hierarchy[alternative2, fuelCostsCriterion].LocalPriority = 0.212M;
            hierarchy[alternative3, fuelCostsCriterion].LocalPriority = 0.133M;
            hierarchy[alternative4, fuelCostsCriterion].LocalPriority = 0.160M;
            hierarchy[alternative5, fuelCostsCriterion].LocalPriority = 0.151M;
            hierarchy[alternative6, fuelCostsCriterion].LocalPriority = 0.156M;

            hierarchy[alternative1, maintenanceCostsCriterion].LocalPriority = 0.357M;
            hierarchy[alternative2, maintenanceCostsCriterion].LocalPriority = 0.312M;
            hierarchy[alternative3, maintenanceCostsCriterion].LocalPriority = 0.084M;
            hierarchy[alternative4, maintenanceCostsCriterion].LocalPriority = 0.100M;
            hierarchy[alternative5, maintenanceCostsCriterion].LocalPriority = 0.089M;
            hierarchy[alternative6, maintenanceCostsCriterion].LocalPriority = 0.058M;

            hierarchy[alternative1, resaleValueCriterion].LocalPriority = 0.225M;
            hierarchy[alternative2, resaleValueCriterion].LocalPriority = 0.095M;
            hierarchy[alternative3, resaleValueCriterion].LocalPriority = 0.055M;
            hierarchy[alternative4, resaleValueCriterion].LocalPriority = 0.415M;
            hierarchy[alternative5, resaleValueCriterion].LocalPriority = 0.105M;
            hierarchy[alternative6, resaleValueCriterion].LocalPriority = 0.105M;

            hierarchy[alternative1, safetyCriterion].LocalPriority = 0.215M;
            hierarchy[alternative2, safetyCriterion].LocalPriority = 0.215M;
            hierarchy[alternative3, safetyCriterion].LocalPriority = 0.083M;
            hierarchy[alternative4, safetyCriterion].LocalPriority = 0.038M;
            hierarchy[alternative5, safetyCriterion].LocalPriority = 0.025M;
            hierarchy[alternative6, safetyCriterion].LocalPriority = 0.424M;

            hierarchy[alternative1, styleCriterion].LocalPriority = 0.346M;
            hierarchy[alternative2, styleCriterion].LocalPriority = 0.346M;
            hierarchy[alternative3, styleCriterion].LocalPriority = 0.045M;
            hierarchy[alternative4, styleCriterion].LocalPriority = 0.160M;
            hierarchy[alternative5, styleCriterion].LocalPriority = 0.025M;
            hierarchy[alternative6, styleCriterion].LocalPriority = 0.078M;

            hierarchy[alternative1, cargoCapacityCriterion].LocalPriority = 0.090M;
            hierarchy[alternative2, cargoCapacityCriterion].LocalPriority = 0.090M;
            hierarchy[alternative3, cargoCapacityCriterion].LocalPriority = 0.170M;
            hierarchy[alternative4, cargoCapacityCriterion].LocalPriority = 0.170M;
            hierarchy[alternative5, cargoCapacityCriterion].LocalPriority = 0.170M;
            hierarchy[alternative6, cargoCapacityCriterion].LocalPriority = 0.310M;

            hierarchy[alternative1, passengerCapacityCriterion].LocalPriority = 0.136M;
            hierarchy[alternative2, passengerCapacityCriterion].LocalPriority = 0.136M;
            hierarchy[alternative3, passengerCapacityCriterion].LocalPriority = 0.273M;
            hierarchy[alternative4, passengerCapacityCriterion].LocalPriority = 0.136M;
            hierarchy[alternative5, passengerCapacityCriterion].LocalPriority = 0.046M;
            hierarchy[alternative6, passengerCapacityCriterion].LocalPriority = 0.273M;

            #endregion

            //Act
            var processor = new HierarchyProcessor();
            var result = processor.Analyse(hierarchy);

            //Assert
            Assert.AreEqual(result[alternative1], 0.212885342M);
            Assert.AreEqual(result[alternative2], 0.150225038M);
            Assert.AreEqual(result[alternative3], 0.109231639M);
            Assert.AreEqual(result[alternative4], 0.164945910M);
            Assert.AreEqual(result[alternative5], 0.142593084M);
            Assert.AreEqual(result[alternative6], 0.220118987M);
        }
    }
}
