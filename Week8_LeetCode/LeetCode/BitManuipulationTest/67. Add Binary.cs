using System.Text;

namespace BitManuipulationTest;

[TestClass]
public class _67
{
    #region Solution
    public string AddBinary(string a, string b)
    {
        StringBuilder sb = new();
        int carry = 0;
        int i = a.Length - 1;
        int j = b.Length - 1;

        while (i >= 0 || j >= 0 || carry > 0)
        {
            int sum = carry;
            
            if (i >= 0)
            {
                sum += a[i] - '0';
                i--;
            }
            
            if (j >= 0)
            {
                sum += b[j] - '0';
                j--;
            }
            
            sb.Insert(0, sum % 2);
            carry = sum / 2;
        }
        
        return sb.ToString();
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: a = "11", b = "1" -> "100"
        string a = "11";
        string b = "1";
        string expected = "100";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_LongerExample()
    {
        // Test case: a = "1010", b = "1011" -> "10101"
        string a = "1010";
        string b = "1011";
        string expected = "10101";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_SingleDigits()
    {
        // Test case: a = "0", b = "0" -> "0"
        string a = "0";
        string b = "0";
        string expected = "0";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleDigitsWithCarry()
    {
        // Test case: a = "1", b = "1" -> "10"
        string a = "1";
        string b = "1";
        string expected = "10";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_OneZero()
    {
        // Test case: a = "1", b = "0" -> "1"
        string a = "1";
        string b = "0";
        string expected = "1";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_DifferentLengths()
    {
        // Test case: a = "1111", b = "1" -> "10000"
        string a = "1111";
        string b = "1";
        string expected = "10000";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_LongBinaryStrings()
    {
        // Test case: a = "11111111", b = "1" -> "100000000"
        string a = "11111111";
        string b = "1";
        string expected = "100000000";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_EqualLengthNoCarry()
    {
        // Test case: a = "101", b = "010" -> "111"
        string a = "101";
        string b = "010";
        string expected = "111";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_AllOnes()
    {
        // Test case: a = "111", b = "111" -> "1110"
        string a = "111";
        string b = "111";
        string expected = "1110";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_LargerNumbers()
    {
        // Test case: a = "1101", b = "101" -> "10010"
        string a = "1101";
        string b = "101";
        string expected = "10010";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_VeryDifferentLengths()
    {
        // Test case: a = "1", b = "11111" -> "100000"
        string a = "1";
        string b = "11111";
        string expected = "100000";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ComplexCarryPropagation()
    {
        // Test case: a = "1111", b = "1111" -> "11110"
        string a = "1111";
        string b = "1111";
        string expected = "11110";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_LongSequence()
    {
        // Test case: a = "10101010", b = "01010101" -> "11111111"
        string a = "10101010";
        string b = "01010101";
        string expected = "11111111";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_EmptyLikeCase()
    {
        // Test case: a = "0", b = "1" -> "1"
        string a = "0";
        string b = "1";
        string expected = "1";
        string actual = AddBinary(a, b);
        Assert.AreEqual(expected, actual);
    }
}
