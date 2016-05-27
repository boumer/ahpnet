using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ahp;

namespace Ahp.Tests
{
    [TestClass]
    public class PairwiseComparisonTests
    {
        [TestMethod]
        public void PairwiseComparison_Constructor_NullCriterions_ThrowsArgumentNullException()
        {
            //Arrange
            Exception exception = null;

            //Act
            try
            {
                var comparison = new PairwiseComparison((List<CriterionNode>)null);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
            Assert.AreEqual("criterions", ((ArgumentNullException)exception).ParamName);
        }

        [TestMethod]
        public void PairwiseComparison_Constructor_NotUniqueCriterions_ThrowsInvalidOperationException()
        {
            //Arrange
            var criterion = new CriterionNode();
            var criterions = new List<CriterionNode>() { criterion, criterion };
            Exception exception = null;

            //Act
            try
            {
                var comparison = new PairwiseComparison(criterions);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("Minimum 2 unique nodes are expected for pairwise comparison.", exception.Message);
        }

        [TestMethod]
        public void PairwiseComparison_Constructor_ValidCriterions_InitsImportancesPrioritiesAndLMax()
        {
            //Arrange
            var criterion1 = new CriterionNode("Criterion1");
            var criterion2 = new CriterionNode("Criterion2");
            var criterion3 = new CriterionNode("Criterion3");
            var criterions = new List<CriterionNode>() { criterion1, criterion2, criterion3 };

            //Act
            var comparison = new PairwiseComparison(criterions);

            //Assert
            foreach (var node1 in criterions)
            {
                foreach (var node2 in criterions)
                {
                    Assert.AreEqual(1M, comparison[node1, node2]);
                }
            }

            foreach (var node in criterions)
            {
                Assert.AreEqual(1M / 3, comparison[node]);
            }

            Assert.AreEqual(3, comparison.LMax);
        }

        [TestMethod]
        public void PairwiseComparison_Constructor_NullAlternatives_ThrowsArgumentNullException()
        {
            //Arrange
            Exception exception = null;

            //Act
            try
            {
                var comparison = new PairwiseComparison((List<AlternativeNode>)null);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
            Assert.AreEqual("alternatives", ((ArgumentNullException)exception).ParamName);
        }

        [TestMethod]
        public void PairwiseComparison_Constructor_NotUniqueAlternatives_ThrowsInvalidOperationException()
        {
            //Arrange
            var alternative = new AlternativeNode(new Alternative());
            var alternatives = new List<AlternativeNode>() { alternative, alternative };
            Exception exception = null;

            //Act
            try
            {
                var comparison = new PairwiseComparison(alternatives);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("Minimum 2 unique nodes are expected for pairwise comparison.", exception.Message);
        }

        [TestMethod]
        public void PairwiseComparison_Constructor_ValidAlternatives_InitsImportancesPrioritiesAndLMax()
        {
            //Arrange
            var alternative1 = new AlternativeNode(new Alternative());
            var alternative2 = new AlternativeNode(new Alternative());
            var alternative3 = new AlternativeNode(new Alternative());
            var alternatives = new List<AlternativeNode>() { alternative1, alternative2, alternative3 };

            //Act
            var comparison = new PairwiseComparison(alternatives);

            //Assert
            foreach (var node1 in alternatives)
            {
                foreach (var node2 in alternatives)
                {
                    Assert.AreEqual(1M, comparison[node1, node2]);
                }
            }

            foreach (var node in alternatives)
            {
                Assert.AreEqual(1M / 3, comparison[node]);
            }

            Assert.AreEqual(3, comparison.LMax);
        }

        [TestMethod]
        public void PairwiseComparison_ImportanceIndexerSetterAndGetter_NullNode_ThrowsArgumentNullException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2 });
            Exception exception1 = null;
            Exception exception2 = null;
            Exception exception3 = null;
            Exception exception4 = null;

            //Act
            try
            {
                comparison[node1, null] = 1M;
            }
            catch (Exception e)
            {
                exception1 = e;
            }

            try
            {
                comparison[null, node2] = 1M;
            }
            catch (Exception e)
            {
                exception2 = e;
            }

            try
            {
                var importance = comparison[node1, null];
            }
            catch (Exception e)
            {
                exception3 = e;
            }

            try
            {
                var importance = comparison[null, node2];
            }
            catch (Exception e)
            {
                exception4 = e;
            }

            //Assert
            Assert.IsNotNull(exception1);
            Assert.IsNotNull(exception2);
            Assert.IsNotNull(exception3);
            Assert.IsNotNull(exception4);
            Assert.IsInstanceOfType(exception1, typeof(ArgumentNullException));
            Assert.IsInstanceOfType(exception2, typeof(ArgumentNullException));
            Assert.IsInstanceOfType(exception3, typeof(ArgumentNullException));
            Assert.IsInstanceOfType(exception4, typeof(ArgumentNullException));
            Assert.AreEqual("node2", ((ArgumentNullException)exception1).ParamName);
            Assert.AreEqual("node1", ((ArgumentNullException)exception2).ParamName);
            Assert.AreEqual("node2", ((ArgumentNullException)exception3).ParamName);
            Assert.AreEqual("node1", ((ArgumentNullException)exception4).ParamName);
        }

        [TestMethod]
        public void PairwiseComparison_ImportanceIndexerSetterGetter_InvalidNode_ThrowsKeyNotFoundException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var nodeWrong = new CriterionNode("Criterion3");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2 });
            Exception exception1 = null;
            Exception exception2 = null;
            Exception exception3 = null;
            Exception exception4 = null;

