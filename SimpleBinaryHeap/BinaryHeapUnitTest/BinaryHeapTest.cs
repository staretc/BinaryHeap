using BinaryHeapLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeapUnitTest
{
    [TestClass]
    public class BinaryHeapTest
    {
        [TestMethod]
        public void Creating_IsNotNull()
        {
            var heap = new BinaryHeap<int>();
            Assert.IsNotNull(heap);
        }
        [TestMethod]
        public void Count_EqualZeroAfterCreation()
        {
            var heap = new BinaryHeap<int>();
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, heap.Count);
        }
        [TestMethod]
        public void Count_IncreasesAfterAdding()
        {
            var heap = new BinaryHeap<int>();
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                heap.Add(i);
            }
            Assert.AreEqual(count, heap.Count);
        }
        [TestMethod]
        public void Count_DecreasesAfterRemoving()
        {
            var heap = new BinaryHeap<int>();
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                heap.Add(i);
            }
            for (int i = 0; i < count; i++)
            {
                heap.Remove();
            }
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, heap.Count);
        }
        [TestMethod]
        public void Add_Remove_MaxHeap_NonExistentItems_ShouldBeAddedAndRemoved()
        {
            var heap = new BinaryHeap<int>();
            var count = 100;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
            }
            var list = hashset.ToList();
            list.Sort();
            while(count > 0)
            {
                var currentItem = heap.Remove();
                Assert.AreEqual(list[count - 1], currentItem);
                count--;
            }
        }
        [TestMethod]
        public void Add_Remove_MaxHeap_DuplicateItems_ShouldBeAddedAndRemoved()
        {
            var heap = new BinaryHeap<int>();
            var count = 200;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count / 2)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
                heap.Add(item);
            }
            var list = hashset.ToList();
            list = list.Concat(list).ToList();
            list.Sort();
            while (count > 0)
            {
                var currentItem = heap.Remove();
                Assert.AreEqual(list[count - 1], currentItem);
                count--;
            }
        }
        [TestMethod]
        public void Add_Remove_MinHeap_NonExistentItems_ShouldBeAddedAndRemoved()
        {
            var heap = new BinaryHeap<int>(new MinHeapComparer<int>());
            var count = 100;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
            }
            var list = hashset.ToList();
            list.Sort();
            list.Reverse();
            while (count > 0)
            {
                var currentItem = heap.Remove();
                Assert.AreEqual(list[count - 1], currentItem);
                count--;
            }
        }
        [TestMethod]
        public void Add_Remove_MinHeap_DuplicateItems_ShouldBeAddedAndRemoved()
        {
            var heap = new BinaryHeap<int>(new MinHeapComparer<int>());
            var count = 200;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count / 2)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
                heap.Add(item);
            }
            var list = hashset.ToList();
            list = list.Concat(list).ToList();
            list.Sort();
            list.Reverse();
            while (count > 0)
            {
                var currentItem = heap.Remove();
                Assert.AreEqual(list[count - 1], currentItem);
                count--;
            }
        }
        [TestMethod]
        public void Peek_MaxHeap_ShouldReturnMaxItem()
        {
            var heap = new BinaryHeap<int>();
            var count = 100;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
            }
            var list = hashset.ToList();
            list.Sort();
            while (count > 0)
            {
                Assert.AreEqual(list[count - 1], heap.Peek());
                heap.Remove();
                count--;
            }
        }
        [TestMethod]
        public void Peek_MinHeap_ShouldReturnMinItem()
        {
            var heap = new BinaryHeap<int>(new MinHeapComparer<int>());
            var count = 100;
            var hashset = new HashSet<int>();
            var random = new Random();
            while (hashset.Count < count)
            {
                hashset.Add(random.Next(1, 3 * count));
            }
            foreach (var item in hashset)
            {
                heap.Add(item);
            }
            var list = hashset.ToList();
            list.Sort();
            list.Reverse();
            while (count > 0)
            {
                Assert.AreEqual(list[count - 1], heap.Peek());
                heap.Remove();
                count--;
            }
        }
    }
}
