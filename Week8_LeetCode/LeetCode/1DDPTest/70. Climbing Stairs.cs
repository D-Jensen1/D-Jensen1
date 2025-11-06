namespace _1DDPTest;

[TestClass]
public class _70
{
    #region Solution
    public int ClimbStairsRecursive(int n)
    {
        if (n <= 2) return n;

        return ClimbStairsRecursive(n-1)+ ClimbStairsRecursive(n - 2);
    }


    public int ClimbStairs(int n)
    {
        if (n <= 2) return n;
        int[] nums = new int[n + 1];
        nums[0] = 1;
        nums[1] = 1;
        for (int i = 2; i < nums.Length; i++)
        {
            nums[i] = nums[i - 1] + nums[i - 2];
        }
        return nums[n];
    }

    /*
        public int ClimbStairs(int n)
        {
            if (n <= 2) return n;

            // Dynamic programming approach - space optimized
            // dp[i] represents number of ways to reach step i
            // dp[i] = dp[i-1] + dp[i-2] (Fibonacci-like sequence)
            int prev2 = 1; // dp[0] = 1 (one way to stay at ground)
            int prev1 = 2; // dp[1] = 2 (1+1 or 2 steps to reach step 2)

            for (int i = 3; i <= n; i++)
            {
                int current = prev1 + prev2;
                prev2 = prev1;
                prev1 = current;
            }

            return prev1;
        }
    */
    #endregion

    [TestMethod]
    public void TestMethod1_TwoSteps()
    {
        // Test case: n = 2 -> 2
        // Ways: (1+1) or (2)
        int n = 2;
        int expected = 2;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_ThreeSteps()
    {
        // Test case: n = 3 -> 3
        // Ways: (1+1+1), (1+2), (2+1)
        int n = 3;
        int expected = 3;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_OneStep()
    {
        // Test case: n = 1 -> 1
        // Ways: (1)
        int n = 1;
        int expected = 1;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_FourSteps()
    {
        // Test case: n = 4 -> 5
        // Ways: (1+1+1+1), (1+1+2), (1+2+1), (2+1+1), (2+2)
        int n = 4;
        int expected = 5;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_FiveSteps()
    {
        // Test case: n = 5 -> 8
        // Ways: (1+1+1+1+1), (1+1+1+2), (1+1+2+1), (1+2+1+1), (2+1+1+1), (1+2+2), (2+1+2), (2+2+1)
        int n = 5;
        int expected = 8;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SixSteps()
    {
        // Test case: n = 6 -> 13
        int n = 6;
        int expected = 13;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SevenSteps()
    {
        // Test case: n = 7 -> 21
        int n = 7;
        int expected = 21;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_EightSteps()
    {
        // Test case: n = 8 -> 34
        int n = 8;
        int expected = 34;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_TenSteps()
    {
        // Test case: n = 10 -> 89
        int n = 10;
        int expected = 89;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_FifteenSteps()
    {
        // Test case: n = 15 -> 987
        int n = 15;
        int expected = 987;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_TwentySteps()
    {
        // Test case: n = 20 -> 10946
        int n = 20;
        int expected = 10946;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ThirtySteps()
    {
        // Test case: n = 30 -> 1346269
        int n = 30;
        int expected = 1346269;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_FibonacciSequence()
    {
        // Test the Fibonacci-like pattern: f(n) = f(n-1) + f(n-2)
        // Verify that ClimbStairs(n) = ClimbStairs(n-1) + ClimbStairs(n-2)
        int n = 9;
        int result_n = ClimbStairs(n);
        int result_n_minus_1 = ClimbStairs(n - 1);
        int result_n_minus_2 = ClimbStairs(n - 2);
        
        Assert.AreEqual(result_n, result_n_minus_1 + result_n_minus_2);
        Assert.AreEqual(55, result_n); // Expected value for n=9
    }

    [TestMethod]
    public void TestMethod14_SmallCases()
    {
        // Test edge cases
        Assert.AreEqual(1, ClimbStairs(1));
        Assert.AreEqual(2, ClimbStairs(2));
    }

    [TestMethod]
    public void TestMethod15_MediumCase()
    {
        // Test case: n = 12 -> 233
        int n = 12;
        int expected = 233;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_VerifyPattern()
    {
        // Verify the Fibonacci pattern for multiple values
        int[] expectedValues = [1, 2, 3, 5, 8, 13, 21, 34, 55, 89];
        
        for (int i = 0; i < expectedValues.Length; i++)
        {
            int n = i + 1;
            int actual = ClimbStairs(n);
            Assert.AreEqual(expectedValues[i], actual, $"Failed for n={n}");
        }
    }

    [TestMethod]
    public void TestMethod17_LargerNumber()
    {
        // Test case: n = 25 -> 121393
        int n = 25;
        int expected = 121393;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod18_PerformanceTest()
    {
        // Test a relatively large number to ensure efficiency
        // n = 35 should complete quickly with O(n) solution
        int n = 35;
        int expected = 14930352;
        int actual = ClimbStairs(n);
        Assert.AreEqual(expected, actual);
    }
}
