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
            var alternatives = new AlternativeCollection();

            //Act
            var alternative = alternatives.Add("Alternative");

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
            var alternatives = new AlternativeCollection();

            //Act
            var alternative = alternatives.Add("ID123", "Alternative");

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
            var alternatives = new AlternativeCollection();
            var alternative = new Alternative("Alternative");
            var index = 0;

            //Act
            alternatives.Add(alternative);

            //Assert
            Assert.AreEqual(alternative, alternatives[index]);
        }

        [TestMethod]
        public void AlternativeCollection_Add_AlternativeTwice_ThrowsException()
        {
            //Arrange
            var alternatives = new AlternativeCollection();
            var alternative1 = new Alternative("Alternative1");
            Exception exception = null;

            //Act
            alternatives.Add(alternative1);
            try
            {
                alternatives.Add(alternative1);
            }
            catch (Exception ex)
            {
                exception = ex;
            }            

            //Assert
            Assert.AreEqual(1, alternatives.Count);
            Assert.AreEqual(alternative1, alternatives[0]);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Same item can not be added twice.", exception.Message);
        }

        [TestMethod]
        public void AlternativeCollection_Contains_ReturnsCorrectResult()
        {
            //Arrange
            var alternatives = new AlternativeCollection();
            var alternative1 = new Alternative("Alternative1");
            var alternative2 = new Alternative("Alternative2");

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
            var alternatives = new AlternativeCollection();
            var alternative = new Alternative("Alternative1");

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
            var alternatives = new AlternativeCollection();
            var alternative1 = new Alternative();
            var alternative2 = new Alternative();

            //Act
            alternatives.Add(alternative1);
            alternatives.Add(alternative2);
            alternatives.Clear();

            //Assert
            Assert.AreEqual(0, alternatives.Count);
        }
    }
}
