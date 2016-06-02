using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class HierarchyTests
    {
        [TestMethod]
        public void Hierarchy_Constructor_SetsDefaultGoal()
        {
            //Arrange => Act
            var hierarchy1 = new Hierarchy();
            var hierarchy2 = new Hierarchy("Other goal");

            //Assert
            Assert.IsNotNull(hierarchy1.GoalNode);
            Assert.IsNotNull(hierarchy2.GoalNode);
            Assert.AreEqual("Goal", hierarchy1.GoalNode.Name);
            Assert.AreEqual("Other goal", hierarchy2.GoalNode.Name);
        }

        [TestMethod]
        public void Hierarchy_AlternativesAdd_AdsAlternatives()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.CriterionNodes.ElementAt(0).AddSubcriterionNode(new CriterionNode("Criterion11"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion3"));

            var alternative1 = new Alternative("Alternative1");
            var alternative2 = new Alternative("Alternative2");
            var alternative3 = new Alternative("Alternative3");

            //Act
            hierarchy.AddAlternative(alternative1);
            hierarchy.AddAlternative(alternative2);
            hierarchy.AddAlternative(alternative3);

            //Assert
            foreach (var criterion in hierarchy.GetLowestCriterionNodes())
            {
                Assert.IsNotNull(criterion.GetAlternativeNode(alternative1));
                Assert.IsNotNull(criterion.GetAlternativeNode(alternative2));
                Assert.IsNotNull(criterion.GetAlternativeNode(alternative3));
            }
        }

        [TestMethod]
        public void Hierarchy_AlternativesRemove_RemovesAlternatives()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion3"));
            hierarchy.GoalNode.CriterionNodes.ElementAt(0).AddSubcriterionNode(new CriterionNode("Criterion11"));

            var alternative1 = new Alternative("Alternative1");
            var alternative2 = new Alternative("Alternative2");
            var alternative3 = new Alternative("Alternative3");

            //Act
            hierarchy.AddAlternative(alternative1);
            hierarchy.AddAlternative(alternative2);
            hierarchy.AddAlternative(alternative3);
            hierarchy.RemoveAlternative(alternative1);

            //Assert
            foreach (var criterion in hierarchy.GetLowestCriterionNodes())
            {
                Assert.IsNull(criterion.GetAlternativeNode(alternative1));
            }
        }

        [TestMethod]
        public void Hierarchy_Indexer_WrongAlternativeKey_ThrowsException()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = hierarchy.GoalNode.AddCriterionNode("Criterion");
            Exception exception = null;

            //Act
            try
            {
                var alternativeNode = hierarchy[new Alternative("WrongKey"), criterion];
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(KeyNotFoundException));
            Assert.AreEqual(exception.Message, "Alternative 'WrongKey' not found in the Altenatives collection of the hierarchy");
        }

        [TestMethod]
        public void Hierarchy_Indexer_WrongCriterionKey_ThrowsException()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var alternative = hierarchy.AddAlternative("Alternative");
            Exception exception = null;

            //Act
            try
            {
                var alternativeNode = hierarchy[alternative, new CriterionNode("WrongKey")];
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(KeyNotFoundException));
            Assert.AreEqual(exception.Message, "Criterion node 'WrongKey' not found in the criterion nodes which are at lowest level of the hierarchy");
        }

        [TestMethod]
        public void Hierarchy_Indexer_ValidAlternativeAndCriterion_ReturnsAlternativeNode()
        {
            //Arrange => Act
            var hierarchy = new Hierarchy();
            var alternative1 = hierarchy.AddAlternative("Alternative1");
            var alternative2 = hierarchy.AddAlternative("Alternative2");

            var criterion1 = hierarchy.GoalNode.AddCriterionNode("Criterion1");
            var criterion2 = hierarchy.GoalNode.AddCriterionNode("Criterion2");
            var criterion11 = hierarchy.GoalNode.CriterionNodes.ElementAt(0).AddSubcriterionNode("Criterion11");

            //Assert
            Assert.AreEqual(hierarchy[alternative1, criterion2].Alternative, alternative1);
            Assert.AreEqual(hierarchy[alternative1, criterion2].CriterionNode, criterion2);

            Assert.AreEqual(hierarchy[alternative1, criterion11].Alternative, alternative1);
            Assert.AreEqual(hierarchy[alternative1, criterion11].CriterionNode, criterion11);

            Assert.AreEqual(hierarchy[alternative2, criterion2].Alternative, alternative2);
            Assert.AreEqual(hierarchy[alternative2, criterion2].CriterionNode, criterion2);

            Assert.AreEqual(hierarchy[alternative2, criterion11].Alternative, alternative2);
            Assert.AreEqual(hierarchy[alternative1, criterion11].CriterionNode, criterion11);
        }

        [TestMethod]
        public void Hierarchy_RefreshAlternativeNodes_AppendsAlternativeNodes()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var alternative1 = hierarchy.AddAlternative("Alternative1");
            var alternative2 = hierarchy.AddAlternative("Alternative2");
            var alternative3 = hierarchy.AddAlternative("Alternative3");

            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.AddCriterionNode(new CriterionNode("Criterion3"));

            var alternative4 = new Alternative("Alternative4");
            var criterion11 = hierarchy.GoalNode.CriterionNodes.ElementAt(0).AddSubcriterionNode("Criterion11");
            criterion11.AddAlternativeNode(new AlternativeNode(alternative4));
            
            //Act
            hierarchy.RefreshAlternativeNodes();

            //Assert
            foreach (var criterion in hierarchy.GetLowestCriterionNodes())
            {
                Assert.IsTrue(criterion.ContainsAlternativeNode(alternative1));
                Assert.IsTrue(criterion.ContainsAlternativeNode(alternative2));
                Assert.IsTrue(criterion.ContainsAlternativeNode(alternative3));
                Assert.IsFalse(criterion.ContainsAlternativeNode(alternative4));
            }
        }

        [TestMethod]
        public void Hierarchy_GetLowestCriterionNodes_ReturnsCorrectNodes()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion1 = hierarchy.GoalNode.AddCriterionNode("Criterion1");
            var criterion11 = criterion1.AddSubcriterionNode("Criterion11");
            var criterion111 = criterion11.AddSubcriterionNode("Criterion111");
            var criterion112 = criterion11.AddSubcriterionNode("Criterion112");
            var criterion12 = criterion1.AddSubcriterionNode("Criterion12");
            var criterion121 = criterion12.AddSubcriterionNode("Criterion121");
            var criterion2 = hierarchy.GoalNode.AddCriterionNode("Criterion2");
            var criterion21 = criterion2.AddSubcriterionNode("Criterion21");
            var criterion3 = hierarchy.GoalNode.AddCriterionNode("Criterion3");

            //Act
            var criterions = hierarchy.GetLowestCriterionNodes();

            //Assert
            Assert.AreEqual(5, criterions.Count);
            Assert.IsTrue(criterions.Contains(criterion111));
            Assert.IsTrue(criterions.Contains(criterion112));
            Assert.IsTrue(criterions.Contains(criterion121));
            Assert.IsTrue(criterions.Contains(criterion21));
            Assert.IsTrue(criterions.Contains(criterion3));
        }
    }
}
