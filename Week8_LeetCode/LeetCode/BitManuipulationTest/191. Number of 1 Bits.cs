namespace BitManuipulationTest;

[TestClass]
public class _191
{
    #region Solution
    public int HammingWeight(int n)
    {
        int count = 0;
        while (n != 0)
        {
            count++;
            n &= (n - 1); // Brian Kernighan's algorithm: clears the lowest set bit
        }
        return count;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: n = 00000000000000000000000000001011 (11 in decimal) -> 3
        int n = 0b00000000000000000000000000001011;
        int expected = 3;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SingleBit()
    {
        // Test case: n = 00000000000000000000000010000000 (128 in decimal) -> 1
        int n = 0b00000000000000000000000010000000;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_HighBitSet()
    {
        // Test case: n = 11111111111111111111111111111101 (-3 in decimal) -> 31
        int n = unchecked((int)0b11111111111111111111111111111101);
        int expected = 31;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_Zero()
    {
        // Test case: n = 0 -> 0
        int n = 0;
        int expected = 0;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_One()
    {
        // Test case: n = 1 -> 1
        int n = 1;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_MaxValue()
    {
        // Test case: n = 11111111111111111111111111111111 (-1 in decimal) -> 32
        int n = -1; // All bits set
        int expected = 32;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_PowerOfTwo()
    {
        // Test case: n = 16 (10000 in binary) -> 1
        int n = 16;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_AlternatingBits()
    {
        // Test case: n = 10101010101010101010101010101010 (-1431655766 in decimal) -> 16
        int n = unchecked((int)0b10101010101010101010101010101010);
        int expected = 16;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_ConsecutiveBits()
    {
        // Test case: n = 15 (1111 in binary) -> 4
        int n = 15;
        int expected = 4;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_LargeNumber()
    {
        // Test case: n = 255 (11111111 in binary) -> 8
        int n = 255;
        int expected = 8;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SparseBits()
    {
        // Test case: n = 1024 (10000000000 in binary) -> 1
        int n = 1024;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_MixedPattern()
    {
        // Test case: n = 85 (01010101 in binary) -> 4
        int n = 85;
        int expected = 4;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_HighBitOnly()
    {
        // Test case: n = -2147483648 (10000000000000000000000000000000 in binary) -> 1
        int n = unchecked((int)0b10000000000000000000000000000000); // int.MinValue
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_MultipleBitsSet()
    {
        // Test case: n = 7 (111 in binary) -> 3
        int n = 7;
        int expected = 3;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_LargeSparseBits()
    {
        // Test case: n = -2147483647 (10000000000000000000000000000001 in binary) -> 2
        int n = unchecked((int)0b10000000000000000000000000000001);
        int expected = 2;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }
}
