namespace AvlTreeTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AvlTreeLab;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AvlInsertTests
    {
        [TestMethod]
        public void AddSeveralElements_EmptyTree_ShouldIncreaseCount()
        {
            var nums = TestUtils.ToIntArray("1 2 3");

            var tree = new AvlTree<int>();
            foreach (int num in nums)
            {
                tree.Add(num);
            }

            Assert.AreEqual(nums.Length, tree.Count);
        }

        [TestMethod]
        public void Add_RepeatingElements_ShouldNotAddDuplicates()
        {
            var nums = TestUtils.ToIntArray("1 1 1");

            var tree = new AvlTree<int>();
            foreach (int num in nums)
            {
                tree.Add(num);
            }

            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void AddingMultipleItems_RandomOrder_ShouldForeachInOrder()
        {
            var nums = TestUtils.ToIntArray("1 5 3 20 6 13 40 70 100 200 -50");

            var tree = new AvlTree<int>();
            foreach (var num in nums)
            {
                tree.Add(num);
            }

            var sortedNumbers = nums.OrderBy(n => n).ToArray();
            var expectedSequence = new Queue<int>(sortedNumbers);

            tree.ForeachDfs((depth, num) =>
            {
                Assert.AreEqual(expectedSequence.Dequeue(), num);
            });
        }

        [TestMethod]
        public void Foreach_AddedManyRandomElements_ShouldReturnSortedAscending()
        {
            const int NumCount = 10000;
            var tree = new AvlTree<int>();
            var nums = new HashSet<int>();
            var random = new Random();
            for (int i = 0; i < NumCount; i++)
            {
                var num = random.Next(0, NumCount);
                nums.Add(num);
                tree.Add(num);
            }

            var sortedNumbers = nums.OrderBy(n => n).ToArray();
            var expectedSequence = new Queue<int>(sortedNumbers);

            tree.ForeachDfs((depth, num) =>
            {
                Assert.AreEqual(expectedSequence.Dequeue(), num);
            });
        }

        [TestMethod]
        public void AddingMultipleItems_InBalancedWay_ShouldForeachInOrder()
        {
            var numbers = TestUtils.ToIntArray("20 10 30 0 15 25 40");
            var tree = new AvlTree<int>();

            foreach (int number in numbers)
            {
                tree.Add(number);
            }

            var sortedNumbers = numbers.OrderBy(n => n).ToArray();
            var expectedSequence = new Queue<int>(sortedNumbers);

            tree.ForeachDfs((depth, num) =>
            {
                Assert.AreEqual(expectedSequence.Dequeue(), num);
            });
        }

        [TestMethod]
        public void Contains_AddedElement_ShouldReturnTrue()
        {
            var numbers = TestUtils.ToIntArray("-2 3 10 0 1 -16");
            var tree = new AvlTree<int>();
            foreach (int number in numbers)
            {
                tree.Add(number);
            }

            var contains3 = tree.Contains(3);
            Assert.IsTrue(contains3);
        }

        [TestMethod]
        public void Contains_NonAddedElement_ShouldReturnFalse()
        {
            var numbers = TestUtils.ToIntArray("1 7 3 -4 10 0");
            var tree = new AvlTree<int>();
            foreach (int number in numbers)
            {
                tree.Add(number);
            }

            var contains2 = tree.Contains(2);
            Assert.IsFalse(contains2);
        }

        [TestMethod]
        public void Range_NonEmptyTree_ShouldReturnElementsInRangeInOrder()
        {
            const int From = 4;
            const int To = 34;
            var numbers = TestUtils.ToIntArray("20 30 5 8 14 18 -2 0 50 50");
            var tree = new AvlTree<int>();
            foreach (var number in numbers)
            {
                tree.Add(number);
            }

            var actualResult = tree.Range(From, To);
            var expectedResult = TestUtils.ToIntArray("5 8 14 18 20 30");
            CollectionAssert.AreEqual(expectedResult, actualResult.ToArray());
        }

        [TestMethod]
        public void Range_NoItemsInRange_ShouldReturnEmptyCollection()
        {
            const int From = 21;
            const int To = 10000;
            var numbers = TestUtils.ToIntArray("0 0 -10 20 3 4 5 6 7 8 9 10 11 12 13");
            var tree = new AvlTree<int>();
            foreach (var number in numbers)
            {
                tree.Add(number);
            }

            var actualResult = tree.Range(From, To);
            var expectedResult = new int[] {};
            CollectionAssert.AreEqual(expectedResult, actualResult.ToArray());
        }

        [TestMethod]
        public void Range_EmptyTree_ShouldReturnEmptyCollection()
        {
            const int From = 4;
            const int To = 34;
            var tree = new AvlTree<int>();

            var actualResult = tree.Range(From, To);
            var expectedResult = new int[] {};
            CollectionAssert.AreEqual(expectedResult, actualResult.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Range_InvalidFromToArguments_ShouldThrow()
        {
            const int From = 34;
            const int To = 4;
            var tree = new AvlTree<int>();
            tree.Range(From, To);
        }

        [TestMethod]
        public void Index_ValidIndex_ShouldReturnCorrectElement()
        {
            var numbers = TestUtils.ToIntArray("20 30 5 8 14 18 -2 0 50 50");
            var tree = new AvlTree<int>();
            foreach (var number in numbers)
            {
                tree.Add(number);
            }

            var actualResult = tree[3];
            Assert.AreEqual(8, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Index_InValidIndex_ShouldThrow()
        {
            var numbers = TestUtils.ToIntArray("20 30 5 8 14 18 -2 0 50 50");
            var tree = new AvlTree<int>();
            foreach (var number in numbers)
            {
                tree.Add(number);
            }

            var actualResult = tree[numbers.Length];
        }
    }
}
