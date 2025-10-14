using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetArrayTest;

public class _13
{
    public int RomanToInt(string s)
    {
        Dictionary<string, int> romanDict = new Dictionary<string, int>
        {
            { "I", 1 },
            { "IV", 4 },    
            { "V", 5 },
            { "IX", 9 },    
            { "X", 10 },
            { "XL", 40 },   
            { "L", 50 },
            { "XC", 90 },   
            { "C", 100 },
            { "CD", 400 },  
            { "D", 500 },
            { "CM", 900 },  
            { "M", 1000 }
        };
        int result = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (i + 1 < s.Length)
            {
                string twoLettersKey = String.Concat(s[i].ToString(), s[i + 1].ToString());
                if (romanDict.ContainsKey(twoLettersKey))
                {
                    result += romanDict[twoLettersKey];
                    i++; 
                }
                else
                {
                    result += romanDict[s[i].ToString()];
                }    
            }
            else
            {
                result += romanDict[s[i].ToString()];
            }
        }
        return result;
    }
}

[TestClass]
public class _13Test
{
    private _13 _solution = new _13();

    [TestMethod]
    public void RomanToInt_SingleCharacters_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(1, _solution.RomanToInt("I"));
        Assert.AreEqual(5, _solution.RomanToInt("V"));
        Assert.AreEqual(10, _solution.RomanToInt("X"));
        Assert.AreEqual(50, _solution.RomanToInt("L"));
        Assert.AreEqual(100, _solution.RomanToInt("C"));
        Assert.AreEqual(500, _solution.RomanToInt("D"));
        Assert.AreEqual(1000, _solution.RomanToInt("M"));
    }

    [TestMethod]
    public void RomanToInt_SimpleAddition_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(2, _solution.RomanToInt("II"));
        Assert.AreEqual(3, _solution.RomanToInt("III"));
        Assert.AreEqual(6, _solution.RomanToInt("VI"));
        Assert.AreEqual(7, _solution.RomanToInt("VII"));
        Assert.AreEqual(8, _solution.RomanToInt("VIII"));
        Assert.AreEqual(11, _solution.RomanToInt("XI"));
        Assert.AreEqual(12, _solution.RomanToInt("XII"));
        Assert.AreEqual(13, _solution.RomanToInt("XIII"));
    }

    [TestMethod]
    public void RomanToInt_SubtractionRules_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(4, _solution.RomanToInt("IV"));   // 5 - 1
        Assert.AreEqual(9, _solution.RomanToInt("IX"));   // 10 - 1
        Assert.AreEqual(40, _solution.RomanToInt("XL"));  // 50 - 10
        Assert.AreEqual(90, _solution.RomanToInt("XC"));  // 100 - 10
        Assert.AreEqual(400, _solution.RomanToInt("CD")); // 500 - 100
        Assert.AreEqual(900, _solution.RomanToInt("CM")); // 1000 - 100
    }

    [TestMethod]
    public void RomanToInt_ComplexNumbers_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(14, _solution.RomanToInt("XIV"));    // 10 + (5 - 1)
        Assert.AreEqual(19, _solution.RomanToInt("XIX"));    // 10 + (10 - 1)
        Assert.AreEqual(24, _solution.RomanToInt("XXIV"));   // 20 + (5 - 1)
        Assert.AreEqual(44, _solution.RomanToInt("XLIV"));   // (50 - 10) + (5 - 1)
        Assert.AreEqual(49, _solution.RomanToInt("XLIX"));   // (50 - 10) + (10 - 1)
        Assert.AreEqual(94, _solution.RomanToInt("XCIV"));   // (100 - 10) + (5 - 1)
        Assert.AreEqual(99, _solution.RomanToInt("XCIX"));   // (100 - 10) + (10 - 1)
    }

    [TestMethod]
    public void RomanToInt_LargeNumbers_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(444, _solution.RomanToInt("CDXLIV"));     // 400 + 40 + 4
        Assert.AreEqual(494, _solution.RomanToInt("CDXCIV"));     // 400 + 90 + 4
        Assert.AreEqual(944, _solution.RomanToInt("CMXLIV"));     // 900 + 40 + 4
        Assert.AreEqual(994, _solution.RomanToInt("CMXCIV"));     // 900 + 90 + 4
        Assert.AreEqual(1994, _solution.RomanToInt("MCMXCIV"));   // 1000 + 900 + 90 + 4
        Assert.AreEqual(3999, _solution.RomanToInt("MMMCMXCIX")); // 3000 + 900 + 90 + 9
    }

    [TestMethod]
    public void RomanToInt_LeetCodeExamples_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(3, _solution.RomanToInt("III"));
        Assert.AreEqual(58, _solution.RomanToInt("LVIII"));   // L = 50, V= 5, III = 3
        Assert.AreEqual(1994, _solution.RomanToInt("MCMXCIV")); // M = 1000, CM = 900, XC = 90, IV = 4
    }

    [TestMethod]
    public void RomanToInt_MinimumValue_ReturnsOne()
    {
        // Arrange & Act
        int result = _solution.RomanToInt("I");

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void RomanToInt_MaximumValue_ReturnsCorrectValue()
    {
        // Arrange & Act
        int result = _solution.RomanToInt("MMMCMXCIX"); // 3999

        // Assert
        Assert.AreEqual(3999, result);
    }

    [TestMethod]
    public void RomanToInt_ConsecutiveSubtractions_HandlesCorrectly()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(1904, _solution.RomanToInt("MCMIV"));  // 1000 + 900 + 4
        Assert.AreEqual(1909, _solution.RomanToInt("MCMIX"));  // 1000 + 900 + 9
    }

    [TestMethod]
    public void RomanToInt_VariousYears_ReturnsCorrectValues()
    {
        // Arrange & Act & Assert
        Assert.AreEqual(1776, _solution.RomanToInt("MDCCLXXVI"));  // American Independence
        Assert.AreEqual(2000, _solution.RomanToInt("MM"));         // Year 2000
        Assert.AreEqual(2024, _solution.RomanToInt("MMXXIV"));     // Current year
        Assert.AreEqual(1066, _solution.RomanToInt("MLXVI"));      // Battle of Hastings
        Assert.AreEqual(753, _solution.RomanToInt("DCCLIII"));     // Founding of Rome
    }

    [TestMethod]
    public void RomanToInt_NumbersWithAllSymbols_ReturnsCorrectValues()
    {
        // Test numbers that use all Roman numeral symbols
        Assert.AreEqual(1666, _solution.RomanToInt("MDCLXVI"));    // M + D + C + L + X + V + I
        Assert.AreEqual(2574, _solution.RomanToInt("MMDLXXIV"));   // Complex combination
    }
}
