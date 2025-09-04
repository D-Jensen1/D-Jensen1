using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.AccessControl;

namespace LearnTypes;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void OnePlusOneEqualsTwo()
    {
        //Arrange
        int a = 1;
        int b = 1;
        //Act
        int c = a + b;
        //Assert
        Assert.AreEqual(2, c);
    }

    [TestMethod]
    public void LimitOfWholeNumbers() //public members should be PascalCased
    {
        //Arrange
        byte byteMin = byte.MinValue;
        byte byteMax = byte.MaxValue;

        sbyte sbyteMin = sbyte.MinValue;
        sbyte sbyteMax = sbyte.MaxValue;

        short shortMin = short.MinValue;
        short shortMax = short.MaxValue;

        ushort ushortMin = ushort.MinValue;
        ushort ushortMax = ushort.MaxValue;

        int intMin = int.MinValue;
        int intMax = int.MaxValue;

        uint uintMin = uint.MinValue;
        uint uintMax = uint.MaxValue;

        long longMin = long.MinValue;
        long longMax = long.MaxValue;

        ulong ulongMin = ulong.MinValue;
        ulong ulongMax = ulong.MaxValue;
        //Act
        byte byteExpectedMin = 0;
        byte byteExpectedMax = 255;

        sbyte sbyteExpectedMin = -128;
        sbyte sbyteExpectedMax = 127;

        short shortExpectedMin = -32_768;
        short shortExpectedMax = 32_767;

        ushort ushortExpectedMin = 0;
        ushort ushortExpectedMax = 65_535;

        int intExpectedMin = -2_147_483_648;
        int intExpectedMax = 2_147_483_647;

        uint uintExpectedMin = 0;
        uint uintExpectedMax = 4_294_967_295;

        long longExpectedMin = -9_223_372_036_854_775_808;
        long longExpectedMax = 9_223_372_036_854_775_807;

        ulong ulongExpectedMin = 0;
        ulong ulongExpectedMax = 18_446_744_073_709_551_615;
        //Assert
        Assert.AreEqual(byteExpectedMin, byteMin);
        Assert.AreEqual(byteExpectedMax, byteMax);

        Assert.AreEqual(sbyteExpectedMin, sbyteMin);
        Assert.AreEqual(sbyteExpectedMax, sbyteMax);

        Assert.AreEqual(shortExpectedMin, shortMin);
        Assert.AreEqual(shortExpectedMax, shortMax);

        Assert.AreEqual(ushortExpectedMin, ushortMin);
        Assert.AreEqual(ushortExpectedMax, ushortMax);

        Assert.AreEqual(intExpectedMin, intMin);
        Assert.AreEqual(intExpectedMax, intMax);

        Assert.AreEqual(uintExpectedMin, uintMin);
        Assert.AreEqual(uintExpectedMax, uintMax);

        Assert.AreEqual(longExpectedMin, longMin);
        Assert.AreEqual(longExpectedMax, longMax);

        Assert.AreEqual(ulongExpectedMin, ulongMin);
        Assert.AreEqual(ulongExpectedMax, ulongMax);

        Assert.AreEqual("10000000", Convert.ToString(sbyteExpectedMin, 2)[8..]);
    }

    [TestMethod]
    public void IntegrateArithmetics()
    {
        var a = 10;
        int b = 3;

        var c = a / b;
        int d = a % b; //returns remainder of integer divide

        Assert.AreEqual(3, c);
        Assert.AreEqual(1, d);
        Assert.AreEqual(11, ++a);
        Assert.AreEqual(3, b++);
        Assert.AreEqual(4, b);

        int e = 5;
        int f = e;

        e += 3;

        Assert.AreEqual(8, e);
        Assert.AreEqual(5, f);

        int g = int.MaxValue;

        //checked //do not allow wrap around
        //{
        //    try
        //    {
        //        int h = g + 1; 
        //    }
        //    catch (OverflowException ex)
        //    {
        //        Assert.IsTrue(ex is OverflowException);
        //    }   
        //}

        int h = g + 1; //adding to max value wraps around to min value

        Assert.AreEqual(int.MinValue, h);
    }

    [TestMethod]
    public void NumberWithFractions()
    {
        //approximated fractions
        float a = 1.23f; //use LITERAL SUFFIX f - float   d - double    m - decimal
        float f; //4 bytes
        double d = 2.456789E3; //8 bytes

        //precision fractions
        decimal c = 1.2345678907768M;
    }

    [TestMethod]
    public void NauticalMileCalculator()
    {
        //arrange
        float nauticalMile = 100.5F;
        float expectedMile = 115.653F;
        float expectedKm = 186.13f;
        //act
        float actualMile = 0;
        float actualKm = 0;
        const float milesPerNauticalMile = 1.15078F;
        const float kmPerNauticalMile = 1.852F;
        actualMile = nauticalMile * milesPerNauticalMile;
        actualKm = nauticalMile * kmPerNauticalMile;

        Assert.IsTrue(Math.Abs(expectedMile - actualMile) < 0.001);
        Assert.IsTrue(Math.Abs(expectedKm - actualKm) < 0.01);

        //Assert.AreEqual(expectedMile, actualMile);
        //Assert.AreEqual(expectedKm, actualKm);

    }

    [TestMethod]
    public void CompoundInterestCalculator()
    {
        //arrange
        

        decimal expectedFinalBalance = 91_766.39m;
        decimal expectedCompoundInterest = 64_124.16m;
        //act
        const float initialBalance = 27_642.23f;
        const float interestRate = 0.06f;
        const float termInYears = 20f;
        const float compoundRate = 365f;

        decimal actualFinalBalance = 0m;
        decimal actualCompoundInterest = 0m;

        //Formulas
        double periods = compoundRate * termInYears; 
        double ratePerPeriod = interestRate / compoundRate; 
         
        //Added *100 / 100 at the end to make ceiling round up to nearest 100th place
        actualFinalBalance = Math.Ceiling((decimal)(initialBalance * Math.Pow(1 + ratePerPeriod, periods)*100))/100;
        //Not rounded answer
        //actualFinalBalance = (decimal)(initialBalance * Math.Pow(1 + ratePerPeriod, periods));

        actualCompoundInterest = actualFinalBalance - (decimal)initialBalance;

        //assert
        Assert.AreEqual(expectedFinalBalance, actualFinalBalance);
        Assert.AreEqual(expectedCompoundInterest, actualCompoundInterest);
    }

    [TestMethod]
    public void LearnBoolean()
    {
        bool x = true;
        bool y = false;
        Assert.IsTrue(x);
        Assert.IsFalse(y);

        //logical operators
        bool result = x && y;
        Assert.IsFalse(result);

        //AND Truth Table for AND(&&)
        Assert.IsFalse(true && false);
        Assert.IsFalse(false && true);
        Assert.IsTrue (true && true);
        Assert.IsFalse(false && false);

        //OR Truth Table for OR (||)
        Assert.IsTrue (true || false);
        Assert.IsTrue (false || true);
        Assert.IsTrue (true || true);
        Assert.IsFalse(false || false);

        //XOR Truth Table for XOR (^) - there can only be one true statement
        Assert.IsTrue (true ^ false);
        Assert.IsTrue (false ^ true);
        Assert.IsFalse(true ^ true);
        Assert.IsFalse(false ^ false);


        //in c#, we combine !(a && b) to create NAND
        //          combine !(a || b) to create NOR
        //   ! = unary negation operator

        if (true) Assert.IsTrue(true); //simplest if statement

        if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
        {
            Assert.IsTrue(true);
        }

        string isTacoTuesday = (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) ? "It.s taco Tuesday!" : "No taco for you.";
        // ? :   ternary operator
        // (bool expression) ? [return if true] : [return if false];

        //challenge:
        //verify a number between 50 and 100
        int num = 60;

        if (num >= 50 && num <= 100) { Assert.IsTrue(true); }
        
        else { Assert.Fail(); }

        //avoid changing any value in the if condition
    }

    [TestMethod]
    public void ScopeTestA()
    {
        int a = 10; // the scope of a is "method"

        if (true)
        {
            int b = 100; // the scope of b is "block

            if (true)
            {
                int c = 1000; // the scope of c is inner if "block
                Assert.AreEqual(10, a);
                Assert.AreEqual(100, b);
                Assert.AreEqual(1000, c);

            }

            Assert.AreEqual(10, a);
            Assert.AreEqual(100, b);
            //Assert.AreEqual(1000, c); //this will not compile, c is "blocked scope" and not accessible
        }
        Assert.AreEqual(10, a);
        //Assert.AreEqual(100, b); //this will not compile, b is "blocked scope" and not accessible outside of if
    }

    [TestMethod]
    public void ClassVarTest()
    {
        Assert.AreEqual(100, classVar); //classVar isn't defined in this method, its a class level field
        //in .net -- NO variable/data can exist outside of a class. (no global variables)

        //data can be allocated at
        // 1 - code block level
        // 2 - method level
        // 3 - class level
    }

    [TestMethod]
    public void StackTest()
    {
        // managed memory place data into 1 - stack, 2 - heap
        int a = 10;
        StackChild();
        Assert.AreEqual(10, a);
    }
    public void StackChild()
    {
        // managed memory place data into 1 - stack, 2 - heap
        int a = 100;
        StackGrandChild();
        Assert.AreEqual(100, a);
        
    }
    public void StackGrandChild()
    {
        // managed memory place data into 1 - stack, 2 - heap
        int a = 1000;
        Assert.AreEqual(1000, a);
    }

    int classVar = 100; //class member classVar is a "field" of Test1 Class
    //field
    //property
    //event
    //method

    [TestMethod]
    public void LearnDateTime()
    {
        DateTime dt;
        DateTimeOffset dto;

        dt = new(); // Create date time without any specifications. default constructor

        Assert.AreEqual(1, dt.Year);
        Assert.AreEqual(1, dt.Month);
        Assert.AreEqual(1, dt.Day);

        
        dt = new(2025,11,4); // Create a date time using year/month/day

        Assert.AreEqual(0, dt.Hour);
        Assert.AreEqual(0, dt.Minute);
        Assert.AreEqual(0, dt.Millisecond);
        Assert.AreEqual(DayOfWeek.Tuesday, dt.DayOfWeek);
        Assert.AreEqual(DateTimeKind.Unspecified, dt.Kind);

        dt = DateTime.Now; // This gets NOW from local machine/local time

        Assert.AreEqual(247, dt.DayOfYear);
        Assert.AreEqual(DayOfWeek.Thursday, dt.DayOfWeek);

        DateTime yesterday = new(2025, 9, 2);

        Assert.IsTrue(dt > yesterday);

        TimeSpan ageToDrink = new(21*365,0,0,0,0);
        DateTime bornOnThisDate = dt - ageToDrink;

        bornOnThisDate = dt.AddYears(-21);
        Assert.AreEqual(2004, bornOnThisDate.Year);
        Assert.AreEqual(9, bornOnThisDate.Month);
        Assert.AreEqual(4, bornOnThisDate.Day);

        //print out 24 month lease due dates

        //lease payment begins on first day of next month
        dt = DateTime.Now;
        DateTime startDate = new(dt.Year, dt.AddMonths(1).Month, 1);
        
      
        //DateTime startDate = DateTime.Now;
        //while (dt.Month == startDate.Month)
        //{
        //    startDate = startDate.AddDays(1); //immutable object
        //}
        //Console.WriteLine(startDate.ToString());

        for (int i = 0; i < 24; i++)
        {
            Console.WriteLine($"Month {i + 1}: {startDate.AddMonths(i)}");
        }
    }


    [TestMethod]
    public void LearnString()
    {
        string a = "hello";
        char b = 'e';
        char c = (char)101;

        Assert.AreEqual(5, a.Length);
        Assert.AreEqual(b, a[1]);
        Assert.AreEqual(101, b); // 101 is the hex value of char 'e'
        Assert.AreEqual(b, c);


        for (int i = 0; i < 26; i++)
        {
            Console.Write((char)('a' + i));
        }
        Console.WriteLine();

        char unicodeChar = '\u1F63'; //unicode char accomodates 2 bytes unicode

        for (int i = 0; i < 26; i++)
        {
            Console.Write((char)(unicodeChar + i));
        }

        string threeEmojis = "😆🙃🤩";
        Assert.AreEqual(6, threeEmojis.Length);

        string myName = "Dominic";
        Assert.IsTrue(myName.EndsWith('c'));
        Assert.IsTrue(myName.EndsWith("nic"));
        Assert.IsTrue(myName.EndsWith("mIniC",StringComparison.OrdinalIgnoreCase));

        Assert.AreEqual("nic",myName.Substring(4));
        Assert.AreEqual("Dom", myName.Substring(0,3));

        Assert.AreEqual("Dommmminic",myName.Insert(2,"mmm"));
        Assert.AreEqual("Domaxic", myName.Replace("min","max"));
        Assert.AreEqual(3, myName.IndexOfAny(new[] { 'i' }));


    }

    [TestMethod]
    public void LearnArray()
    {
        int[] anIntArray;
        string[] aStringArray;
        float[] aFloatArray;
        DateTime[] aDateTimeArray;

        anIntArray = new int[5];
        Assert.AreEqual(5, anIntArray.Length);
        for (int i = 0; i < anIntArray.Length; i++)
        {
            Assert.AreEqual(0, anIntArray[i]);
        }

        //aStringArray[1] = "hello"; --array must be initialized first
        //aStringArray = new strin[] {"apple", "banana", "cherry"}; --old syntax
        aStringArray = ["apple", "banana", "cherry"]; //[...,...,...] collection initializer
        aFloatArray = [1.2f, 3.7f, 4.5f];
        aDateTimeArray = [DateTime.Today, DateTime.Today + TimeSpan.FromDays(30)];

        // create array of int that contains 100 multiples of 3
        int[] multiplesOfThree = new int[100];
        for (int i = 0; i < multiplesOfThree.Length; i++)
        {
            multiplesOfThree[i] = (i+1) * 3;
            Console.WriteLine($"Element {i}: {multiplesOfThree[i]}");
        }

        // Common Array Operations

        // Find things
        // BinarySearch REQUIRES sorted array. O(logN)
        Assert.AreEqual(17, Array.BinarySearch(multiplesOfThree, 54));
        // IndexOf does linear search (loops through each element until found). O(n)
        Assert.AreEqual(17, Array.IndexOf(multiplesOfThree,54));


        
        // Copy between array
        int[] destinationArray = new int[10];
        Array.Copy(multiplesOfThree, destinationArray, 10);

        int[] secondPointer = destinationArray;
        Assert.AreSame(destinationArray, secondPointer);


        Array.Resize(ref destinationArray, 20);

        Assert.AreNotSame(destinationArray, secondPointer);
        Assert.AreEqual(20, destinationArray.Length);
        Assert.AreEqual(10, secondPointer.Length);

        Array.Reverse(multiplesOfThree);
        Assert.AreEqual(300, multiplesOfThree[0]);

        int a = 1;
        int b = a;
        // Value type creates a copy
        a += 1;
        Assert.AreEqual(2, a);
        Assert.AreEqual(1, b);

        // Now int int is stored as an element of the array, can use reference to access value type
        int[] c = [7];
        int[] d = c;
        c[0] += 1;
        Assert.AreEqual(8, c[0]);
        Assert.AreEqual(8, d[0]);

        int e = 5;
        object o = e; // boxing - copy to heap, box is the opcode in compile c# code
        int e2 = (int)o; // unboxing
    }

    //expense - date, amount, merchant, category, isReimbursable
    //expense report - employee, date, expense[], isApproved


    [TestMethod]
    public void MagicSquareTest()
    {
        int[] newList = [4, 9, 2, 3, 5, 7, 8, 1, 6];
        int length = (int)Math.Sqrt(newList.Count());
        int[] diag = new int[2];
        
        //int[] diag1 = new int[length];
        //int[] diag2 = new int[length];
        //int[] expectedDiag1 = [4, 5, 6];
        //int[] expectedDiag2 = [2, 5, 8];

        int diagIncrement1 = 0;
        int diagIncrement2 = length - 1;

        for (int i = 0; i < length; i++)
        {
            diag[0] += newList[diagIncrement1];
            diag[1] += newList[diagIncrement2];

            diagIncrement1 += length + 1;
            diagIncrement2 += length - 1;
        }


        //for (int i = 0; i < diag1.Length; i++)
        //{
        //    diag1[1] += newArray[diagIncrement1];
        //    diag2[2] += newArray[diagIncrement2];

        //    diagIncrement1 += length + 1;
        //    diagIncrement2 += length - 1;
        //}
        Assert.AreEqual(diag[0], 15);
        Assert.AreEqual(diag[1], 15);

        Assert.AreEqual(newList.Count(), 9);
        //CollectionAssert.AreEqual(expectedDiag1, diag1);
        //CollectionAssert.AreEqual(expectedDiag2, diag2);

    }
}