namespace LeetHashmapTest;

[TestClass]
public class _383
{
    public bool CanConstruct2(string ransomNote, string magazine)
    {
        Dictionary<char, int> dict = new Dictionary<char, int>();
        foreach (var letter in magazine)
        {
            if (!dict.TryAdd(letter,1))
            {
                dict[letter]++;
            }
        }

        foreach (var letter in ransomNote)
        {
            if (!dict.ContainsKey(letter) || dict[letter] == 0)
            {
                return false;
            }
            dict[letter]--;
        }
        return true;
    }

    public bool CanConstruct(string ransomNote, string magazine)
    {
        // Use an array to count character frequencies for lowercase letters a-z
        // Index 0 represents 'a', index 1 represents 'b', etc.
        // Each element stores the count of that character in the magazine
        int[] map = new int[26];

        // Count frequency of each character in magazine
        // Subtract 97 (ASCII value of 'a') to convert char to array index
        // Works because chars can be implicitly converted to int and vice versa
        foreach (char c in magazine)
        {
            map[c - 97]++;
        }

        // Check if ransom note can be constructed from magazine
        // Decrement count for each character used from magazine
        foreach (char n in ransomNote)
        {
            if (map[n - 97] == 0) return false;
            map[n - 97]--;
        }
        return true;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleFalse()
    {
        // Test case: ransomNote = "a", magazine = "b" -> false
        string ransomNote = "a";
        string magazine = "b";
        bool expected = false;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleFalse2()
    {
        // Test case: ransomNote = "aa", magazine = "ab" -> false
        string ransomNote = "aa";
        string magazine = "ab";
        bool expected = false;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleTrue()
    {
        // Test case: ransomNote = "aa", magazine = "aab" -> true
        string ransomNote = "aa";
        string magazine = "aab";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_EmptyRansomNote()
    {
        // Test case: ransomNote = "", magazine = "abc" -> true
        string ransomNote = "";
        string magazine = "abc";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_EmptyMagazine()
    {
        // Test case: ransomNote = "a", magazine = "" -> false
        string ransomNote = "a";
        string magazine = "";
        bool expected = false;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_BothEmpty()
    {
        // Test case: ransomNote = "", magazine = "" -> true
        string ransomNote = "";
        string magazine = "";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SameStrings()
    {
        // Test case: ransomNote = "abc", magazine = "abc" -> true
        string ransomNote = "abc";
        string magazine = "abc";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_RepeatedCharacters()
    {
        // Test case: ransomNote = "aaa", magazine = "aaabbb" -> true
        string ransomNote = "aaa";
        string magazine = "aaabbb";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_InsufficientRepeatedCharacters()
    {
        // Test case: ransomNote = "aaaa", magazine = "aaa" -> false
        string ransomNote = "aaaa";
        string magazine = "aaa";
        bool expected = false;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_ComplexExample()
    {
        // Test case: ransomNote = "hello", magazine = "hlleoo" -> true
        string ransomNote = "hello";
        string magazine = "hlleoo";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_ComplexExampleFalse()
    {
        // Test case: ransomNote = "hello", magazine = "helo" -> false
        string ransomNote = "hello";
        string magazine = "helo";
        bool expected = false;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_LongMagazine()
    {
        // Test case: ransomNote = "abc", magazine = "aabbccdefghijk" -> true
        string ransomNote = "abc";
        string magazine = "aabbccdefghijk";
        bool expected = true;
        bool actual = CanConstruct(ransomNote, magazine);
        Assert.AreEqual(expected, actual);
    }
}
