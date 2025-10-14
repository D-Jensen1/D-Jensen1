using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetArrayTest;

public class _58
{
    public int LengthOfLastWord(string s)
    {
        string[] words = s.Trim().Split(' ');
        Array.Reverse(words);

        return words[0].Length;
    }

    public int LengthOfLastWord2(string s)
    {
        int endPointer = s.Length - 1;

        while (Char.IsWhiteSpace(s[endPointer]))
        {
            endPointer--;
        }
        int count = 0;
        while (endPointer >= 0 && !Char.IsWhiteSpace(s[endPointer]))
        {
            count++;
            endPointer--;
        }

        return count;
    }    
}

[TestClass]
public class _58Test
{
    private _58 _solution = new _58();

    [TestMethod]
    public void LengthOfLastWord_BasicCase_ReturnsCorrectLength()
    {
        // Arrange
        string s = "Hello World";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void LengthOfLastWord_TrailingSpaces_ReturnsCorrectLength()
    {
        // Arrange
        string s = "   fly me   to   the moon  ";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(4, result); // "moon"
    }

    [TestMethod]
    public void LengthOfLastWord_SingleWord_ReturnsWordLength()
    {
        // Arrange
        string s = "luffy";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void LengthOfLastWord_SingleWordWithSpaces_ReturnsWordLength()
    {
        // Arrange
        string s = "   hello   ";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void LengthOfLastWord_MultipleSpacesBetweenWords_ReturnsCorrectLength()
    {
        // Arrange
        string s = "a   b    c";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(1, result); // "c"
    }

    [TestMethod]
    public void LengthOfLastWord_SingleCharacter_ReturnsOne()
    {
        // Arrange
        string s = "a";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void LengthOfLastWord_TwoWords_ReturnsLastWordLength()
    {
        // Arrange
        string s = "Today is";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(2, result); // "is"
    }

    [TestMethod]
    public void LengthOfLastWord_LongLastWord_ReturnsCorrectLength()
    {
        // Arrange
        string s = "programming";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(11, result);
    }

    [TestMethod]
    public void LengthOfLastWord_NumbersAsWords_ReturnsCorrectLength()
    {
        // Arrange
        string s = "test 123 word";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(4, result); // "word"
    }

    [TestMethod]
    public void LengthOfLastWord_SpecialCharacters_ReturnsCorrectLength()
    {
        // Arrange
        string s = "hello world!";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(6, result); // "world!"
    }

    [TestMethod]
    public void LengthOfLastWord_LeadingSpaces_ReturnsCorrectLength()
    {
        // Arrange
        string s = "    the quick brown fox";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(3, result); // "fox"
    }

    [TestMethod]
    public void LengthOfLastWord_MixedCaseWords_ReturnsCorrectLength()
    {
        // Arrange
        string s = "Hello WoRLd";

        // Act
        int result = _solution.LengthOfLastWord(s);

        // Assert
        Assert.AreEqual(5, result); // "WoRLd"
    }
}