            //Act
            try
            {
                comparison[node1, nodeWrong] = 1M;
            }
            catch (Exception e)
            {
                exception1 = e;
            }

            try
            {
                comparison[nodeWrong, node2] = 1M;
            }
            catch (Exception e)
            {
                exception2 = e;
            }

            try
            {
                var importance = comparison[node1, nodeWrong];
            }
            catch (Exception e)
            {
                exception3 = e;
            }

            try
            {
                var importance = comparison[nodeWrong, node2];
            }
            catch (Exception e)
            {
                exception4 = e;
            }

            //Assert
            Assert.IsNotNull(exception1);
            Assert.IsNotNull(exception2);
            Assert.IsNotNull(exception3);
            Assert.IsNotNull(exception4);
            Assert.IsInstanceOfType(exception1, typeof(KeyNotFoundException));
            Assert.IsInstanceOfType(exception2, typeof(KeyNotFoundException));
            Assert.IsInstanceOfType(exception3, typeof(KeyNotFoundException));
            Assert.IsInstanceOfType(exception4, typeof(KeyNotFoundException));
            Assert.AreEqual("Parameter node2 is not found in the nodes of the PairwiseComparison object.", exception1.Message);
            Assert.AreEqual("Parameter node1 is not found in the nodes of the PairwiseComparison object.", exception2.Message);
            Assert.AreEqual("Parameter node2 is not found in the nodes of the PairwiseComparison object.", exception3.Message);
            Assert.AreEqual("Parameter node1 is not found in the nodes of the PairwiseComparison object.", exception4.Message);
        }

        [TestMethod]
        public void PairwiseComparison_ImportanceIndexerSetter_InvalidValue_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2 });
            Exception exception1 = null;
            Exception exception2 = null;

            //Act
            try
            {
                comparison[node1, node2] = 1M / 10M;
            }
            catch (Exception e)
            {
                exception1 = e;
            }

            try
            {
                comparison[node1, node2] = 10;
            }
            catch (Exception e)
            {
                exception2 = e;
            }

            //Assert
            Assert.IsNotNull(exception1);
            Assert.IsNotNull(exception2);
            Assert.IsInstanceOfType(exception1, typeof(ArgumentOutOfRangeException));
            Assert.IsInstanceOfType(exception2, typeof(ArgumentOutOfRangeException));
            Assert.AreEqual("value", ((ArgumentOutOfRangeException)exception1).ParamName);
            Assert.AreEqual("value", ((ArgumentOutOfRangeException)exception2).ParamName);
        }

        [TestMethod]
        public void PairwiseComparison_ImportanceIndexer_ValidValue_SetsAndGetsValues()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var node3 = new CriterionNode("Criterion2");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2, node3 });

            //Act
            comparison[node1, node1] = 3M; //must be reset to 1
            comparison[node1, node2] = 3M;
            comparison[node1, node3] = 5M;
            comparison[node2, node3] = 1M / 7M;

            //Assert
            Assert.AreEqual(1M, comparison[node1, node1]);
            Assert.AreEqual(3M, comparison[node1, node2]);
            Assert.AreEqual(5M, comparison[node1, node3]);
            Assert.AreEqual(1M / 3M, comparison[node2, node1]);
            Assert.AreEqual(1M, comparison[node2, node2]);
            Assert.AreEqual(1M / 7M, comparison[node2, node3]);
            Assert.AreEqual(1M / 5M, comparison[node3, node1]);
            Assert.AreEqual((1M / (1M / 7M)), comparison[node3, node2]);
            Assert.AreEqual(1M, comparison[node3, node3]);
        }

        [TestMethod]
        public void PairwiseComparison_PriorityIndexerGetter_NullNode_ThrowsArgumentNullException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2 });
            Exception exception = null;

            //Act
            try
            {
                var priority = comparison[null];
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
            Assert.AreEqual("node", ((ArgumentNullException)exception).ParamName);
        }

        [TestMethod]
        public void PairwiseComparison_PriorityIndexerGetter_InvalidNode_ThrowsKeyNotFoundException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var nodeWrong = new CriterionNode("Criterion3");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2 });
            Exception exception = null;

            //Act
            try
            {
                var priority = comparison[nodeWrong];
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(KeyNotFoundException));
            Assert.AreEqual("Parameter node is not found in the nodes of the PairwiseComparison object.", exception.Message);
        }

        [TestMethod]
        public void PairwiseComparison_CR_NodeCountMoreThan10_ThrowsInvalidOperationException()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var node3 = new CriterionNode("Criterion3");
            var node4 = new CriterionNode("Criterion4");
            var node5 = new CriterionNode("Criterion5");
            var node6 = new CriterionNode("Criterion6");
            var node7 = new CriterionNode("Criterion7");
            var node8 = new CriterionNode("Criterion8");
            var node9 = new CriterionNode("Criterion9");
            var node10 = new CriterionNode("Criterion10");
            var node11 = new CriterionNode("Criterion11");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2, node3, node4, node5, node6, node7, node8, node9, node10, node11 });
            Exception exception = null;

            //Act
            try
            {
                var cr = comparison.CR;
            }
            catch (Exception e)
            {
                exception = e;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("Consistency Ratio can not be calculated", exception.Message);
        }

        [TestMethod]
        public void PairwiseComparison_ValidValues_CalculatesPrioritiesLMaxCIAndCR()
        {
            //Arrange
            var node1 = new CriterionNode("Criterion1");
            var node2 = new CriterionNode("Criterion2");
            var node3 = new CriterionNode("Criterion3");
            var node4 = new CriterionNode("Criterion4");
            var comparison = new PairwiseComparison(new CriterionNode[] { node1, node2, node3, node4 });

            //Act
            comparison[node1, node2] = 5M;
            comparison[node1, node3] = 1M / 3M;
            comparison[node1, node4] = 7M;
            comparison[node2, node3] = 1M / 5M;
            comparison[node2, node4] = 3M;
            comparison[node3, node4] = 9M;

            //Assert
            Assert.AreEqual(0.3022281814153362243706377751M, comparison[node1]);
            Assert.AreEqual(0.096248411816356253140776933M, comparison[node2]);
            Assert.AreEqual(0.5574189162664633700502966175M, comparison[node3]);
            Assert.AreEqual(0.0441044905018441524382886743M, comparison[node4]);
            Assert.AreEqual(4.2020834004705332212662807083M, comparison.LMax);
            Assert.AreEqual(0.0673611334901777404220935694M, comparison.CI);
            Assert.AreEqual(0.0748457038779752671356595216M, comparison.CR);
        }
    }
}
