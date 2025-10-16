namespace LeetHashmapTest;

[TestClass]
public class _202_Happy_Number
{
    public bool IsHappy3(int n)
    {
        // 26.64% runtime

        if (n == 1) return true;
        if (n < 10 && n * n < 10) return false;

        HashSet<int> seenNum = new();
        int result = 0;
        while(result != 1)
        {
            if (seenNum.Contains(result)) return false;
            seenNum.Add(result);

            int temp = 0;
            while (n > 0)
            {
                temp += (int)Math.Pow(n % 10, 2);
                n /= 10;
            }
            result = temp;
            result += (int)Math.Pow(n, 2);
            n = result;
        }      
        return true;
    }

    // 100% runtime
    private int Next(int x)
    {
        int sum = 0;
        while (x > 0)
        {
            int d = x % 10;
            sum += d * d;
            x /= 10;
        }
        return sum;
    }
    public bool IsHappy(int n)
    {
        int slow = n;
        int fast = n;
        do
        {
            slow = Next(slow); // move one step
            fast = Next(Next(fast)); // move two steps
        } while (slow != fast);

        return slow == 1;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: n = 1 -> true (1 is happy by definition)
        int n = 1;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleTrue2()
    {
        // Test case: n = 7 -> true
        // 7 -> 49 -> 97 -> 130 -> 10 -> 1
        int n = 7;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleFalse()
    {
        // Test case: n = 2 -> false (enters cycle)
        int n = 2;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_ClassicExample()
    {
        // Test case: n = 19 -> true
        // 19 -> 82 -> 68 -> 100 -> 1
        int n = 19;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleDigitHappy()
    {
        // Test case: n = 7 -> true
        int n = 7;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SingleDigitUnhappy()
    {
        // Test case: n = 4 -> false
        int n = 4;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_TwoDigitHappy()
    {
        // Test case: n = 10 -> true
        // 10 -> 1 -> 1
        int n = 10;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_TwoDigitUnhappy()
    {
        // Test case: n = 20 -> false (enters cycle)
        int n = 20;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_ThreeDigitHappy()
    {
        // Test case: n = 100 -> true
        // 100 -> 1 -> 1
        int n = 100;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_ThreeDigitUnhappy()
    {
        // Test case: n = 145 -> false (enters cycle)
        int n = 145;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LargeHappyNumber()
    {
        // Test case: n = 1000 -> true
        // 1000 -> 1 -> 1
        int n = 1000;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_AnotherHappyNumber()
    {
        // Test case: n = 23 -> true
        int n = 23;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_CommonUnhappyNumber()
    {
        // Test case: n = 5 -> false
        int n = 5;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LargeUnhappyNumber()
    {
        // Test case: n = 999 -> false
        int n = 999;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_EdgeCaseSmall()
    {
        // Test case: n = 3 -> false
        int n = 3;
        bool expected = false;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_BigNumber()
    {
        
        int n = 1563712132;
        bool expected = true;
        bool actual = IsHappy(n);
        Assert.AreEqual(expected, actual);
    }
}
