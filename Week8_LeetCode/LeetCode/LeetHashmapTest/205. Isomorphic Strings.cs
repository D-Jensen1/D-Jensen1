using System.Text;

namespace LeetHashmapTest;

[TestClass]
public class _205
{
    public bool IsIsomorphic(string s, string t)
    {
        //6.44% runtime
        var dictS = new Dictionary<char, char>();
        var dictT = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            if ((dictS.ContainsKey(s[i]) && dictS[s[i]] != t[i]) ||
                (dictS.ContainsKey(s[i]) && !dictS.ContainsValue(t[i])))
            {
                return false;
            }
            else if (!dictS.ContainsKey(s[i]))
            {
                dictS.Add(s[i], t[i]);
            }

            if ((dictT.ContainsKey(t[i]) && dictT[t[i]] != s[i]) ||
                (dictT.ContainsKey(t[i]) && !dictT.ContainsValue(s[i])))
            {
                return false;
            }
            else if (!dictT.ContainsKey(t[i]))
            {
                dictT.Add(t[i], s[i]);
            }
        }
        return true;
    }

    public bool IsIsomorphic2(string s, string t)
    {
        //7.03% runtime
        var dictS = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            if ((dictS.ContainsKey(s[i]) && dictS[s[i]] != t[i]) ||
                (dictS.ContainsKey(s[i]) && !dictS.ContainsValue(t[i])) ||
                !dictS.ContainsKey(s[i]) && dictS.ContainsValue(t[i]))
            {
                return false;
            }
            else if (!dictS.ContainsKey(s[i]))
            {
                dictS.Add(s[i], t[i]);
            }
        }
        return true;
    }

    public bool IsIsomorphic3(string s, string t)
    {
        //47.72% runtime
        var dictS = new Dictionary<char, char>();
        var dictT = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            char sChar = s[i], tChar = t[i];
            char sValue = '_', tValue = '_';

            if (dictS.TryGetValue(sChar,out sValue))
            {
                if (sValue != tChar) return false;
            }
            else
            {
                if (dictT.TryGetValue(tChar, out tValue))
                {
                    if (tValue != sChar) return false;
                }
            }
            dictS[sChar] = tChar;
            dictT[tChar] = sChar;
        }
        return true;
    }
    public bool IsIsomorphic4(string s, string t)
    {
        //100% runtime
        int[] sCount = new int[128];
        int[] tCount = new int[128];

        for (int i = 0; i < s.Length; i++)
        {
            if (sCount[s[i]] != tCount[t[i]]) return false;

            sCount[s[i]] = i + 1;
            tCount[t[i]] = i + 1;
        }
        return true;
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: s = "egg", t = "add" -> true
        string s = "egg";
        string t = "add";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleFalse()
    {
        // Test case: s = "foo", t = "bar" -> false
        string s = "foo";
        string t = "bar";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleTrue2()
    {
        // Test case: s = "paper", t = "title" -> true
        string s = "paper";
        string t = "title";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_EmptyStrings()
    {
        // Test case: s = "", t = "" -> true
        string s = "";
        string t = "";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleCharacterSame()
    {
        // Test case: s = "a", t = "a" -> true
        string s = "a";
        string t = "a";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SingleCharacterDifferent()
    {
        // Test case: s = "a", t = "b" -> true
        string s = "a";
        string t = "b";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_RepeatedCharacterMapping()
    {
        // Test case: s = "abba", t = "cddc" -> true
        string s = "abba";
        string t = "cddc";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_InvalidRepeatedMapping()
    {
        // Test case: s = "abba", t = "cdcd" -> false
        string s = "abba";
        string t = "cdcd";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_OneToManyMapping()
    {
        // Test case: s = "ab", t = "aa" -> false (two different chars map to same char)
        string s = "ab";
        string t = "aa";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SelfMapping()
    {
        // Test case: s = "abc", t = "abc" -> true
        string s = "abc";
        string t = "abc";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ComplexPattern()
    {
        // Test case: s = "abcabc", t = "xyzxyz" -> true
        string s = "abcabc";
        string t = "xyzxyz";
        bool expected = true;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_ComplexPatternFalse()
    {
        // Test case: s = "abcabc", t = "xyzxzy" -> false
        string s = "abcabc";
        string t = "xyzxzy";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_EdgeCaseBidirectional()
    {
        // Test case: s = "badc", t = "baba" -> false (bidirectional mapping issue)
        string s = "badc";
        string t = "baba";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15()
    {
        // Test case: s = "badc", t = "baba" -> false (bidirectional mapping issue)
        string s = "egcd";
        string t = "adfd";
        bool expected = false;
        bool actual = IsIsomorphic(s, t);
        Assert.AreEqual(expected, actual);
    }
}
