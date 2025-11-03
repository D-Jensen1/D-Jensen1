namespace BitManuipulationTest;

[TestClass]
public class _191
{
    #region Solution
    public int HammingWeight(int n)
    {
        return 0;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: n = 00000000000000000000000000001011 (11 in decimal) -> 3
        uint n = 0b00000000000000000000000000001011;
        int expected = 3;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SingleBit()
    {
        // Test case: n = 00000000000000000000000010000000 (128 in decimal) -> 1
        uint n = 0b00000000000000000000000010000000;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_HighBitSet()
    {
        // Test case: n = 11111111111111111111111111111101 (4294967293 in decimal) -> 31
        uint n = 0b11111111111111111111111111111101;
        int expected = 31;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_Zero()
    {
        // Test case: n = 0 -> 0
        uint n = 0;
        int expected = 0;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_One()
    {
        // Test case: n = 1 -> 1
        uint n = 1;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_MaxValue()
    {
        // Test case: n = 11111111111111111111111111111111 (uint.MaxValue) -> 32
        uint n = uint.MaxValue;
        int expected = 32;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_PowerOfTwo()
    {
        // Test case: n = 16 (10000 in binary) -> 1
        uint n = 16;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_AlternatingBits()
    {
        // Test case: n = 10101010101010101010101010101010 (2863311530 in decimal) -> 16
        uint n = 0b10101010101010101010101010101010;
        int expected = 16;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_ConsecutiveBits()
    {
        // Test case: n = 15 (1111 in binary) -> 4
        uint n = 15;
        int expected = 4;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_LargeNumber()
    {
        // Test case: n = 255 (11111111 in binary) -> 8
        uint n = 255;
        int expected = 8;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SparseBits()
    {
        // Test case: n = 1024 (10000000000 in binary) -> 1
        uint n = 1024;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_MixedPattern()
    {
        // Test case: n = 85 (01010101 in binary) -> 4
        uint n = 85;
        int expected = 4;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_HighBitOnly()
    {
        // Test case: n = 2147483648 (10000000000000000000000000000000 in binary) -> 1
        uint n = 0b10000000000000000000000000000000;
        int expected = 1;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_MultipleBitsSet()
    {
        // Test case: n = 7 (111 in binary) -> 3
        uint n = 7;
        int expected = 3;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_LargeSparseBits()
    {
        // Test case: n = 2147483649 (10000000000000000000000000000001 in binary) -> 2
        uint n = 0b10000000000000000000000000000001;
        int expected = 2;
        int actual = HammingWeight(n);
        Assert.AreEqual(expected, actual);
    }
}
