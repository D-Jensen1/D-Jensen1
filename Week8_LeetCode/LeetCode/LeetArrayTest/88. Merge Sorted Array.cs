using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetArrayTest
{
    class _88
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (n == 0) return;
            Array.Copy(nums2, 0, nums1, m, n);
            Array.Sort(nums1);
        }

        public void Merge2(int[] nums1, int m, int[] nums2, int n)
        {
            int p1 = m - 1;
            int p2 = n - 1;
            int p = m + n - 1;

            while (p1 >= 0 && p2 >= 0)
            {
                if (nums1[p1] > nums2[p2])
                {
                    nums1[p] = nums1[p1];
                    p1--;
                }
                    
                else
                {
                    nums1[p] = nums2[p2];
                    p2--;
                }
                p--;
            }

            while (p2 >= 0)
            {
                nums1[p] = nums2[p2];
                p2--;
                p--;
            }
                
        }
    };

    [TestClass]
    public class _88Test
    {
        _88 obj = new _88();

        [TestMethod]
        public void Merge_BothArraysNotEmpty_MergesCorrectly()
        {
            _88 merger = new _88();
            int[] nums1 = new int[] { 1, 2, 3, 0, 0, 0 };
            int m = 3;
            int[] nums2 = new int[] { 2, 5, 6 };
            int n = 3;

            merger.Merge2(nums1, m, nums2, n);

            CollectionAssert.AreEqual(new int[] { 1, 2, 2, 3, 5, 6 }, nums1);
        }

        [TestMethod]
        public void Merge_FirstArrayEmpty_MergesCorrectly()
        {
            _88 merger = new _88();
            int[] nums1 = new int[] { 0 };
            int m = 0;
            int[] nums2 = new int[] { 1 };
            int n = 1;

            merger.Merge(nums1, m, nums2, n);

            CollectionAssert.AreEqual(new int[] { 1 }, nums1);
        }

        [TestMethod]
        public void Merge_SecondArrayEmpty_MergesCorrectly()
        {
            _88 merger = new _88();
            int[] nums1 = new int[] { 1, 2, 3, 0, 0, 0 };
            int m = 3;
            int[] nums2 = new int[] { };
            int n = 0;

            merger.Merge(nums1, m, nums2, n);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 0, 0, 0 }, nums1);
        }

        [TestMethod]
        public void Merge_OneElementEach_MergesCorrectly()
        {
            _88 merger = new _88();
            int[] nums1 = new int[] { 1, 0 };
            int m = 1;
            int[] nums2 = new int[] { 2 };
            int n = 1;

            merger.Merge(nums1, m, nums2, n);

            CollectionAssert.AreEqual(new int[] { 1, 2 }, nums1);
        }

        [TestMethod]
        public void Merge_FirstArraySufficientlyLarge_MergesCorrectly()
        {
            _88 merger = new _88();
            int[] nums1 = new int[] { 4, 5, 6, 0, 0, 0 };
            int m = 3;
            int[] nums2 = new int[] { 1, 2, 3 };
            int n = 3;

            merger.Merge(nums1, m, nums2, n);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6 }, nums1);
        }
    }
}

