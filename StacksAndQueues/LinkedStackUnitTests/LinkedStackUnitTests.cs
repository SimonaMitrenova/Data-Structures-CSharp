namespace LinkedStackUnitTests
{
    using System;
    using LinkedStack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedStackUnitTests
    {
        [TestMethod]
        public void TestCount_EmptyNumberStack_ShouldHaveCountZero()
        {
            var stack = new LinkedStack<int>();
            Assert.AreEqual(0, stack.Count, "An empty stack must have zero count of elements.");
        }

        [TestMethod]
        public void TestPush_OneElement_ShouldReturnCorrectCount()
        {
            var stack = new LinkedStack<int>();
            stack.Push(2);

            Assert.AreEqual(1, stack.Count, "Stack with one element must return count one.");
        }

        [TestMethod]
        public void TestPop_OneElement_ShouldReturnTheElementAndCorrectCount()
        {
            var stack = new LinkedStack<int>();
            stack.Push(2);
            int element = stack.Pop();

            Assert.AreEqual(0, stack.Count, "An empty stack must have zero count of elements.");
            Assert.AreEqual(2, element, "Pop method must return last pushed element.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPop_EmptyStack_ShouldThrow()
        {
            var stack = new LinkedStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void TestCount_EmptyStringStack_ShouldHaveCountZero()
        {
            var stack = new LinkedStack<string>();
            Assert.AreEqual(0, stack.Count, "An empty stack must have zero count of elements.");
        }

        [TestMethod]
        public void TestPush_StackWith1000Elements_ShouldReturnCorrectCount()
        {
            var stack = new LinkedStack<string>();

            for (int i = 1; i <= 1000; i++)
            {
                stack.Push(i.ToString());
                Assert.AreEqual(i, stack.Count, "Stack must return correct elements count.");
            }
        }

        [TestMethod]
        public void TestPop_StackWith1000Elements_ShouldReturnCorrectElementAndCount()
        {
            var stack = new LinkedStack<string>();

            for (int i = 1; i <= 1000; i++)
            {
                stack.Push(i.ToString());
            }

            for (int i = 1000; i > 0; i--)
            {
                string element = stack.Pop();
                Assert.AreEqual(i - 1, stack.Count, "Stack must return correct elements count.");
                Assert.AreEqual(i.ToString(), element, "Pop method must return correct element.");
            }
        }

        [TestMethod]
        public void TestPop_StackTwoElementsAdded_ShouldReturnCorrectCountAndElement()
        {
            var stack = new LinkedStack<string>();
            stack.Push("first");
            stack.Push("last");

            string actualLastAddedElement = stack.Pop();
            Assert.AreEqual("last", actualLastAddedElement, "Pop method must return last pushed element (last).");
            Assert.AreEqual(1, stack.Count, "Stack must return correct elements count (1).");

            string actualFirstAddedElement = stack.Pop();
            Assert.AreEqual("first", actualFirstAddedElement, "Pop method must return last pushed element (first).");
            Assert.AreEqual(0, stack.Count, "Stack must return correct elements count (0).");
        }

        [TestMethod]
        public void TestToArray_SeveralElements_ShouldReturnArrayWithElementsInReversedOrder()
        {
            var stack = new LinkedStack<int>();
            stack.Push(3);
            stack.Push(5);
            stack.Push(-2);
            stack.Push(7);

            var actualArray = stack.ToArray();
            var expectedResult = new int[] { 7, -2, 5, 3 };

            CollectionAssert.AreEqual(expectedResult, actualArray, "ToArray method must return array with elements in reversed order.");
        }

        [TestMethod]
        public void TestToArray_EmptyStack_ShouldReturnEmptyArray()
        {
            var stack = new LinkedStack<DateTime>();

            var actualArray = stack.ToArray();
            var expectedResult = new DateTime[] { };

            CollectionAssert.AreEqual(expectedResult, actualArray, "Empty stack - ToArray method must return empty array.");
        }
    }
}
