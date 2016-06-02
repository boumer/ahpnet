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
            var hierarchy = new Hierarchy("My Goal");

            //Assert
            Assert.AreEqual("My Goal", hierarchy.GoalNode.Name);
        }

        [TestMethod]
        public void GoalNode_LocalPriority_AfterInit_Returns1M()
        {
            //Arrange => Act
            var hierarchy = new Hierarchy();

            //Assert
            Assert.AreEqual(1M, hierarchy.GoalNode.LocalPriority);
        }

        [TestMethod]
        public void GoalNode_LocalPrioritySetter_ThrowsException()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            Exception exception = null;

            //Act
            try
            {
                hierarchy.GoalNode.LocalPriority = 2M;
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
            var hierarchy = new Hierarchy();

            //Assert
            Assert.AreEqual(1M, hierarchy.GoalNode.GlobalPriority);
        }

        [TestMethod]
        public void GoalNode_AddCriterionNode_AdsWithFixUp()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = new CriterionNode();

            //Act
            hierarchy.GoalNode.AddCriterionNode(criterion);

            //Assert
            Assert.AreEqual(hierarchy.GoalNode, criterion.GoalNode);
        }

        [TestMethod]
        public void GoalNode_RemoveCriterionNode_RemovesWithFixUp()
        {
            //Arrange
            var hierarchy = new Hierarchy();
            var criterion = new CriterionNode();

            //Act
            hierarchy.GoalNode.AddCriterionNode(criterion);
            hierarchy.GoalNode.RemoveCriterionNode(criterion);

            //Assert
            Assert.IsNull(criterion.GoalNode);
        }
    }
}
