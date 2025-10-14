using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetArrayTest;

public class _28
{
    public int StrStr2(string haystack, string needle)
    {
        if(haystack.Contains(needle))
        {
            for (int i = 0; i < haystack.Length; i++)
            {
                //if (i + needle.Length > haystack.Length) break;

                if (String.Join("", haystack.Skip(i).Take(i + needle.Length)) == needle)
                {
                    return i;
                    
                }
            }
        }
        return -1;
    }

    public int StrStr(string haystack, string needle)
    {
        if (haystack.Length < needle.Length) return -1;
        int windowSize = needle.Length;
        int startingPosition = 0;

        while(startingPosition + windowSize <= haystack.Length)
        {
            if (haystack.Substring(startingPosition, windowSize) == needle) return startingPosition;
            startingPosition++;
        }
        return -1;
    }
}

[TestClass]
public class _28Test
{
    private _28 _solution = new _28();

    [TestMethod]
    public void StrStr_BasicCase_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "sadbutsad";
        string needle = "sad";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_NeedleNotFound_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "leetcode";
        string needle = "leeto";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_EmptyNeedle_ReturnsZero()
    {
        // Arrange
        string haystack = "hello";
        string needle = "";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_EmptyHaystack_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "";
        string needle = "a";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_BothEmpty_ReturnsZero()
    {
        // Arrange
        string haystack = "";
        string needle = "";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_NeedleAtEnd_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "hello world";
        string needle = "world";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void StrStr_NeedleInMiddle_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "programming";
        string needle = "gram";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void StrStr_SingleCharacterNeedle_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "abcdef";
        string needle = "c";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void StrStr_SingleCharacterHaystack_Found_ReturnsZero()
    {
        // Arrange
        string haystack = "a";
        string needle = "a";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_SingleCharacterHaystack_NotFound_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "a";
        string needle = "b";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_NeedleLongerThanHaystack_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "abc";
        string needle = "abcdef";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_MultipleOccurrences_ReturnsFirstIndex()
    {
        // Arrange
        string haystack = "ababcabab";
        string needle = "abab";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result); // First occurrence at index 0
    }

    [TestMethod]
    public void StrStr_PartialMatchThenFullMatch_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "mississippi";
        string needle = "issip";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void StrStr_RepeatingPattern_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "aaaaaaa";
        string needle = "aaa";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_CaseSensitive_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "Hello World";
        string needle = "hello";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result); // Case sensitive - 'H' != 'h'
    }

    [TestMethod]
    public void StrStr_SpecialCharacters_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "hello@world!";
        string needle = "@world";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void StrStr_NumbersInString_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "test123test456";
        string needle = "123";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void StrStr_SpacesInString_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "hello world test";
        string needle = " world";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void StrStr_EntireHaystackIsNeedle_ReturnsZero()
    {
        // Arrange
        string haystack = "testing";
        string needle = "testing";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_LeetCodeExample1_ReturnsCorrectIndex()
    {
        // Arrange - Example from LeetCode
        string haystack = "sadbutsad";
        string needle = "sad";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void StrStr_LeetCodeExample2_ReturnsMinusOne()
    {
        // Arrange - Example from LeetCode
        string haystack = "leetcode";
        string needle = "leeto";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_OverlappingPattern_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "abababab";
        string needle = "baba";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void StrStr_LongNeedleNotFound_ReturnsMinusOne()
    {
        // Arrange
        string haystack = "abcdefghijklmnop";
        string needle = "xyz";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void StrStr_NearEndOfHaystack_ReturnsCorrectIndex()
    {
        // Arrange
        string haystack = "abcdefghijklmnop";
        string needle = "nop";
        
        // Act
        int result = _solution.StrStr(haystack, needle);
        
        // Assert
        Assert.AreEqual(13, result);
    }
}