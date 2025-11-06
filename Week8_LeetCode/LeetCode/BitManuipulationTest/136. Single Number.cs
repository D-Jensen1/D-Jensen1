namespace BitManuipulationTest;

[TestClass]
public class _136
{
    #region Solution
    public int SingleNumber(int[] nums)
    {
        int result = 0;
        foreach (int i in nums)
        {
            result ^= i;
        }
        return result;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: nums = [2,2,1] -> 1
        int[] nums = [2, 2, 1];
        int expected = 1;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_LargerArray()
    {
        // Test case: nums = [4,1,2,1,2] -> 4
        int[] nums = [4, 1, 2, 1, 2];
        int expected = 4;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_SingleElement()
    {
        // Test case: nums = [1] -> 1
        int[] nums = [1];
        int expected = 1;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_NegativeNumbers()
    {
        // Test case: nums = [-1, -1, 5] -> 5
        int[] nums = [-1, -1, 5];
        int expected = 5;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_MixedSignNumbers()
    {
        // Test case: nums = [-3, 7, -3, 2, 2] -> 7
        int[] nums = [-3, 7, -3, 2, 2];
        int expected = 7;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_Zero()
    {
        // Test case: nums = [0, 1, 1] -> 0
        int[] nums = [0, 1, 1];
        int expected = 0;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_LargeNumbers()
    {
        // Test case: nums = [1000, 999, 1000] -> 999
        int[] nums = [1000, 999, 1000];
        int expected = 999;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_RandomOrder()
    {
        // Test case: nums = [3, 5, 3, 7, 5] -> 7
        int[] nums = [3, 5, 3, 7, 5];
        int expected = 7;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LongerArray()
    {
        // Test case: nums = [1, 2, 3, 4, 5, 1, 2, 3, 4] -> 5
        int[] nums = [1, 2, 3, 4, 5, 1, 2, 3, 4];
        int expected = 5;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_PowersOfTwo()
    {
        // Test case: nums = [1, 2, 4, 8, 1, 2, 4] -> 8
        int[] nums = [1, 2, 4, 8, 1, 2, 4];
        int expected = 8;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_NegativeSingle()
    {
        // Test case: nums = [5, 5, -7] -> -7
        int[] nums = [5, 5, -7];
        int expected = -7;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_LargeArray()
    {
        // Test case: nums = [10, 20, 30, 40, 50, 10, 20, 30, 40] -> 50
        int[] nums = [10, 20, 30, 40, 50, 10, 20, 30, 40];
        int expected = 50;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_ConsecutiveNumbers()
    {
        // Test case: nums = [1, 2, 3, 2, 1] -> 3
        int[] nums = [1, 2, 3, 2, 1];
        int expected = 3;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_MaxIntValue()
    {
        // Test case: nums = [int.MaxValue, 1, 1] -> int.MaxValue
        int[] nums = [int.MaxValue, 1, 1];
        int expected = int.MaxValue;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_MinIntValue()
    {
        // Test case: nums = [int.MinValue, 100, 100] -> int.MinValue
        int[] nums = [int.MinValue, 100, 100];
        int expected = int.MinValue;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_DuplicatesAtEnd()
    {
        // Test case: nums = [9, 1, 2, 3, 1, 2, 3] -> 9
        int[] nums = [9, 1, 2, 3, 1, 2, 3];
        int expected = 9;
        int actual = SingleNumber(nums);
        Assert.AreEqual(expected, actual);
    }
}
