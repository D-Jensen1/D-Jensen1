namespace MathTest;

[TestClass]
public class _69
{
    #region Solution
    public int MySqrt(int x)
    {
        if (x == 0) return 0;
        if (x == 1) return 1;
        
        int start = 1;
        int end = x / 2 + 1; 

        while (start <= end)
        {
            int mid = start + (end - start) / 2;

            if (mid == x / mid)
            {
                return mid;
            }
            else if (mid < x / mid)
            {
                start = mid + 1;
            }
            else
            {
                end = mid - 1;
            }
        }

        return end;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_PerfectSquare()
    {
        // Test case: x = 4 -> 2
        int x = 4;
        int expected = 2;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_NonPerfectSquare()
    {
        // Test case: x = 8 -> 2 (floor of 2.828...)
        int x = 8;
        int expected = 2;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_Zero()
    {
        // Test case: x = 0 -> 0
        int x = 0;
        int expected = 0;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_One()
    {
        // Test case: x = 1 -> 1
        int x = 1;
        int expected = 1;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_LargePerfectSquare()
    {
        // Test case: x = 16 -> 4
        int x = 16;
        int expected = 4;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_Nine()
    {
        // Test case: x = 9 -> 3
        int x = 9;
        int expected = 3;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_TwentyFive()
    {
        // Test case: x = 25 -> 5
        int x = 25;
        int expected = 5;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_NonPerfectLarge()
    {
        // Test case: x = 15 -> 3 (floor of 3.872...)
        int x = 15;
        int expected = 3;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_Two()
    {
        // Test case: x = 2 -> 1 (floor of 1.414...)
        int x = 2;
        int expected = 1;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_Three()
    {
        // Test case: x = 3 -> 1 (floor of 1.732...)
        int x = 3;
        int expected = 1;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LargeNumber()
    {
        // Test case: x = 100 -> 10
        int x = 100;
        int expected = 10;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_VeryLargeNumber()
    {
        // Test case: x = 2147395600 -> 46340 (close to int.MaxValue)
        int x = 2147395600; // 46340^2
        int expected = 46340;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_MaxIntValue()
    {
        // Test case: x = int.MaxValue -> 46340
        int x = int.MaxValue;
        int expected = 46340; // floor(sqrt(2147483647))
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_ThirtySix()
    {
        // Test case: x = 36 -> 6
        int x = 36;
        int expected = 6;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_FortyNine()
    {
        // Test case: x = 49 -> 7
        int x = 49;
        int expected = 7;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_SixtyFour()
    {
        // Test case: x = 64 -> 8
        int x = 64;
        int expected = 8;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod17_EightyOne()
    {
        // Test case: x = 81 -> 9
        int x = 81;
        int expected = 9;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod18_NonPerfectMid()
    {
        // Test case: x = 50 -> 7 (floor of 7.071...)
        int x = 50;
        int expected = 7;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod19_OneHundredTwentyOne()
    {
        // Test case: x = 121 -> 11
        int x = 121;
        int expected = 11;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod20_OneHundredFourtyFour()
    {
        // Test case: x = 144 -> 12
        int x = 144;
        int expected = 12;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod21_SmallNonPerfect()
    {
        // Test case: x = 5 -> 2 (floor of 2.236...)
        int x = 5;
        int expected = 2;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod22_Six()
    {
        // Test case: x = 6 -> 2 (floor of 2.449...)
        int x = 6;
        int expected = 2;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod23_Seven()
    {
        // Test case: x = 7 -> 2 (floor of 2.645...)
        int x = 7;
        int expected = 2;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod24_PowerOfTwoNonPerfect()
    {
        // Test case: x = 1024 -> 32
        int x = 1024;
        int expected = 32;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod25_EdgeCase()
    {
        // Test case: x = 2147483647 (int.MaxValue) -> 46340
        int x = 2147483647;
        int expected = 46340;
        int actual = MySqrt(x);
        Assert.AreEqual(expected, actual);
    }
}
