using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class AlternativeNodeCollectionTests
    {
        [TestMethod]
        public void AlternativeNodeCollection_Indexer_ValidIndex_ReturnsAlternativeNode()
        {
            //Arrange => Act
            var alternativeNodes = new AlternativeNodeCollection();
            var alternativeNode1 = new AlternativeNode(new Alternative("Alternative1"));
            var alternativeNode2 = new AlternativeNode(new Alternative("Alternative2"));
            alternativeNodes.Add(alternativeNode1);
            alternativeNodes.Add(alternativeNode2);

            //Assert
            Assert.AreEqual(alternativeNode1, alternativeNodes[0]);
            Assert.AreEqual(alternativeNode2, alternativeNodes[1]);
        }

        [TestMethod]
        public void AlternativeNodeCollection_Indexer_ValidAlternative_ReturnsAlternativeNode()
        {
            //Arrange => Act
            var alternativeNodes = new AlternativeNodeCollection();
            var alternative1 = new Alternative("Alternative1");
            var alternative2 = new Alternative("Alternative2");
            var alternative3 = new Alternative("Alternative2");
            var alternativeNode1 = new AlternativeNode(alternative1);
            var alternativeNode2 = new AlternativeNode(alternative2);
            alternativeNodes.Add(alternativeNode1);
            alternativeNodes.Add(alternativeNode2);

            //Assert
            Assert.AreEqual(alternativeNode1, alternativeNodes[alternative1]);
            Assert.AreEqual(alternativeNode2, alternativeNodes[alternative2]);
            Assert.IsNull(alternativeNodes[alternative3]);
        }

        [TestMethod]
        public void AlternativeNodeCollection_AddTwice_ThrowsException()
        {
            //Arrange
            var alternativeNodes = new AlternativeNodeCollection();
            var alternativeNode1 = new AlternativeNode(new Alternative("Alternative1"));
            Exception exception = null;

            //Act
            alternativeNodes.Add(alternativeNode1);
            try
            {
                alternativeNodes.Add(alternativeNode1);
            }
            catch (Exception ex) 
            {
                exception = ex;
            }            

            //Assert
            Assert.AreEqual(1, alternativeNodes.Count);
            Assert.AreEqual(alternativeNode1, alternativeNodes[0]);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Same item can not be added twice.", exception.Message);
        }

        [TestMethod]
        public void AlternativeNodeCollection_Add_NodeWithDuplicateAlternative_ThrowsException()
        {
            //Arrange
            var alternativeNodes = new AlternativeNodeCollection();
            var alternative = new Alternative();
            var alternativeNode1 = new AlternativeNode(alternative);
            var alternativeNode2 = new AlternativeNode(alternative);
            Exception exception = null;

            //Act
            try
            {
                alternativeNodes.Add(alternativeNode1);
                alternativeNodes.Add(alternativeNode2);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual(exception.Message, "AlternativeNode with the same value of Alternative property has already been added");
        }

        [TestMethod]
        public void AlternativeNodeCollection_Contains_AlternativeNode_ReturnsCorrectResult()
        {
            //Arrange
            var alternativeNodes = new AlternativeNodeCollection();
            var alternativeNode1 = new AlternativeNode(new Alternative("Alternative1"));
            var alternativeNode2 = new AlternativeNode(new Alternative("Alternative2"));

            //Act
            alternativeNodes.Add(alternativeNode1);

            //Assert
            Assert.IsTrue(alternativeNodes.Contains(alternativeNode1));
            Assert.IsFalse(alternativeNodes.Contains(alternativeNode2));
        }

        [TestMethod]
        public void AlternativeNodeCollection_Contains_Alternative_ReturnsCorrectResult()
        {
            //Arrange => Act
            var alternativeNodes = new AlternativeNodeCollection();
            var alternative1 = new Alternative("Alternative1");
            var alternative2 = new Alternative("Alternative2");
            var alternative3 = new Alternative("Alternative2");
            var alternativeNode1 = new AlternativeNode(alternative1);
            var alternativeNode2 = new AlternativeNode(alternative2);
            alternativeNodes.Add(alternativeNode1);
            alternativeNodes.Add(alternativeNode2);

            //Assert
            Assert.IsTrue(alternativeNodes.Contains(alternative1));
            Assert.IsTrue(alternativeNodes.Contains(alternative2));
            Assert.IsFalse(alternativeNodes.Contains(alternative3));
        }

        [TestMethod]
        public void AlternativeCollection_Remove_RemovesAlternative()
        {
            //Arrange
            var alternativeNodes = new AlternativeNodeCollection();
            var alternativeNode = new AlternativeNode(new Alternative("Alternative"));

            //Act
            alternativeNodes.Add(alternativeNode);
            alternativeNodes.Remove(alternativeNode);

            //Assert
            Assert.IsFalse(alternativeNodes.Contains(alternativeNode));
        }

        [TestMethod]
        public void AlternativeCollection_Clear_ClearsCollection()
        {
            //Arrange
            var alternativeNodes = new AlternativeNodeCollection();
            var alternativeNode1 = new AlternativeNode(new Alternative("Alternative1"));
            var alternativeNode2 = new AlternativeNode(new Alternative("Alternative2"));

            //Act
            alternativeNodes.Add(alternativeNode1);
            alternativeNodes.Add(alternativeNode2);
            alternativeNodes.Clear();

            //Assert
            Assert.AreEqual(0, alternativeNodes.Count);
        }
    }
}
