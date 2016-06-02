using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class CriterionNodeTests
    {
        [TestMethod]
        public void CriterionNode_GlobalPriority_ReturnsCorrectValue()
        {
            //Arrange => Act
            var criterion1 = new CriterionNode("Criterion1", 0.153M);
            var criterion11 = criterion1.AddSubcriterionNode("Criterion11", 0.345M);
            var criterion111 = criterion11.AddSubcriterionNode("Criterion111", 0.876M);

            //Assert
            Assert.AreEqual(0.153M, criterion1.GlobalPriority);
            Assert.AreEqual(0.153M * 0.345M, criterion11.GlobalPriority);
            Assert.AreEqual(0.153M * 0.345M * 0.876M, criterion111.GlobalPriority);
        }

        [TestMethod]
        public void CriterionNode_GoalNode_SetsGoalAndSetsParentCriterionNodeToNull()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = new CriterionNode();
            criterion.ParentCriterionNode = new CriterionNode();

            //Act
            criterion.GoalNode = hierarchy.GoalNode;

            //Assert
            Assert.AreEqual(hierarchy.GoalNode, criterion.GoalNode);
            Assert.IsTrue(hierarchy.GoalNode.CriterionNodes.Contains(criterion));
            Assert.IsNull(criterion.ParentCriterionNode);
        }

        [TestMethod]
        public void CriterionNode_GoalNode_ChangesGoal()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var hierarchyNew = new Hierarchy();
            var criterion = new CriterionNode();

            //Act
            criterion.GoalNode = hierarchy.GoalNode;
            criterion.GoalNode = hierarchyNew.GoalNode;

            //Assert
            Assert.IsFalse(hierarchy.GoalNode.CriterionNodes.Contains(criterion));
            Assert.AreEqual(hierarchyNew.GoalNode, criterion.GoalNode);
            Assert.IsTrue(hierarchyNew.GoalNode.CriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_GoalNode_SetsGoalToNull()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = new CriterionNode();

            //Act
            criterion.GoalNode = hierarchy.GoalNode;
            criterion.GoalNode = null;

            //Assert
            Assert.IsNull(criterion.GoalNode);
            Assert.IsFalse(hierarchy.GoalNode.CriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_ParentCriterionNode_SetsParentAndSetsGoalNodeToNull()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = new CriterionNode();
            var parent = new CriterionNode();
            criterion.GoalNode = hierarchy.GoalNode;

            //Act
            criterion.ParentCriterionNode = parent;

            //Assert
            Assert.AreEqual(parent, criterion.ParentCriterionNode);
            Assert.IsTrue(parent.SubcriterionNodes.Contains(criterion));
            Assert.IsNull(criterion.GoalNode);
        }

        [TestMethod]
        public void CriterionNode_ParentCriterionNode_ChangesParent()
        {
            //Arrange
            var criterion = new CriterionNode();
            var parent = new CriterionNode();
            var parentNew = new CriterionNode();

            //Act
            criterion.ParentCriterionNode = parent;
            criterion.ParentCriterionNode = parentNew;

            //Assert
            Assert.IsFalse(parent.SubcriterionNodes.Contains(criterion));
            Assert.AreEqual(parentNew, criterion.ParentCriterionNode);
            Assert.IsTrue(parentNew.SubcriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_ParentCriterionNode_SetsParentToNull()
        {
            //Arrange
            var criterion = new CriterionNode();
            var parent = new CriterionNode();

            //Act
            criterion.ParentCriterionNode = parent;
            criterion.ParentCriterionNode = null;

            //Assert
            Assert.IsNull(criterion.ParentCriterionNode);
            Assert.IsFalse(parent.SubcriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodes_Add_AddsWithFixUp()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            hierarchy.AddAlternative(new Alternative("Alternative1"));
            hierarchy.AddAlternative(new Alternative("Alternative2"));

            var criterion = hierarchy.GoalNode.AddCriterionNode("Criterion");
            var subcriterion = new CriterionNode();

            //Act
            criterion.AddSubcriterionNode(subcriterion);

            //Assert
            Assert.AreEqual(criterion, subcriterion.ParentCriterionNode);
            Assert.AreEqual(0, criterion.AlternativeNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodesRemove_RemovesWithWixUp()
        {
            //Arrange
            var criterion = new CriterionNode();
            var subcriterion = new CriterionNode();

            //Act
            criterion.AddSubcriterionNode(subcriterion);
            criterion.RemoveSubcriterionNode(subcriterion);

            //Assert
            Assert.IsNull(subcriterion.ParentCriterionNode);
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodesCount_WithoutNodes_Returns0()
        {
            //Arrange => Act
            var criterion = new CriterionNode();

            //Assert
            Assert.AreEqual(0, criterion.SubcriterionNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodesCount_WithNodes_Returns1()
        {
            //Arrange => Act
            var criterion = new CriterionNode();
            criterion.AddSubcriterionNode("Subcriterion");

            //Assert
            Assert.AreEqual(1, criterion.SubcriterionNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_AlternativeNodesCount_WithoutNodes_Returns0()
        {
            //Arrange => Act
            var criterion = new CriterionNode();

            //Assert
            Assert.AreEqual(0, criterion.AlternativeNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_AlternativeNodesCount_WithNodes_Returns1()
        {
            //Arrange => Act
            var hierarchy = new Hierarchy();
            hierarchy.AddAlternative(new Alternative("Alternative"));
            var criterion = hierarchy.GoalNode.AddCriterionNode("Criterion");

            //Assert
            Assert.AreEqual(1, criterion.AlternativeNodes.Count);
        }
    }
}
