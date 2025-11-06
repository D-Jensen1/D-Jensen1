namespace MathTest;

[TestClass]
public class _9
{
    #region Solution
    public bool IsPalindrome(int x)
    {
        if (x < 0) return false;
        if (x < 10) return true;
        if (x % 10 == 0) return false;

        int reverse = 0;
        int temp = x;

        while (temp!=0)
        {
            int lastDigit = temp % 10;
            temp = temp / 10;
            reverse = reverse * 10 + lastDigit;
        }
        return reverse == x;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_PositivePalindrome()
    {
        // Test case: x = 121 -> true
        int x = 121;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_NegativeNumber()
    {
        // Test case: x = -121 -> false
        int x = -121;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_NonPalindrome()
    {
        // Test case: x = 10 -> false
        int x = 10;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleDigit()
    {
        // Test case: x = 7 -> true
        int x = 7;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_Zero()
    {
        // Test case: x = 0 -> true
        int x = 0;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_EvenDigitPalindrome()
    {
        // Test case: x = 1221 -> true
        int x = 1221;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_OddDigitPalindrome()
    {
        // Test case: x = 12321 -> true
        int x = 12321;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_LargePalindrome()
    {
        // Test case: x = 1234321 -> true
        int x = 1234321;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LargeNonPalindrome()
    {
        // Test case: x = 1234567 -> false
        int x = 1234567;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_TwoDigitPalindrome()
    {
        // Test case: x = 11 -> true
        int x = 11;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_TwoDigitNonPalindrome()
    {
        // Test case: x = 12 -> false
        int x = 12;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ThreeDigitPalindrome()
    {
        // Test case: x = 101 -> true
        int x = 101;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_ThreeDigitNonPalindrome()
    {
        // Test case: x = 102 -> false
        int x = 102;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_NumberEndingInZero()
    {
        // Test case: x = 100 -> false
        int x = 100;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_LargeNumberEndingInZero()
    {
        // Test case: x = 12320 -> false
        int x = 12320;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_FourDigitPalindrome()
    {
        // Test case: x = 1001 -> true
        int x = 1001;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod17_FiveDigitPalindrome()
    {
        // Test case: x = 12021 -> true
        int x = 12021;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod18_SixDigitPalindrome()
    {
        // Test case: x = 123321 -> true
        int x = 123321;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod19_AllSameDigits()
    {
        // Test case: x = 7777 -> true
        int x = 7777;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod20_MaxIntPalindrome()
    {
        // Test case: x = 1234554321 -> true (within int range)
        int x = 1234554321;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod21_EdgeCaseSmallNegative()
    {
        // Test case: x = -1 -> false
        int x = -1;
        bool expected = false;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod22_VeryLargeNonPalindrome()
    {
        // Test case: x = 2147447412 -> true
        int x = 2147447412;
        bool expected = true;
        bool actual = IsPalindrome(x);
        Assert.AreEqual(expected, actual);
    }
}
