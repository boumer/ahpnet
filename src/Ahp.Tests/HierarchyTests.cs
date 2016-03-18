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
            Hierarchy hierarchy1 = new Hierarchy();
            Hierarchy hierarchy2 = new Hierarchy("Other goal");

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
            Hierarchy hierarchy = new Hierarchy();
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.CriterionNodes[0].SubcriterionNodes.Add(new CriterionNode("Criterion11"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion3"));

            Alternative alternative1 = new Alternative("Alternative1");
            Alternative alternative2 = new Alternative("Alternative2");
            Alternative alternative3 = new Alternative("Alternative3");

            //Act
            hierarchy.Alternatives.Add(alternative1);
            hierarchy.Alternatives.Add(alternative2);
            hierarchy.Alternatives.Add(alternative3);

            //Assert
            foreach (CriterionNode criterion in hierarchy.GoalNode.GetLowestCriterionNodes())
            {
                Assert.IsNotNull(criterion.AlternativeNodes[alternative1]);
                Assert.IsNotNull(criterion.AlternativeNodes[alternative2]);
                Assert.IsNotNull(criterion.AlternativeNodes[alternative3]);
            }
        }

        [TestMethod]
        public void Hierarchy_AlternativesRemove_RemovesAlternatives()
        {
            //Arrange
            Hierarchy hierarchy = new Hierarchy();
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion3"));
            hierarchy.GoalNode.CriterionNodes[0].SubcriterionNodes.Add(new CriterionNode("Criterion11"));

            Alternative alternative1 = new Alternative("Alternative1");
            Alternative alternative2 = new Alternative("Alternative2");
            Alternative alternative3 = new Alternative("Alternative3");

            //Act
            hierarchy.Alternatives.Add(alternative1);
            hierarchy.Alternatives.Add(alternative2);
            hierarchy.Alternatives.Add(alternative3);
            hierarchy.Alternatives.Remove(alternative1);

            //Assert
            foreach (CriterionNode criterion in hierarchy.GoalNode.GetLowestCriterionNodes())
            {
                Assert.IsNull(criterion.AlternativeNodes[alternative1]);
            }
        }

        [TestMethod]
        public void Hierarchy_AlternativesClear_ClearsAlternatives()
        {
            //Arrange
            Hierarchy hierarchy = new Hierarchy();
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion3"));
            hierarchy.GoalNode.CriterionNodes[0].SubcriterionNodes.Add(new CriterionNode("Criterion11"));

            Alternative alternative1 = hierarchy.Alternatives.Add("Alternative1");
            Alternative alternative2 = hierarchy.Alternatives.Add("Alternative2");
            Alternative alternative3 = hierarchy.Alternatives.Add("Alternative3");

            //Act
            hierarchy.Alternatives.Clear();

            //Assert
            foreach (CriterionNode criterion in hierarchy.GoalNode.GetLowestCriterionNodes())
            {
                Assert.IsFalse(criterion.AlternativeNodes.Contains(alternative1));
                Assert.IsFalse(criterion.AlternativeNodes.Contains(alternative2));
                Assert.IsFalse(criterion.AlternativeNodes.Contains(alternative3));
            }
        }

        [TestMethod]
        public void Hierarchy_Indexer_WrongAlternativeKey_ThrowsException()
        {
            //Arrange
            Hierarchy hierarchy = new Hierarchy();
            CriterionNode criterion = hierarchy.GoalNode.CriterionNodes.Add("Criterion");
            Exception exception = null;

            //Act
            try
            {
                AlternativeNode alternativeNode = hierarchy[new Alternative("WrongKey"), criterion];
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
            Hierarchy hierarchy = new Hierarchy();
            Alternative alternative = hierarchy.Alternatives.Add("Alternative");
            Exception exception = null;

            //Act
            try
            {
                AlternativeNode alternativeNode = hierarchy[alternative, new CriterionNode("WrongKey")];
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
            Hierarchy hierarchy = new Hierarchy();
            Alternative alternative1 = hierarchy.Alternatives.Add("Alternative1");
            Alternative alternative2 = hierarchy.Alternatives.Add("Alternative2");

            CriterionNode criterion1 = hierarchy.GoalNode.CriterionNodes.Add("Criterion1");
            CriterionNode criterion2 = hierarchy.GoalNode.CriterionNodes.Add("Criterion2");
            CriterionNode criterion11 = hierarchy.GoalNode.CriterionNodes[0].SubcriterionNodes.Add("Criterion11");

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
            Hierarchy hierarchy = new Hierarchy();
            Alternative alternative1 = hierarchy.Alternatives.Add("Alternative1");
            Alternative alternative2 = hierarchy.Alternatives.Add("Alternative2");
            Alternative alternative3 = hierarchy.Alternatives.Add("Alternative3");

            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion1"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion2"));
            hierarchy.GoalNode.CriterionNodes.Add(new CriterionNode("Criterion3"));

            Alternative alternative4 = new Alternative("Alternative4");
            CriterionNode criterion11 = hierarchy.GoalNode.CriterionNodes[0].SubcriterionNodes.Add("Criterion11");
            criterion11.AlternativeNodes.Add(new AlternativeNode(alternative4));

            //Act
            hierarchy.RefreshAlternativeNodes();

            //Assert
            foreach (CriterionNode criterion in hierarchy.GoalNode.GetLowestCriterionNodes())
            {
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative1));
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative2));
                Assert.IsTrue(criterion.AlternativeNodes.Contains(alternative3));
                Assert.IsFalse(criterion.AlternativeNodes.Contains(alternative4));
            }
        }
    }
}
