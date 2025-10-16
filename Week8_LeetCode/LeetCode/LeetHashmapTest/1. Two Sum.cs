namespace LeetHashmapTest;

[TestClass]
public class _1
{
    public int[] TwoSum(int[] nums, int target)
    {
        //99.09% runtime
        Dictionary<int, int> dict = new();

        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.ContainsKey(target - nums[i]))
            {
                return [dict[target - nums[i]], i];
            }

            dict.TryAdd(nums[i], i);
        }
        return [0, 0];
    }

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: nums = [2,7,11,15], target = 9 -> [0,1]
        int[] nums = { 2, 7, 11, 15 };
        int target = 9;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SecondExample()
    {
        // Test case: nums = [3,2,4], target = 6 -> [1,2]
        int[] nums = { 3, 2, 4 };
        int target = 6;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_ThirdExample()
    {
        // Test case: nums = [3,3], target = 6 -> [0,1]
        int[] nums = { 3, 3 };
        int target = 6;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_NegativeNumbers()
    {
        // Test case: nums = [-1,-2,-3,-4,-5], target = -8 -> [2,4]
        int[] nums = { -1, -2, -3, -4, -5 };
        int target = -8;
        int[] expected = { 2, 4 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_MixedNumbers()
    {
        // Test case: nums = [-3,4,3,90], target = 0 -> [0,2]
        int[] nums = { -3, 4, 3, 90 };
        int target = 0;
        int[] expected = { 0, 2 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_ZeroTarget()
    {
        // Test case: nums = [0,4,3,0], target = 0 -> [0,3]
        int[] nums = { 0, 4, 3, 0 };
        int target = 0;
        int[] expected = { 0, 3 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_LargeNumbers()
    {
        // Test case: nums = [1000000, 2000000, 3000000], target = 3000000 -> [0,1]
        int[] nums = { 1000000, 2000000, 3000000 };
        int target = 3000000;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_AdjacentElements()
    {
        // Test case: nums = [1,2,3,4,5], target = 3 -> [0,1]
        int[] nums = { 1, 2, 3, 4, 5 };
        int target = 3;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_FarApartElements()
    {
        // Test case: nums = [1,5,6,2,3], target = 4 -> [0,3]
        int[] nums = { 1, 5, 6, 2, 3 };
        int target = 4;
        int[] expected = { 0, 4 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_DuplicateValues()
    {
        // Test case: nums = [1,1,2,2,3], target = 2 -> [0,1]
        int[] nums = { 1, 1, 2, 2, 3 };
        int target = 2;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LastTwoElements()
    {
        // Test case: nums = [1,2,3,4], target = 7 -> [2,3]
        int[] nums = { 1, 2, 3, 4 };
        int target = 7;
        int[] expected = { 2, 3 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_NegativeTarget()
    {
        // Test case: nums = [1,-1,0], target = -1 -> [1,2]
        int[] nums = { 1, -1, 0 };
        int target = -1;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_MinimumArray()
    {
        // Test case: nums = [1,2], target = 3 -> [0,1]
        int[] nums = { 1, 2 };
        int target = 3;
        int[] expected = { 0, 1 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LargeTarget()
    {
        // Test case: nums = [5,10,15,20], target = 35 -> [2,3]
        int[] nums = { 5, 10, 15, 20 };
        int target = 35;
        int[] expected = { 2, 3 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_UnsortedArray()
    {
        // Test case: nums = [15,2,7,11], target = 9 -> [1,2]
        int[] nums = { 15, 2, 7, 11 };
        int target = 9;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(nums, target);
        CollectionAssert.AreEqual(expected, actual);
    }
}
