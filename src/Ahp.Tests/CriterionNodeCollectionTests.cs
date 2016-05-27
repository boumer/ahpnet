using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class CriterionNodeCollectionTests
    {
        [TestMethod]
        public void CriterionNodeCollection_Indexer_ByIndex_ReturnsCriterionNode()
        {
            //Arrange => Act
            var criterions = new CriterionNodeCollection();
            var criterion1 = new CriterionNode();
            var criterion2 = new CriterionNode();
            criterions.Add(criterion1);
            criterions.Add(criterion2);

            //Assert
            Assert.AreEqual(criterion1, criterions[0]);
            Assert.AreEqual(criterion2, criterions[1]);
        }

        [TestMethod]
        public void CriterionNodeCollection_Add_Name_ReturnsNewCriterion()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();

            //Act
            var criterion = criterions.Add("Criterion");

            //Assert
            Assert.AreEqual(1, criterions.Count);
            Assert.AreEqual(criterion, criterions[0]);
            Assert.AreEqual("Criterion", criterion.Name);
            Assert.AreEqual(0, criterion.LocalPriority);
        }

        [TestMethod]
        public void CriterionNodeCollection_Add_NameAndWeight_ReturnsNewCriterion()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();

            //Act
            var criterion = criterions.Add("Criterion", 0.345M);

            //Assert
            Assert.AreEqual(1, criterions.Count);
            Assert.AreEqual(criterion, criterions[0]);
            Assert.AreEqual("Criterion", criterion.Name);
            Assert.AreEqual(0.345M, criterion.LocalPriority);
        }

        [TestMethod]
        public void CriterionNodeCollection_AddTwice_ThrowsException()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();
            var criterion1 = new CriterionNode("Criterion1");
            Exception exception = null;

            //Act
            criterions.Add(criterion1);
            try
            {
                criterions.Add(criterion1);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.AreEqual(1, criterions.Count);
            Assert.AreEqual(criterion1, criterions[0]);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Same item can not be added twice.", exception.Message);
        }

        [TestMethod]
        public void CriterionNodeCollection_Contains_ReturnsCorrectResult()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();
            var criterion1 = new CriterionNode("Criterion1");
            var criterion2 = new CriterionNode("Criterion2");

            //Act
            criterions.Add(criterion1);

            //Assert
            Assert.IsTrue(criterions.Contains(criterion1));
            Assert.IsFalse(criterions.Contains(criterion2));
        }

        [TestMethod]
        public void CriterionNodeCollection_Remove_RemovesCriterionNode()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();
            var criterion = new CriterionNode("Criterion1");

            //Act
            criterions.Add(criterion);
            criterions.Remove(criterion);

            //Assert
            Assert.IsFalse(criterions.Contains(criterion));
        }

        [TestMethod]
        public void CriterionNodeCollection_Clear_ClearsCollection()
        {
            //Arrange
            var criterions = new CriterionNodeCollection();
            var criterion1 = new CriterionNode();
            var criterion2 = new CriterionNode();

            //Act
            criterions.Add(criterion1);
            criterions.Add(criterion2);
            criterions.Clear();

            //Assert
            Assert.AreEqual(0, criterions.Count);
        }
    }
}
