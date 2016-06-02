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
        public void Hierarchy_AddAlternative_Name_ReturnsNewAlternative()
        {
            //Arrange
            var hiearachy = new Hierarchy();

            //Act
            var alternative = hiearachy.AddAlternative("Alternative");

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative, hiearachy.Alternatives.ElementAt(0));
            Assert.IsNotNull(alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void Hierarchy_AddAlternative_IdAndName_ReturnsNewAlternative()
        {
            //Arrange
            var hiearachy = new Hierarchy();

            //Act
            var alternative = hiearachy.AddAlternative("ID123", "Alternative");

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative, hiearachy.Alternatives.ElementAt(0));
            Assert.AreEqual("ID123", alternative.ID);
            Assert.AreEqual("Alternative", alternative.Name);
        }

        [TestMethod]
        public void Hierarchy_AddAlternativeTwice_ThrowsException()
        {
            //Arrange
            var hiearachy = new Hierarchy();
            var alternative1 = new Alternative("Alternative1");
            Exception exception = null;

            //Act
            hiearachy.AddAlternative(alternative1);
            try
            {
                hiearachy.AddAlternative(alternative1);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.AreEqual(1, hiearachy.Alternatives.Count());
            Assert.AreEqual(alternative1, hiearachy.Alternatives.ElementAt(0));
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Same alternative can not be added twice.", exception.Message);
        }

        [TestMethod]
        public void AlternativeCollection_Remove_RemovesAlternative()
        {
            //Arrange
            var hiearachy = new Hierarchy();
            var alternative = new Alternative("Alternative1");

            //Act
            hiearachy.AddAlternative(alternative);
            hiearachy.RemoveAlternative(alternative);
            
            //Assert
            Assert.IsFalse(hiearachy.Alternatives.Contains(alternative));
        }
    }
}
