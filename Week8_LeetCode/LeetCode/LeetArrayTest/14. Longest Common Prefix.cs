using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.NetworkInformation;

namespace LeetArrayTest;

public class _14
{
    public string LongestCommonPrefix(string[] strs)
    {        
        if (strs.Length == 1)
            return strs[0];
        
        string prefix = strs[0];
        
        for (int i = 1; i < strs.Length; i++)
        {
            int prefixCounter = 0;
            while (prefixCounter < prefix.Length && prefixCounter < strs[i].Length && prefix[prefixCounter] == strs[i][prefixCounter])
            {
                prefixCounter++;
            }
            
            prefix = prefix.Substring(0, prefixCounter);
            
            if (prefix.Length == 0)
                return "";
        }
        
        return prefix;
    }

    public string LongestCommonPrefix2(string[] strs)
    {
        if (strs == null) return "";
        if (strs.Length == 1) return strs[0];

        Array.Sort(strs, StringComparer.OrdinalIgnoreCase);

        string first = strs[0];
        string last = strs[strs.Length - 1];

        int i = 0;
        while (i < first.Length && i < last.Length && char.ToLower(first[i]) == char.ToLower(last[i]))
        {
            i++;
        }

        return first.Substring(0, i);
    }
}

[TestClass]
public class _14Test
{
    private _14 _solution = new _14();

    [TestMethod]
    public void LongestCommonPrefix_BasicCase_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["flower", "flow", "flight"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("fl", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_NoCommonPrefix_ReturnsEmptyString()
    {
        // Arrange
        string[] strs = ["dog", "racecar", "car"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_SingleString_ReturnsTheString()
    {
        // Arrange
        string[] strs = ["hello"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("hello", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_AllIdenticalStrings_ReturnsFullString()
    {
        // Arrange
        string[] strs = ["test", "test", "test"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_OneEmptyString_ReturnsEmptyString()
    {
        // Arrange
        string[] strs = ["hello", "", "help"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_AllEmptyStrings_ReturnsEmptyString()
    {
        // Arrange
        string[] strs = ["", "", ""];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_SingleCharacterPrefix_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["a", "ab", "abc"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("a", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_TwoStrings_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["programming", "program"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("program", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_ComplexCase_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["interspecies", "interstellar", "interstate"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("inters", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_CaseSensitive_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["Test", "test", "Testing"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("", result); // Case sensitive - 'T' != 't'
    }

    [TestMethod]
    public void LongestCommonPrefix_DifferentLengths_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["ab", "abcdef", "abcd"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("ab", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_FirstStringLongest_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["abcdefgh", "abc", "ab"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("ab", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_LastStringLongest_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["ab", "abc", "abcdefgh"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("ab", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_SpecialCharacters_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["hello@world", "hello@", "hello@test"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("hello@", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_NumbersInStrings_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["test123", "test456", "test789"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_SpacesInStrings_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["hello world", "hello there", "hello"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("hello", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_LeetCodeExample1_ReturnsCorrectPrefix()
    {
        // Arrange - Example from LeetCode
        string[] strs = ["flower", "flow", "flight"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("fl", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_LeetCodeExample2_ReturnsEmptyString()
    {
        // Arrange - Example from LeetCode
        string[] strs = ["dog", "racecar", "car"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void LongestCommonPrefix_ManyStrings_ReturnsCorrectPrefix()
    {
        // Arrange
        string[] strs = ["prefix1", "prefix2", "prefix3", "prefix4", "prefix5"];
        
        // Act
        string result = _solution.LongestCommonPrefix(strs);
        
        // Assert
        Assert.AreEqual("prefix", result);
    }
}
