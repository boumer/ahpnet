using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class AlternativeCollectionTests
    {
        [TestMethod]
        public void AlternativeCollection_Add_Name_ReturnsNewAlternative()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();

            //Act
            Alternative alternative = alternatives.Add("Alternative");

            //Assert
            Assert.AreEqual(1, alternatives.Count);
            Assert.AreEqual(alternative, alternatives[0]);
            Assert.IsNotNull(alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void AlternativeCollection_Add_IdAndName_ReturnsNewAlternative()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();

            //Act
            Alternative alternative = alternatives.Add("ID123", "Alternative");

            //Assert
            Assert.AreEqual(1, alternatives.Count);
            Assert.AreEqual(alternative, alternatives[0]);
            Assert.AreEqual("ID123", alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void AlternativeCollection_Add_Alternative_ReturnsIndex()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();
            Alternative alternative = new Alternative("Alternative");

            //Act
            int index = alternatives.Add(alternative);

            //Assert
            Assert.AreEqual(0, index);
            Assert.AreEqual(alternative, alternatives[index]);
        }

        [TestMethod]
        public void AlternativeCollection_Add_Alternative_AddsAlternativeOnlyOnce()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();
            Alternative alternative1 = new Alternative("Alternative1");
            Alternative alternative2 = new Alternative("Alternative2");

            //Act
            alternatives.Add(alternative1);
            alternatives.Add(alternative2);
            alternatives.Add(alternative2);

            //Assert
            Assert.AreEqual(2, alternatives.Count);
            Assert.AreEqual(alternative1, alternatives[0]);
            Assert.AreEqual(alternative2, alternatives[1]);
        }

        [TestMethod]
        public void AlternativeCollection_Contains_ReturnsCorrectResult()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();
            Alternative alternative1 = new Alternative("Alternative1");
            Alternative alternative2 = new Alternative("Alternative2");

            //Act
            alternatives.Add(alternative1);

            //Assert
            Assert.IsTrue(alternatives.Contains(alternative1));
            Assert.IsFalse(alternatives.Contains(alternative2));
        }

        [TestMethod]
        public void AlternativeCollection_Remove_RemovesAlternative()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();
            Alternative alternative = new Alternative("Alternative1");

            //Act
            alternatives.Add(alternative);
            alternatives.Remove(alternative);

            //Assert
            Assert.IsFalse(alternatives.Contains(alternative));
        }

        [TestMethod]
        public void AlternativeCollection_Clear_ClearsCollection()
        {
            //Arrange
            AlternativeCollection alternatives = new AlternativeCollection();
            Alternative alternative1 = new Alternative();
            Alternative alternative2 = new Alternative();

            //Act
            alternatives.Add(alternative1);
            alternatives.Add(alternative2);
            alternatives.Clear();

            //Assert
            Assert.AreEqual(0, alternatives.Count);
        }
    }
}
