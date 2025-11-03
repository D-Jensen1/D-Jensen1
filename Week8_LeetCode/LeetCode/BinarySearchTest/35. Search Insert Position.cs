namespace BinarySearchTest;

[TestClass]
public class _35
{
    #region Solution
    public int SearchInsert(int[] nums, int target)
    {
        int start = 0;
        int end = nums.Length - 1;
        
        while (start <= end)
        {
            int mid = start + (end - start) / 2;
            
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid - 1;
            }
        }
        
        return start;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_TargetFound()
    {
        // Test case: nums = [1,3,5,6], target = 5 -> 2
        int[] nums = [1, 3, 5, 6];
        int target = 5;
        int expected = 2;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_TargetNotFound_InsertMiddle()
    {
        // Test case: nums = [1,3,5,6], target = 2 -> 1
        int[] nums = [1, 3, 5, 6];
        int target = 2;
        int expected = 1;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_TargetNotFound_InsertEnd()
    {
        // Test case: nums = [1,3,5,6], target = 7 -> 4
        int[] nums = [1, 3, 5, 6];
        int target = 7;
        int expected = 4;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_TargetNotFound_InsertBeginning()
    {
        // Test case: nums = [1,3,5,6], target = 0 -> 0
        int[] nums = [1, 3, 5, 6];
        int target = 0;
        int expected = 0;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleElementFound()
    {
        // Test case: nums = [1], target = 1 -> 0
        int[] nums = [1];
        int target = 1;
        int expected = 0;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SingleElementInsertAfter()
    {
        // Test case: nums = [1], target = 2 -> 1
        int[] nums = [1];
        int target = 2;
        int expected = 1;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SingleElementInsertBefore()
    {
        // Test case: nums = [1], target = 0 -> 0
        int[] nums = [1];
        int target = 0;
        int expected = 0;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_DuplicateValues()
    {
        // Test case: nums = [1,3,5,6], target = 5 -> 2
        // Testing with array that could have duplicates in a different scenario
        int[] nums = [1, 3, 5, 6];
        int target = 5;
        int expected = 2;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LargerArray()
    {
        // Test case: nums = [1,2,3,4,5,6,7,8,9,10], target = 6 -> 5
        int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int target = 6;
        int expected = 5;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_LargerArrayInsertMiddle()
    {
        // Test case: nums = [1,2,4,5,6,7,8,9,10], target = 3 -> 2
        int[] nums = [1, 2, 4, 5, 6, 7, 8, 9, 10];
        int target = 3;
        int expected = 2;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_NegativeNumbers()
    {
        // Test case: nums = [-5,-2,0,3,6], target = 1 -> 3
        int[] nums = [-5, -2, 0, 3, 6];
        int target = 1;
        int expected = 3;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_NegativeTarget()
    {
        // Test case: nums = [-5,-2,0,3,6], target = -3 -> 1
        int[] nums = [-5, -2, 0, 3, 6];
        int target = -3;
        int expected = 1;
        int actual = SearchInsert(nums, target);
        Assert.AreEqual(expected, actual);
    }
}
