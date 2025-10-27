using System.Runtime.InteropServices;

namespace LeetStackTest;

[TestClass]
public class _20
{
    public bool IsValid(string s)
    {
        char[] open = { '(', '[', '{' };
        char[] close = { ')', ']', '}' };


        if (s.Length % 2 != 0) return false;

        Stack<char> stack = new();


        foreach (char c in s)
        {
            int openIdx = Array.IndexOf(open, c);
            int closeIdx = Array.IndexOf(close, c);

            if (openIdx != -1)
            {
                stack.Push(c);
            }
            else if (closeIdx != -1)
            {
                if (stack.Count == 0 || stack.Pop() != open[closeIdx])
                    return false;
            }

        }
        return stack.Count == 0;
    }

    public bool IsValid2(string s)
    {
        Dictionary<char, char> parentheses = new()
        {
            { '(',')' },
            { '[',']' },
            { '{','}' },
        };
        if (s.Length % 2 != 0) return false;

        for (int i = 0; i < s.Length; i++)
        {
            if (parentheses[s[i]] != s[i + 1]) return false;
            i++;
        }
        return true;
    }
    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: s = "()" -> true
        string s = "([])";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleTrue2()
    {
        // Test case: s = "()[]{}" -> true
        string s = "()[]{}";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_BasicExampleFalse()
    {
        // Test case: s = "(]" -> false
        string s = "(]";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_EmptyString()
    {
        // Test case: s = "" -> true
        string s = "";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_NestedParentheses()
    {
        // Test case: s = "((()))" -> true
        string s = "((()))";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_MixedNested()
    {
        // Test case: s = "{[()]}" -> true
        string s = "{[()]}";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_UnmatchedOpening()
    {
        // Test case: s = "(((" -> false
        string s = "(((";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_UnmatchedClosing()
    {
        // Test case: s = ")))" -> false
        string s = ")))";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_WrongOrder()
    {
        // Test case: s = ")(" -> false
        string s = "(){][}";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_MismatchedTypes()
    {
        // Test case: s = "([)]" -> false
        string s = "([)]";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_ComplexValid()
    {
        // Test case: s = "({[()]})" -> true
        string s = "({[()]})";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ComplexInvalid()
    {
        // Test case: s = "({[()]}]" -> false
        string s = "({[()]}]";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_OnlySquareBrackets()
    {
        // Test case: s = "[[[]]]" -> true
        string s = "[[[]]]";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_OnlyCurlyBraces()
    {
        // Test case: s = "{{{}}}" -> true
        string s = "{{{}}}";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_SingleCharacters()
    {
        // Test case: s = "(" -> false
        string s = "(";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_SingleCharactersClosing()
    {
        // Test case: s = ")" -> false
        string s = ")";
        bool expected = false;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod17_LongValidSequence()
    {
        // Test case: s = "(()()())" -> true
        string s = "(()()())";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod18_AlternatingValid()
    {
        // Test case: s = "()[]{}()[]" -> true
        string s = "()[]{}()[]";
        bool expected = true;
        bool actual = IsValid(s);
        Assert.AreEqual(expected, actual);
    }
}
