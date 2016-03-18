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
            CriterionNode criterion1 = new CriterionNode("Criterion1", 0.153M);
            CriterionNode criterion11 = criterion1.SubcriterionNodes.Add("Criterion11", 0.345M);
            CriterionNode criterion111 = criterion11.SubcriterionNodes.Add("Criterion111", 0.876M);

            //Assert
            Assert.AreEqual(0.153M, criterion1.GlobalPriority);
            Assert.AreEqual(0.153M * 0.345M, criterion11.GlobalPriority);
            Assert.AreEqual(0.153M * 0.345M * 0.876M, criterion111.GlobalPriority);
        }

        [TestMethod]
        public void CriterionNode_GoalNode_SetsGoalAndSetsParentCriterionNodeToNull()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            CriterionNode criterion = new CriterionNode();
            criterion.ParentCriterionNode = new CriterionNode();

            //Act
            criterion.GoalNode = goal;

            //Assert
            Assert.AreEqual(goal, criterion.GoalNode);
            Assert.IsTrue(goal.CriterionNodes.Contains(criterion));
            Assert.IsNull(criterion.ParentCriterionNode);
        }

        [TestMethod]
        public void CriterionNode_GoalNode_ChangesGoal()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            GoalNode goalNew = new GoalNode();
            CriterionNode criterion = new CriterionNode();

            //Act
            criterion.GoalNode = goal;
            criterion.GoalNode = goalNew;

            //Assert
            Assert.IsFalse(goal.CriterionNodes.Contains(criterion));
            Assert.AreEqual(goalNew, criterion.GoalNode);
            Assert.IsTrue(goalNew.CriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_GoalNode_SetsGoalToNull()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            CriterionNode criterion = new CriterionNode();

            //Act
            criterion.GoalNode = goal;
            criterion.GoalNode = null;

            //Assert
            Assert.IsNull(criterion.GoalNode);
            Assert.IsFalse(goal.CriterionNodes.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNode_ParentCriterionNode_SetsParentAndSetsGoalNodeToNull()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode();
            CriterionNode parent = new CriterionNode();
            criterion.GoalNode = new GoalNode();

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
            CriterionNode criterion = new CriterionNode();
            CriterionNode parent = new CriterionNode();
            CriterionNode parentNew = new CriterionNode();

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
            CriterionNode criterion = new CriterionNode();
            CriterionNode parent = new CriterionNode();

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
            CriterionNode criterion = new CriterionNode();
            criterion.AlternativeNodes.Add(new AlternativeNode(new Alternative("Alternative1")));
            criterion.AlternativeNodes.Add(new AlternativeNode(new Alternative("Alternative2")));
            CriterionNode subcriterion = new CriterionNode();

            //Act
            criterion.SubcriterionNodes.Add(subcriterion);

            //Assert
            Assert.AreEqual(criterion, subcriterion.ParentCriterionNode);
            Assert.AreEqual(0, criterion.AlternativeNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodesRemove_RemovesWithWixUp()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode();
            CriterionNode subcriterion = new CriterionNode();
            
            //Act
            criterion.SubcriterionNodes.Add(subcriterion);
            criterion.SubcriterionNodes.Remove(subcriterion);

            //Assert
            Assert.IsNull(subcriterion.ParentCriterionNode);
        }

        [TestMethod]
        public void CriterionNode_SubcriterionNodesClear_ClearsWithWixUp()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode("Criterion");
            CriterionNode subcriterion1 = new CriterionNode("Subcriterion1");
            CriterionNode subcriterion2 = new CriterionNode("Subcriterion2");
            criterion.SubcriterionNodes.Add(subcriterion1);
            criterion.SubcriterionNodes.Add(subcriterion2);

            //Act
            criterion.SubcriterionNodes.Clear();

            //Assert
            Assert.IsNull(subcriterion1.ParentCriterionNode);
            Assert.IsNull(subcriterion2.ParentCriterionNode);
        }

        [TestMethod]
        public void CriterionNode_AlternativeNodesAdd_AddsWithWixUp()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode("Criterion");
            criterion.SubcriterionNodes.Add(new CriterionNode("Subcriterion1"));
            criterion.SubcriterionNodes.Add(new CriterionNode("Subcriterion2"));
            AlternativeNode alternativeNode = new AlternativeNode(new Alternative("Alternative"));

            //Act
            criterion.AlternativeNodes.Add(alternativeNode);

            //Assert
            Assert.AreEqual(criterion, alternativeNode.CriterionNode);
            Assert.AreEqual(0, criterion.SubcriterionNodes.Count);
        }

        [TestMethod]
        public void CriterionNode_AlternativeNodesRemove_RemovesWithWixUp()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode();
            AlternativeNode alternativeNode = new AlternativeNode(new Alternative());
            criterion.AlternativeNodes.Add(alternativeNode);

            //Act
            criterion.AlternativeNodes.Remove(alternativeNode);

            //Assert
            Assert.IsNull(alternativeNode.CriterionNode);
        }

        [TestMethod]
        public void CriterionNode_AlternativeNodesClear_ClearsWithWixUp()
        {
            //Arrange
            CriterionNode criterion = new CriterionNode("Criterion");
            AlternativeNode alternativeNode1 = new AlternativeNode(new Alternative("Alternative1"));
            AlternativeNode alternativeNode2 = new AlternativeNode(new Alternative("Alternative2"));
            criterion.AlternativeNodes.Add(alternativeNode1);
            criterion.AlternativeNodes.Add(alternativeNode2);

            //Act
            criterion.AlternativeNodes.Clear();

            //Assert
            Assert.IsNull(alternativeNode1.CriterionNode);
            Assert.IsNull(alternativeNode2.CriterionNode);
        }

        [TestMethod]
        public void CriterionNode_HasSubcriterionNodes_WithoutNodes_ReturnsFalse()
        {
            //Arrange => Act
            CriterionNode criterion = new CriterionNode();
            
            //Assert
            Assert.IsFalse(criterion.HasSubcriterionNodes);
        }

        [TestMethod]
        public void CriterionNode_HasSubcriterionNodes_WithNodes_ReturnsTrue()
        {
            //Arrange => Act
            CriterionNode criterion = new CriterionNode();
            criterion.SubcriterionNodes.Add("Subcriterion");

            //Assert
            Assert.IsTrue(criterion.HasSubcriterionNodes);
        }

        [TestMethod]
        public void CriterionNode_HasAlternativeNodes_WithoutNodes_ReturnsFalse()
        {
            //Arrange => Act
            CriterionNode criterion = new CriterionNode();

            //Assert
            Assert.IsFalse(criterion.HasAlternativeNodes);
        }

        [TestMethod]
        public void CriterionNode_HasAlternativeNodes_WithNodes_ReturnsTrue()
        {
            //Arrange => Act
            CriterionNode criterion = new CriterionNode();
            criterion.AlternativeNodes.Add(new AlternativeNode(new Alternative("Subcriterion")));

            //Assert
            Assert.IsTrue(criterion.HasAlternativeNodes);
        }
    }
}
