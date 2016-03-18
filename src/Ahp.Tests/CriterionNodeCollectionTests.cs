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
            CriterionNodeCollection criterions = new CriterionNodeCollection();
            CriterionNode criterion1 = new CriterionNode();
            CriterionNode criterion2 = new CriterionNode();
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
            CriterionNodeCollection criterions = new CriterionNodeCollection();

            //Act
            CriterionNode criterion = criterions.Add("Criterion");

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
            CriterionNodeCollection criterions = new CriterionNodeCollection();

            //Act
            CriterionNode criterion = criterions.Add("Criterion", 0.345M);

            //Assert
            Assert.AreEqual(1, criterions.Count);
            Assert.AreEqual(criterion, criterions[0]);
            Assert.AreEqual("Criterion", criterion.Name);
            Assert.AreEqual(0.345M, criterion.LocalPriority);
        }

        [TestMethod]
        public void CriterionNodeCollection_Add_CriterionNode_AddsCriterionNodeOnlyOnce()
        {
            //Arrange
            CriterionNodeCollection criterions = new CriterionNodeCollection();
            CriterionNode criterion1 = new CriterionNode("Criterion1");
            CriterionNode criterion2 = new CriterionNode("Criterion2");

            //Act
            criterions.Add(criterion1);
            criterions.Add(criterion2);
            criterions.Add(criterion2);

            //Assert
            Assert.AreEqual(2, criterions.Count);
            Assert.AreEqual(criterion1, criterions[0]);
            Assert.AreEqual(criterion2, criterions[1]);
        }

        [TestMethod]
        public void CriterionNodeCollection_Contains_ReturnsCorrectResult()
        {
            //Arrange
            CriterionNodeCollection criterions = new CriterionNodeCollection();
            CriterionNode criterion1 = new CriterionNode("Criterion1");
            CriterionNode criterion2 = new CriterionNode("Criterion2");

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
            CriterionNodeCollection criterions = new CriterionNodeCollection();
            CriterionNode criterion = new CriterionNode("Criterion1");

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
            CriterionNodeCollection criterions = new CriterionNodeCollection();
            CriterionNode criterion1 = new CriterionNode();
            CriterionNode criterion2 = new CriterionNode();

            //Act
            criterions.Add(criterion1);
            criterions.Add(criterion2);
            criterions.Clear();

            //Assert
            Assert.AreEqual(0, criterions.Count);
        }
    }
}
