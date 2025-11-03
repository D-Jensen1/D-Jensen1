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
        uint n = 0b00000010100101000001111010011100;
        uint expected = 0b00111001011110000010100101000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_AllOnes()
    {
        // Test case: n = 11111111111111111111111111111111 -> 11111111111111111111111111111111
        // Input: 4294967295 (uint.MaxValue), Output: 4294967295
        uint n = 0b11111111111111111111111111111111;
        uint expected = 0b11111111111111111111111111111111;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_Zero()
    {
        // Test case: n = 00000000000000000000000000000000 -> 00000000000000000000000000000000
        // Input: 0, Output: 0
        uint n = 0;
        uint expected = 0;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleBitLow()
    {
        // Test case: n = 00000000000000000000000000000001 -> 10000000000000000000000000000000
        // Input: 1, Output: 2147483648
        uint n = 1;
        uint expected = 0b10000000000000000000000000000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleBitHigh()
    {
        // Test case: n = 10000000000000000000000000000000 -> 00000000000000000000000000000001
        // Input: 2147483648, Output: 1
        uint n = 0b10000000000000000000000000000000;
        uint expected = 1;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_AlternatingPattern()
    {
        // Test case: n = 10101010101010101010101010101010 -> 01010101010101010101010101010101
        // Input: 2863311530, Output: 1431655765
        uint n = 0b10101010101010101010101010101010;
        uint expected = 0b01010101010101010101010101010101;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_FirstHalfOnes()
    {
        // Test case: n = 11111111111111110000000000000000 -> 00000000000000001111111111111111
        uint n = 0b11111111111111110000000000000000;
        uint expected = 0b00000000000000001111111111111111;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_SecondHalfOnes()
    {
        // Test case: n = 00000000000000001111111111111111 -> 11111111111111110000000000000000
        uint n = 0b00000000000000001111111111111111;
        uint expected = 0b11111111111111110000000000000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_SmallNumber()
    {
        // Test case: n = 00000000000000000000000000000010 -> 01000000000000000000000000000000
        // Input: 2, Output: 1073741824
        uint n = 2;
        uint expected = 0b01000000000000000000000000000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_PowerOfTwo()
    {
        // Test case: n = 00000000000000000000000000001000 -> 00010000000000000000000000000000
        // Input: 8, Output: 268435456
        uint n = 8;
        uint expected = 0b00010000000000000000000000000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SymmetricPattern()
    {
        // Test case: n = 11000000000000000000000000000011 -> 11000000000000000000000000000011
        // Should be the same when reversed (symmetric)
        uint n = 0b11000000000000000000000000000011;
        uint expected = 0b11000000000000000000000000000011;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_OnlyMiddleBits()
    {
        // Test case: n = 00000000000011111111000000000000 -> 00000000000011111111000000000000
        uint n = 0b00000000000011111111000000000000;
        uint expected = 0b00000000000011111111000000000000;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_EdgeBitsOnly()
    {
        // Test case: n = 10000000000000000000000000000001 -> 10000000000000000000000000000001
        uint n = 0b10000000000000000000000000000001;
        uint expected = 0b10000000000000000000000000000001;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_QuarterPattern()
    {
        // Test case: n = 11110000000000000000000000000000 -> 00000000000000000000000000001111
        uint n = 0b11110000000000000000000000000000;
        uint expected = 0b00000000000000000000000000001111;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ComplexPattern()
    {
        // Test case: n = 11001100110011001100110011001100 -> 00110011001100110011001100110011
        uint n = 0b11001100110011001100110011001100;
        uint expected = 0b00110011001100110011001100110011;
        uint actual = ReverseBits(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_LargeNumber()
    {
        // Test case: Large decimal number to test the algorithm
        uint n = 123456789;
        uint actual = ReverseBits(n);
        uint doubleReversed = ReverseBits(actual);
        
        // Double reversal should give us back the original number
        Assert.AreEqual(n, doubleReversed);
    }
}
