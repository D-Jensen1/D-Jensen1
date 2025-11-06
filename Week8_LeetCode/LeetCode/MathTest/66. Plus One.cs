namespace MathTest;

[TestClass]
public class _66
{
    #region Solution
    public int[] PlusOne(int[] digits)
    {
        int num = digits.Length;
        for (int i = num - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits;
            }
            digits[i] = 0;
        }
        int[] newDigits = new int[num + 1];
        newDigits[0] = 1;

        return newDigits;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicIncrement()
    {
        // Test case: digits = [1,2,3] -> [1,2,4]
        int[] digits = [1, 2, 3];
        int[] expected = [1, 2, 4];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_CarryOver()
    {
        // Test case: digits = [4,3,2,1] -> [4,3,2,2]
        int[] digits = [4, 3, 2, 1];
        int[] expected = [4, 3, 2, 2];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_AllNines()
    {
        // Test case: digits = [9] -> [1,0]
        int[] digits = [9];
        int[] expected = [1, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_MultipleNines()
    {
        // Test case: digits = [9,9,9] -> [1,0,0,0]
        int[] digits = [9, 9, 9];
        int[] expected = [1, 0, 0, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleDigitNonNine()
    {
        // Test case: digits = [0] -> [1]
        int[] digits = [0];
        int[] expected = [1];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_EndingWithNine()
    {
        // Test case: digits = [1,2,9] -> [1,3,0]
        int[] digits = [1, 2, 9];
        int[] expected = [1, 3, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_MultipleCarries()
    {
        // Test case: digits = [9,9,8] -> [9,9,9]
        int[] digits = [9, 9, 8];
        int[] expected = [9, 9, 9];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_LongNumber()
    {
        // Test case: digits = [1,2,3,4,5,6,7,8,9] -> [1,2,3,4,5,6,7,9,0]
        int[] digits = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        int[] expected = [1, 2, 3, 4, 5, 6, 7, 9, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_TwoDigitsAllNines()
    {
        // Test case: digits = [9,9] -> [1,0,0]
        int[] digits = [9, 9];
        int[] expected = [1, 0, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_MixedDigits()
    {
        // Test case: digits = [7,2,8,5,0,9,1,2,9,5,3,6,6,7,3,2,8,4,3,7,9,5,7,7,4,7,4,9,4,7,0,1,1,1,7,4,0,0,6] -> increment by 1
        int[] digits = [7, 2, 8, 5, 0, 9, 1, 2, 9, 5, 3, 6, 6, 7, 3, 2, 8, 4, 3, 7, 9, 5, 7, 7, 4, 7, 4, 9, 4, 7, 0, 1, 1, 1, 7, 4, 0, 0, 6];
        int[] expected = [7, 2, 8, 5, 0, 9, 1, 2, 9, 5, 3, 6, 6, 7, 3, 2, 8, 4, 3, 7, 9, 5, 7, 7, 4, 7, 4, 9, 4, 7, 0, 1, 1, 1, 7, 4, 0, 0, 7];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_AllZeros()
    {
        // Test case: digits = [0,0,0] -> [0,0,1]
        int[] digits = [0, 0, 0];
        int[] expected = [0, 0, 1];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_LargeNumberNoCarry()
    {
        // Test case: digits = [1,0,0,0,0,0,0,0,0,8] -> [1,0,0,0,0,0,0,0,0,9]
        int[] digits = [1, 0, 0, 0, 0, 0, 0, 0, 0, 8];
        int[] expected = [1, 0, 0, 0, 0, 0, 0, 0, 0, 9];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_SingleNineToTen()
    {
        // Test case: digits = [9] -> [1,0]
        int[] digits = [9];
        int[] expected = [1, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_FourNines()
    {
        // Test case: digits = [9,9,9,9] -> [1,0,0,0,0]
        int[] digits = [9, 9, 9, 9];
        int[] expected = [1, 0, 0, 0, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_PartialCarry()
    {
        // Test case: digits = [8,9,9] -> [9,0,0]
        int[] digits = [8, 9, 9];
        int[] expected = [9, 0, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod17_NoCarryNeeded()
    {
        // Test case: digits = [5,4,3,2,1] -> [5,4,3,2,2]
        int[] digits = [5, 4, 3, 2, 1];
        int[] expected = [5, 4, 3, 2, 2];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod19_EndingNinesWithCarry()
    {
        // Test case: digits = [1,9,9,9] -> [2,0,0,0]
        int[] digits = [1, 9, 9, 9];
        int[] expected = [2, 0, 0, 0];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod20_VeryLargeNumber()
    {
        // Test case: Large number ending in 8
        int[] digits = [9, 8, 7, 6, 5, 4, 3, 2, 1, 8];
        int[] expected = [9, 8, 7, 6, 5, 4, 3, 2, 1, 9];
        int[] actual = PlusOne(digits);
        CollectionAssert.AreEqual(expected, actual);
    }
}
