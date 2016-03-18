using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class GoalNodeTests
    {
        [TestMethod]
        public void GoalNode_Constructor_SetsName()
        {
            //Arrange => Act
            GoalNode goal = new GoalNode("My Goal");

            //Assert
            Assert.AreEqual("My Goal", goal.Name);
        }
        
        [TestMethod]
        public void GoalNode_LocalPriority_AfterInit_Returns1M()
        {
            //Arrange => Act
            GoalNode goal = new GoalNode();

            //Assert
            Assert.AreEqual(1M, goal.LocalPriority);
        }

        [TestMethod]
        public void GoalNode_LocalPrioritySetter_ThrowsException()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            Exception exception = null;

            //Act
            try
            {
                goal.LocalPriority = 2M;
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual(exception.Message, "Changing local priority of the GoalNode is not allowed. Its value always equals 1.");
        }

        [TestMethod]
        public void GoalNode_GlobalPriority_AfterInit_Returns1M()
        {
            //Arrange => Act
            GoalNode goal = new GoalNode();

            //Assert
            Assert.AreEqual(1M, goal.GlobalPriority);
        }

        [TestMethod]
        public void GoalNode_CriterionNodesAdd_AdsWithFixUp()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            CriterionNode criterion = new CriterionNode();

            //Act
            goal.CriterionNodes.Add(criterion);

            //Assert
            Assert.AreEqual(goal, criterion.GoalNode);
        }

        [TestMethod]
        public void GoalNode_CriterionNodesRemove_RemovesWithFixUp()
        {
            //Arrange
            GoalNode goal = new GoalNode();
            CriterionNode criterion = new CriterionNode();

            //Act
            goal.CriterionNodes.Add(criterion);
            goal.CriterionNodes.Remove(criterion);

            //Assert
            Assert.IsNull(criterion.GoalNode);
        }

        [TestMethod]
        public void GoalNode_CriterionNodesClear_ClearsWithFixUp()
        {
            //Arrange
            GoalNode goal = new GoalNode("Goal");
            CriterionNode criterion1 = new CriterionNode("Criterion1");
            CriterionNode criterion2 = new CriterionNode("Criterion2");

            //Act
            goal.CriterionNodes.Add(criterion1);
            goal.CriterionNodes.Add(criterion2);
            goal.CriterionNodes.Clear();

            //Assert
            Assert.IsNull(criterion1.GoalNode);
            Assert.IsNull(criterion2.GoalNode);
        }

        [TestMethod]
        public void GoalNode_GetLowestCriterionNodes_ReturnsCorrectNodes()
        {
            //Arrange
            var goal = new GoalNode();
            var criterion1 = goal.CriterionNodes.Add("Criterion1");
            var criterion11 = criterion1.SubcriterionNodes.Add("Criterion11");
            var criterion111 = criterion11.SubcriterionNodes.Add("Criterion111");
            var criterion112 = criterion11.SubcriterionNodes.Add("Criterion112");
            var criterion12 = criterion1.SubcriterionNodes.Add("Criterion12");
            var criterion121 = criterion12.SubcriterionNodes.Add("Criterion121");
            var criterion2 = goal.CriterionNodes.Add("Criterion2");
            var criterion21 = criterion2.SubcriterionNodes.Add("Criterion21");
            var criterion3 = goal.CriterionNodes.Add("Criterion3");

            //Act
            var criterions = goal.GetLowestCriterionNodes();
            
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
