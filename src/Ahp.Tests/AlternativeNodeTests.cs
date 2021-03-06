﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class AlternativeNodeTests
    {
        [TestMethod]
        public void AlternativeNode_Constructor_NullAlternative_ThrowsException()
        {
            //Arrange
            Exception exception = null;

            //Act
            try
            {
                var alternative = new AlternativeNode(null);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        }

        [TestMethod]
        public void AlternativeNode_Name_ReturnsAlternativeName()
        {
            //Arange => Act
            var alternativeNode = new AlternativeNode(new Alternative("Alternative"));

            //Assert
            Assert.AreEqual("Alternative", alternativeNode.Name);
        }

        [TestMethod]
        public void AlternativeNode_Name_Setter_ThrowsException()
        {
            //Arrange
            var alternativeNode = new AlternativeNode(new Alternative("Alternative"));
            Exception exception = null;

            //Act
            try
            {
                alternativeNode.Name = "New alternative";
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual(exception.Message, "Changing Name property of AlternativeNode object directly is not allowed. Change Name property of correspondent Alternative object instead.");
        }

        [TestMethod]
        public void AlternativeNode_GlobalPriority_ReturnsCorrectValue()
        {
            //Arrange => Act
            var alternative1 = new Alternative();
            var alternative2 = new Alternative();
            var criterion = new CriterionNode("Criterion", 0.374M);

            var hierarchy = new Hierarchy();
            hierarchy.AddAlternative(alternative1);
            hierarchy.AddAlternative(alternative2);
            hierarchy.GoalNode.AddCriterionNode(criterion);

            criterion.AlternativeNodes[alternative1].LocalPriority = 0.791M;
            criterion.AlternativeNodes[alternative2].LocalPriority = 0.673M;

            //Assert
            Assert.AreEqual(0.791M * 0.374M, criterion.AlternativeNodes[alternative1].GlobalPriority);
            Assert.AreEqual(0.673M * 0.374M, criterion.AlternativeNodes[alternative2].GlobalPriority);
        }

        [TestMethod]
        public void CriterionNode_CriterionNode_SetsCriterionNode()
        {
            //Arrange
            var alternativeNode = new AlternativeNode(new Alternative());
            var criterion = new CriterionNode();

            //Act
            alternativeNode.CriterionNode = criterion;

            //Assert
            Assert.AreEqual(criterion, alternativeNode.CriterionNode);
            Assert.IsTrue(criterion.AlternativeNodes.Contains(alternativeNode));
        }

        [TestMethod]
        public void CriterionNode_CriterionNode_ExistingCriterion_ChangesCriterionNode()
        {
            //Arrange
            var alternativeNode = new AlternativeNode(new Alternative());
            var criterion = new CriterionNode();
            var criterionNew = new CriterionNode();

            //Act
            alternativeNode.CriterionNode = criterion;
            alternativeNode.CriterionNode = criterionNew;

            //Arrange
            Assert.IsFalse(criterion.AlternativeNodes.Contains(alternativeNode));
            Assert.AreEqual(criterionNew, alternativeNode.CriterionNode);
            Assert.IsTrue(criterionNew.AlternativeNodes.Contains(alternativeNode));
        }

        [TestMethod]
        public void CriterionNode_CriterionNode_ExistingCriterion_SetsCriterionNodeToNull()
        {
            //Arrange
            var alternativeNode = new AlternativeNode(new Alternative());
            var criterion = new CriterionNode();

            //Act
            alternativeNode.CriterionNode = criterion;
            alternativeNode.CriterionNode = null;

            //Arrange
            Assert.IsNull(alternativeNode.CriterionNode);
            Assert.IsFalse(criterion.AlternativeNodes.Contains(alternativeNode));
        }
    }
}
