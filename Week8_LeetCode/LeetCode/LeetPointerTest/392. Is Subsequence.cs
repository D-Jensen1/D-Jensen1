namespace LeetPointerTest;

[TestClass]
public class _392
{
    public bool IsSubsequence(string s, string t)
    {
        int ptrS = 0;
        int ptrT = 0;

        if (s.Length == 0) return true;
        if (t.Length == 0) return false;
        

        while (ptrS < s.Length && ptrT < t.Length)
        {
            if (s.Length - ptrS > t.Length - ptrT) return false;

            if (s[ptrS] == t[ptrT])
            {
                ptrS++;
            }
            ptrT++;
        }

        return !(ptrS < s.Length);
    }

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: s = "abc", t = "aebdc" -> true
        string s = "abc";
        string t = "aebdc";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_NotSubsequence()
    {
        // Test case: s = "axc", t = "ahbgdc" -> false
        string s = "axc";
        string t = "ahbgdc";
        bool expected = false;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_EmptyS()
    {
        // Test case: s = "", t = "abc" -> true (empty string is subsequence of any string)
        string s = "";
        string t = "abc";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_EmptyT()
    {
        // Test case: s = "abc", t = "" -> false (non-empty string cannot be subsequence of empty string)
        string s = "abc";
        string t = "";
        bool expected = false;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_BothEmpty()
    {
        // Test case: s = "", t = "" -> true
        string s = "";
        string t = "";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SameStrings()
    {
        // Test case: s = "abc", t = "abc" -> true
        string s = "abc";
        string t = "abc";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SingleCharacterMatch()
    {
        // Test case: s = "b", t = "abc" -> true
        string s = "b";
        string t = "abc";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_SingleCharacterNoMatch()
    {
        // Test case: s = "z", t = "abc" -> false
        string s = "z";
        string t = "abc";
        bool expected = false;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_RepeatedCharacters()
    {
        // Test case: s = "aaa", t = "aaaaa" -> true
        string s = "aaa";
        string t = "aaaaa";
        bool expected = true;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_RepeatedCharactersNoMatch()
    {
        // Test case: s = "aaa", t = "aa" -> false
        string s = "aaa";
        string t = "aa";
        bool expected = false;
        bool actual = IsSubsequence(s, t);
        Assert.AreEqual(expected, actual);
    }
}
