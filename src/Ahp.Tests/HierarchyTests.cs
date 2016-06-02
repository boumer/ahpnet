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
        public void Hierarchy_AddAlternative_Name_Success()
        {
            //Arrange
            var hiearachy = new Hierarchy();

            //Act
            var alternative = hiearachy.AddAlternative("Alternative");

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative, hiearachy.Alternatives.ElementAt(0));
            Assert.IsNotNull(alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void Hierarchy_AddAlternative_IdAndName_Success()
        {
            //Arrange
            var hiearachy = new Hierarchy();

            //Act
            var alternative = hiearachy.AddAlternative("ID123", "Alternative");

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative, hiearachy.Alternatives.ElementAt(0));
            Assert.AreEqual("ID123", alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void Hierarchy_AddAlternativeTwice_ThrowsException()
        {
            //Arrange
            var hiearachy = new Hierarchy();
            var alternative1 = new Alternative("Alternative1");
            Exception exception = null;

            //Act
            hiearachy.AddAlternative(alternative1);
            try
            {
                hiearachy.AddAlternative(alternative1);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative1, hiearachy.Alternatives.ElementAt(0));
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Same alternative can not be added twice.", exception.Message);
        }

        [TestMethod]
        public void Hierarchy_AddAlternative_Success()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            hierarchy.GoalNode.AddCriterionNode("Criterion1");
            hierarchy.GoalNode.CriterionNodes[0].AddSubcriterionNode("Criterion11");
            hierarchy.GoalNode.AddCriterionNode("Criterion2");
            hierarchy.GoalNode.AddCriterionNode("Criterion3");

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
                Assert.IsNotNull(criterion.AlternativeNodes[alternative1]);
                Assert.IsNotNull(criterion.AlternativeNodes[alternative2]);
                Assert.IsNotNull(criterion.AlternativeNodes[alternative3]);
            }
        }

        [TestMethod]
        public void Hierarchy_RemoveAlternative_Success()
        {
            //Arrange
            var hiearachy = new Hierarchy();
            var alternative = new Alternative("Alternative1");

            //Act
            hiearachy.AddAlternative(alternative);
            hiearachy.RemoveAlternative(alternative);

            //Assert
            Assert.IsFalse(hiearachy.Alternatives.Contains(alternative));
        }

        [TestMethod]
        public void Hierarchy_RemoveAlternative_Success2()
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
                Assert.IsFalse(criterion.AlternativeNodes.Contains(alternative1));
            }
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
            var criterion11 = hierarchy.GoalNode.CriterionNodes[0].AddSubcriterionNode("Criterion11");
            
            //Act
            hierarchy.RefreshAlternativeNodes();

            //Assert
            foreach (var criterion in hierarchy.GetLowestCriterionNodes())
            {
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative1));
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative2));
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative3));
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
