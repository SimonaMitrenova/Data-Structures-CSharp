namespace OrderedSet.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTestOrderedSet
    {
        [TestMethod]
        public void BuildOrderedSet_ForEachInOrder_ShouldWorkCorrectly()
        {
            // Arrange
            var oredredSet =new OrderedSet<int>();
            oredredSet.Add(17);
            oredredSet.Add(9);
            oredredSet.Add(12);
            oredredSet.Add(19);
            oredredSet.Add(6);
            oredredSet.Add(25);

            // Act
            var nodes = new List<int>();
            foreach (var element in oredredSet)
            {
                nodes.Add(element);
            }

            // Assert
            var expectedNodes = new int[] { 6, 9, 12, 17, 19, 25 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
            Assert.AreEqual(nodes.Count, oredredSet.Count);
        }

        [TestMethod]
        public void TestAdd_SameElementsAdded_ShouldAddTheElementOnlyOnce()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();
            oredredSet.Add(1);
            oredredSet.Add(1);
            oredredSet.Add(1);

            // Act
            var nodes = new List<int>();
            foreach (var element in oredredSet)
            {
                nodes.Add(element);
            }

            // Assert
            var expectedNodes = new int[] { 1 };
            CollectionAssert.AreEqual(expectedNodes, nodes);
            Assert.AreEqual(nodes.Count, oredredSet.Count);
        }

        [TestMethod]
        public void TestCount_EmptyOrderedSet_ShouldReturnCountZero()
        {
            // Arrange
            // Act
            var oredredSet = new OrderedSet<int>();

            // Assert
            Assert.AreEqual(0, oredredSet.Count);
        }

        [TestMethod]
        public void TestCount_AddSeveralElements_ShouldReturnCorrectCount()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();

            // Act
            int[] numbers = new[] {4, -1, 0, 10, 25, 3, 6, 2, -3, 14};
            for (int i = 0; i < numbers.Length; i++)
            {
                oredredSet.Add(numbers[i]);

                // Assert
                Assert.AreEqual(i + 1, oredredSet.Count);
            }
        }

        [TestMethod]
        public void TestContains_EmptyOrderedSet_ShouldReturnFalse()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();

            // Act
            var result = oredredSet.Contains(2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestContains_ElementNoAddedInOrderedSet_ShouldReturnFalse()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();
            oredredSet.Add(17);
            oredredSet.Add(9);
            oredredSet.Add(12);
            oredredSet.Add(19);
            oredredSet.Add(6);
            oredredSet.Add(25);

            // Act
            var result = oredredSet.Contains(2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestContains_ElementAddedInOrderedSet_ShouldReturnTrue()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();
            oredredSet.Add(17);
            oredredSet.Add(9);
            oredredSet.Add(12);
            oredredSet.Add(19);
            oredredSet.Add(6);
            oredredSet.Add(25);

            // Act
            var result = oredredSet.Contains(19);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRemove_EmptyOrderedSet_ShouldReturnFalse()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();

            // Act
            var result = oredredSet.Remove(2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestRemove_ElementNotAddedInOrderedSet_ShouldReturnFalse()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();
            oredredSet.Add(17);
            oredredSet.Add(9);
            oredredSet.Add(12);
            oredredSet.Add(19);
            oredredSet.Add(6);
            oredredSet.Add(25);

            // Act
            var result = oredredSet.Remove(2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(6, oredredSet.Count);
        }

        [TestMethod]
        public void TestRemove_ElementAddedInOrderedSet_ShouldReturnTrueAndCorrectElementsCount()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();
            oredredSet.Add(17);
            oredredSet.Add(9);
            oredredSet.Add(12);
            oredredSet.Add(19);
            oredredSet.Add(6);
            oredredSet.Add(25);

            // Act
            var result = oredredSet.Remove(12);
            var expectedNodes = new int[] { 6, 9, 17, 19, 25 };
            var nodes = new List<int>();
            foreach (var element in oredredSet)
            {
                nodes.Add(element);
            }

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedNodes.Length, oredredSet.Count);
            Assert.AreEqual(expectedNodes.Length, nodes.Count);
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }

        [TestMethod]
        public void TestForeach_EmptyOrderedSet_ShouldReturnEmptyCollection()
        {
            // Arrange
            var oredredSet = new OrderedSet<int>();

            // Act
            var expectedNodes = new int[] { };
            var nodes = new List<int>();
            foreach (var element in oredredSet)
            {
                nodes.Add(element);
            }

            // Assert
            CollectionAssert.AreEqual(expectedNodes, nodes);
        }
    }
}
