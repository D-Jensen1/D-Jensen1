namespace LeetHashmapTest;

[TestClass]
public class _242
{
    public bool IsAnagram(string s, string t)
    {
        // 47.87% runtime

        if (t.Length != s.Length) return false;

        Dictionary<char, int> sDict = new Dictionary<char, int>();
        Dictionary<char, int> tDict = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (!sDict.TryAdd(s[i],1))
            {
                sDict[s[i]]++;
            }
            if (!tDict.TryAdd(t[i], 1))
            {
                tDict[t[i]]++;
            }
        }
        foreach (var key in sDict.Keys)
        {
            if (!tDict.ContainsKey(key) || tDict[key] != sDict[key]) return false;
        }
        return true;
    }

    public bool IsAnagram2(string s, string t)
    {
        // 47.87% runtime
        //not working atm
        if (t.Length != s.Length) return false;

        Dictionary<char, int> count = new Dictionary<char, int>();

        foreach (char letter in s)
        {
            if(!count.TryAdd(letter,1))
            {
                count[letter]++;
            }
        }
        foreach (char letter in t)
        {
            if (count[letter] < 0) return false;
            count[letter]--;
        }
        return true;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: s = "anagram", t = "nagaram" -> true
        string s = "anagram";
        string t = "nagaram";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleFalse()
    {
        // Test case: s = "rat", t = "car" -> false
        string s = "rat";
        string t = "car";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_EmptyStrings()
    {
        // Test case: s = "", t = "" -> true
        string s = "";
        string t = "";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleCharacterSame()
    {
        // Test case: s = "a", t = "a" -> true
        string s = "a";
        string t = "a";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleCharacterDifferent()
    {
        // Test case: s = "a", t = "b" -> false
        string s = "a";
        string t = "b";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_DifferentLengths()
    {
        // Test case: s = "abc", t = "abcd" -> false
        string s = "abc";
        string t = "abcd";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SameString()
    {
        // Test case: s = "listen", t = "listen" -> true
        string s = "listen";
        string t = "listen";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_RepeatedCharacters()
    {
        // Test case: s = "aab", t = "aba" -> true
        string s = "aab";
        string t = "aba";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_RepeatedCharactersMismatch()
    {
        // Test case: s = "aab", t = "aaa" -> false
        string s = "aab";
        string t = "aaa";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_ClassicAnagram()
    {
        // Test case: s = "listen", t = "silent" -> true
        string s = "listen";
        string t = "silent";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_AllSameCharacters()
    {
        // Test case: s = "aaa", t = "aaa" -> true
        string s = "aaa";
        string t = "aaa";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_DifferentFrequencies()
    {
        // Test case: s = "abc", t = "def" -> false
        string s = "abc";
        string t = "def";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_LongAnagram()
    {
        // Test case: s = "conversation", t = "voices rant on" (without space) -> true
        string s = "conversation";
        string t = "voicesranton";
        bool expected = true;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_AlmostAnagram()
    {
        // Test case: s = "abcd", t = "abce" -> false (one character different)
        string s = "abcd";
        string t = "abce";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ExtraCharacter()
    {
        // Test case: s = "abc", t = "abcc" -> false (extra character)
        string s = "abc";
        string t = "abcc";
        bool expected = false;
        bool actual = IsAnagram(s, t);
        Assert.AreEqual(expected, actual);
    }
}
