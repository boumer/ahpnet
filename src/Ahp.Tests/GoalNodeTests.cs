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
            var goal = new GoalNode("My Goal");

            //Assert
            Assert.AreEqual("My Goal", goal.Name);
        }
        
        [TestMethod]
        public void GoalNode_LocalPriority_AfterInit_Returns1M()
        {
            //Arrange => Act
            var goal = new GoalNode();

            //Assert
            Assert.AreEqual(1M, goal.LocalPriority);
        }

        [TestMethod]
        public void GoalNode_LocalPrioritySetter_ThrowsException()
        {
            //Arrange
            var goal = new GoalNode();
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
            Assert.AreEqual(exception.Message, "Setting local priority for the GoalNode is not allowed. Its value always equals 1.");
        }

        [TestMethod]
        public void GoalNode_GlobalPriority_AfterInit_Returns1M()
        {
            //Arrange => Act
            var goal = new GoalNode();

            //Assert
            Assert.AreEqual(1M, goal.GlobalPriority);
        }

        [TestMethod]
        public void GoalNode_AddCriterionNode_AdsWithFixUp()
        {
            //Arrange
            var goal = new GoalNode();
            var criterion = new CriterionNode();

            //Act
            goal.AddCriterionNode(criterion);

            //Assert
            Assert.AreEqual(goal, criterion.GoalNode);
        }

        [TestMethod]
        public void GoalNode_RemoveCriterionNode_RemovesWithFixUp()
        {
            //Arrange
            var goal = new GoalNode();
            var criterion = new CriterionNode();

            //Act
            goal.AddCriterionNode(criterion);
            goal.RemoveCriterionNode(criterion);

            //Assert
            Assert.IsNull(criterion.GoalNode);
        }
    }
}
