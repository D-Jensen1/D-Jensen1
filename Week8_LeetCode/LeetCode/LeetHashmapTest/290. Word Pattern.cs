using System.Collections.Generic;

namespace LeetHashmapTest;

[TestClass]
public class _290
{
    public bool WordPattern3(string pattern, string s)
    {
        // 20.82% runtime
        string[] words = s.Split(' ');
        if (pattern.Length != words.Length) return false;

        Dictionary<char, string> dictLetters = new();
        Dictionary<string, char> dictWords = new();

        
        int patternCount = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if ((!dictLetters.TryAdd(pattern[i], words[i]) && dictLetters[pattern[i]] != words[i])
                || (!dictWords.TryAdd(words[i], pattern[i]) && dictWords[words[i]] != pattern[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool WordPattern2(string pattern, string s)
    {
        // 52.95% runtime
        string[] words = s.Split(' ');
        if (pattern.Length != words.Length) return false;

        Dictionary<char, string> dictLetters = new();
        Dictionary<string, char> dictWords = new();


        int patternCount = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (!dictLetters.TryAdd(pattern[i], words[i]) && dictLetters[pattern[i]] != words[i]
                || !dictWords.TryAdd(words[i], pattern[i]) && dictWords[words[i]] != pattern[i])
            {
                return false;
            }
        }
        return true;
    }

    public bool WordPattern(string pattern, string s)
    {
        // 52.95% runtime
        string[] words = s.Split(' ');
        if (pattern.Length != words.Length) return false;

        Dictionary<char, string> dictLetters = new();
        Dictionary<string, char> dictWords = new();


        int patternCount = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (!dictLetters.TryAdd(pattern[i], words[i]) && dictLetters[pattern[i]] != words[i])
            {
                return false;
            }
            if (!dictWords.TryAdd(words[i], pattern[i]) && dictWords[words[i]] != pattern[i])
            {
                return false;
            }
        }
        return true;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: pattern = "abba", s = "dog cat cat dog" -> true
        string pattern = "abba";
        string s = "dog cat cat dog";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleFalse()
    {
        // Test case: pattern = "abba", s = "dog cat cat fish" -> false
        string pattern = "abba";
        string s = "dog cat cat fish";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleFalse2()
    {
        // Test case: pattern = "aaaa", s = "dog cat cat dog" -> false
        string pattern = "aaaa";
        string s = "dog cat cat dog";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleCharacterWord()
    {
        // Test case: pattern = "a", s = "dog" -> true
        string pattern = "a";
        string s = "dog";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_DifferentLengths()
    {
        // Test case: pattern = "abc", s = "dog cat" -> false
        string pattern = "abc";
        string s = "dog cat";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_DifferentLengths2()
    {
        // Test case: pattern = "ab", s = "dog cat fish" -> false
        string pattern = "ab";
        string s = "dog cat fish";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_RepeatedPattern()
    {
        // Test case: pattern = "abc", s = "dog cat fish" -> true
        string pattern = "abc";
        string s = "dog cat fish";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_OneToManyMapping()
    {
        // Test case: pattern = "ab", s = "dog dog" -> false (two chars map to same word)
        string pattern = "ab";
        string s = "dog dog";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_ComplexPatternTrue()
    {
        // Test case: pattern = "abcabc", s = "dog cat fish dog cat fish" -> true
        string pattern = "abcabc";
        string s = "dog cat fish dog cat fish";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_ComplexPatternFalse()
    {
        // Test case: pattern = "abcabc", s = "dog cat fish dog cat bird" -> false
        string pattern = "abcabc";
        string s = "dog cat fish dog cat bird";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SameWordDifferentChars()
    {
        // Test case: pattern = "abc", s = "cat cat cat" -> false (same word maps to different chars)
        string pattern = "abc";
        string s = "cat cat cat";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_SameCharSameWord()
    {
        // Test case: pattern = "aaa", s = "cat cat cat" -> true
        string pattern = "aaa";
        string s = "cat cat cat";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LongWordsPattern()
    {
        // Test case: pattern = "ab", s = "constructor destructor" -> true
        string pattern = "ab";
        string s = "constructor destructor";
        bool expected = true;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_BidirectionalMappingIssue()
    {
        // Test case: pattern = "abba", s = "dog dog dog dog" -> false
        string pattern = "abba";
        string s = "dog dog dog dog";
        bool expected = false;
        bool actual = WordPattern(pattern, s);
        Assert.AreEqual(expected, actual);
    }
}
