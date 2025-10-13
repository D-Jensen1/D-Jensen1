using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeetArrayTest
{
    class _27
    {

        public int RemoveElement(int[] nums, int val)
        {
            int k = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                {
                    nums[k] = nums[i]; // Place non-val element at position k
                    k++;
                }
            }
            return k;
        }

        public int RemoveElement2(int[] nums, int val)
        {
            List<int> temp = new(nums);
            int itemRemoved = temp.RemoveAll(n => n == val);
            Array.Copy(temp.ToArray(), nums, temp.Count);
            return nums.Length - itemRemoved;
        }
    }

    [TestClass]
    public class _27Test
    {
        private _27 _solution = new _27();

        [TestMethod]
        public void RemoveElement_BasicCase_RemovesCorrectly()
        {
            // Arrange
            int[] nums = [3, 2, 2, 3];
            int val = 3;
            int[] expected = [2, 2];
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(2, k);
            CollectionAssert.AreEqual(expected, nums.Take(k).ToArray());
        }

        [TestMethod]
        public void RemoveElement_MultipleOccurrences_RemovesAll()
        {
            // Arrange
            int[] nums = [0, 1, 2, 2, 3, 0, 4, 2];
            int val = 2;
            int[] expected = [0, 1, 4, 0, 3]; // Order doesn't matter, just first k elements
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(5, k);
            // Verify all remaining elements are not equal to val
            for (int i = 0; i < k; i++)
            {
                Assert.AreNotEqual(val, nums[i]);
            }
        }

        [TestMethod]
        public void RemoveElement_EmptyArray_ReturnsZero()
        {
            // Arrange
            int[] nums = [];
            int val = 1;
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(0, k);
        }

        [TestMethod]
        public void RemoveElement_SingleElement_Match_ReturnsZero()
        {
            // Arrange
            int[] nums = [1];
            int val = 1;
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(0, k);
        }

        [TestMethod]
        public void RemoveElement_SingleElement_NoMatch_ReturnsOne()
        {
            // Arrange
            int[] nums = [1];
            int val = 2;
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(1, k);
            Assert.AreEqual(1, nums[0]);
        }

        [TestMethod]
        public void RemoveElement_AllElementsMatch_ReturnsZero()
        {
            // Arrange
            int[] nums = [2, 2, 2, 2];
            int val = 2;
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(0, k);
        }

        [TestMethod]
        public void RemoveElement_NoElementsMatch_ReturnsOriginalLength()
        {
            // Arrange
            int[] nums = [1, 2, 3, 4, 5];
            int val = 6;
            int[] expected = [1, 2, 3, 4, 5];
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(5, k);
            CollectionAssert.AreEqual(expected, nums.Take(k).ToArray());
        }

        [TestMethod]
        public void RemoveElement_ConsecutiveMatches_RemovesCorrectly()
        {
            // Arrange
            int[] nums = [1, 1, 1, 2, 3, 4];
            int val = 1;
        
            // Act
            int k = _solution.RemoveElement(nums, val);
        
            // Assert
            Assert.AreEqual(3, k);
            for (int i = 0; i < k; i++)
            {
                Assert.AreNotEqual(val, nums[i]);
            }
        }
    }
}


