namespace BitManuipulationTest;

[TestClass]
public class _190
{
    #region Solution
    public int ReverseBits(int n)
    {
        int result = 0;
        for (int i = 0; i < 32; i++)
        {
            int rightBit = n & 1; //n & 1 returns right most bit of n
            result <<= 1; //shifts all bit to the left by 1 position
            result = result | rightBit; //line 7 gurantees right most bit of result is a 0. Bitwise OR with rightl
            n >>= 1; //shift n to the right to discard the right most bit.
        }
        return result;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: n = 00000010100101000001111010011100 -> 00111001011110000010100101000000
        // Input: 43261596, Output: 964176192
        int n = 0b00000010100101000001111010011100;
        int expected = 0b00111001011110000010100101000000;
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_AllOnes()
    {
        // Test case: n = 11111111111111111111111111111111 -> 11111111111111111111111111111111
        // Input: -1, Output: -1
        int n = -1; // All bits set (0b11111111111111111111111111111111)
        int expected = -1;
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_Zero()
    {
        // Test case: n = 00000000000000000000000000000000 -> 00000000000000000000000000000000
        // Input: 0, Output: 0
        int n = 0;
        int expected = 0;
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleBitLow()
    {
        // Test case: n = 00000000000000000000000000000001 -> 10000000000000000000000000000000
        // Input: 1, Output: -2147483648 (int.MinValue)
        int n = 1;
        int expected = unchecked((int)0b10000000000000000000000000000000); // -2147483648
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleBitHigh()
    {
        // Test case: n = 10000000000000000000000000000000 -> 00000000000000000000000000000001
        // Input: -2147483648 (int.MinValue), Output: 1
        int n = unchecked((int)0b10000000000000000000000000000000); // -2147483648
        int expected = 1;
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_AlternatingPattern()
    {
        // Test case: n = 10101010101010101010101010101010 -> 01010101010101010101010101010101
        // Input: -1431655766, Output: 1431655765
        int n = unchecked((int)0b10101010101010101010101010101010); // -1431655766
        int expected = 0b01010101010101010101010101010101; // 1431655765
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_FirstHalfOnes()
    {
        // Test case: n = 11111111111111110000000000000000 -> 00000000000000001111111111111111
        int n = unchecked((int)0b11111111111111110000000000000000); // -65536
        int expected = 0b00000000000000001111111111111111; // 65535
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_SecondHalfOnes()
    {
        // Test case: n = 00000000000000001111111111111111 -> 11111111111111110000000000000000
        int n = 0b00000000000000001111111111111111; // 65535
        int expected = unchecked((int)0b11111111111111110000000000000000); // -65536
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_SmallNumber()
    {
        // Test case: n = 00000000000000000000000000000010 -> 01000000000000000000000000000000
        // Input: 2, Output: 1073741824
        int n = 2;
        int expected = 0b01000000000000000000000000000000; // 1073741824
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_PowerOfTwo()
    {
        // Test case: n = 00000000000000000000000000001000 -> 00010000000000000000000000000000
        // Input: 8, Output: 268435456
        int n = 8;
        int expected = 0b00010000000000000000000000000000; // 268435456
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SymmetricPattern()
    {
        // Test case: n = 11000000000000000000000000000011 -> 11000000000000000000000000000011
        // Should be the same when reversed (symmetric)
        int n = unchecked((int)0b11000000000000000000000000000011); // -1073741821
        int expected = unchecked((int)0b11000000000000000000000000000011); // -1073741821
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_OnlyMiddleBits()
    {
        // Test case: n = 00000000000011111111000000000000 -> 00000000000011111111000000000000
        int n = 0b00000000000011111111000000000000; // 16711680
        int expected = 0b00000000000011111111000000000000; // 16711680
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_EdgeBitsOnly()
    {
        // Test case: n = 10000000000000000000000000000001 -> 10000000000000000000000000000001
        int n = unchecked((int)0b10000000000000000000000000000001); // -2147483647
        int expected = unchecked((int)0b10000000000000000000000000000001); // -2147483647
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_QuarterPattern()
    {
        // Test case: n = 11110000000000000000000000000000 -> 00000000000000000000000000001111
        int n = unchecked((int)0b11110000000000000000000000000000); // -268435456
        int expected = 0b00000000000000000000000000001111; // 15
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ComplexPattern()
    {
        // Test case: n = 11001100110011001100110011001100 -> 00110011001100110011001100110011
        int n = unchecked((int)0b11001100110011001100110011001100); // -858993460
        int expected = 0b00110011001100110011001100110011; // 858993459
        int actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_LargeNumber()
    {
        // Test case: Large decimal number to test the algorithm
        int n = 123456789;
        int actual = ReverseBits(n);
        int doubleReversed = ReverseBits(actual);
        
        // Double reversal should give us back the original number
        Assert.AreEqual(n, doubleReversed);
    }
}
