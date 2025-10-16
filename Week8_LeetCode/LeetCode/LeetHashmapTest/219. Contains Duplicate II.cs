namespace LeetHashmapTest;

[TestClass]
public class _219
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        // 58.63% runtime
        Dictionary<int, int> dict = new();

        for (int i = 0; i < nums.Length; i++)
        {
            if (!dict.TryAdd(nums[i], i) && Math.Abs(i - dict[nums[i]]) <= k)
            {
                return true;
            }
            dict[nums[i]] = i;
        }
        return false;
    }

    public bool ContainsNearbyDuplicate2(int[] nums, int k)
    {
        int ptrLeft = 0;
        int ptrRight = ptrLeft + k;
        HashSet<int> window = new(nums[ptrLeft..(ptrRight + 1)]);

        while(ptrRight < nums.Length)
        {
            window.Remove(nums[ptrLeft]);
            ptrLeft++;
            if (!window.Add(nums[ptrRight + 1])) return true;
        }
        return false;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: nums = [1,2,3,1], k = 3 -> true
        int[] nums = { 1, 2, 3, 1 };
        int k = 3;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleTrue2()
    {
        // Test case: nums = [1,0,1,1], k = 1 -> true
        int[] nums = { 1, 0, 1, 1 };
        int k = 1;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleFalse()
    {
        // Test case: nums = [1,2,3,1,2,3], k = 2 -> false
        int[] nums = { 1, 2, 3, 1, 2, 3 };
        int k = 2;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleElement()
    {
        // Test case: nums = [1], k = 1 -> false
        int[] nums = { 1 };
        int k = 1;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_NoDuplicates()
    {
        // Test case: nums = [1,2,3,4,5], k = 2 -> false
        int[] nums = { 1, 2, 3, 4, 5 };
        int k = 2;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_KZero()
    {
        // Test case: nums = [1,1], k = 0 -> false (distance must be <= 0, but indices are different)
        int[] nums = { 1, 1 };
        int k = 0;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_AdjacentDuplicates()
    {
        // Test case: nums = [1,2,1], k = 1 -> false (distance is 2, k is 1)
        int[] nums = { 1, 2, 1 };
        int k = 1;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_ExactlyKDistance()
    {
        // Test case: nums = [1,2,1], k = 2 -> true (distance is exactly 2)
        int[] nums = { 1, 2, 1 };
        int k = 2;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_MultipleDuplicates()
    {
        // Test case: nums = [1,2,3,1,4,1], k = 3 -> true
        int[] nums = { 1, 2, 3, 1, 4, 1 };
        int k = 3;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_AllSameElements()
    {
        // Test case: nums = [1,1,1,1], k = 2 -> true
        int[] nums = { 1, 1, 1, 1 };
        int k = 2;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LargeK()
    {
        // Test case: nums = [1,2,3,1], k = 10 -> true (k is larger than array)
        int[] nums = { 1, 2, 3, 1 };
        int k = 10;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_NegativeNumbers()
    {
        // Test case: nums = [-1,-2,-3,-1], k = 3 -> true
        int[] nums = { -1, -2, -3, -1 };
        int k = 3;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_ZeroElements()
    {
        // Test case: nums = [0,1,0], k = 1 -> false
        int[] nums = { 0, 1, 0 };
        int k = 1;
        bool expected = false;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LongArray()
    {
        // Test case: nums = [1,2,3,4,5,6,7,8,9,1], k = 9 -> true
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
        int k = 9;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ConsecutiveDuplicates()
    {
        // Test case: nums = [1,1], k = 1 -> true (consecutive elements)
        int[] nums = { 1, 1 };
        int k = 1;
        bool expected = true;
        bool actual = ContainsNearbyDuplicate(nums, k);
        Assert.AreEqual(expected, actual);
    }
}
