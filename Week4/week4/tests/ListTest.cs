using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace tests;

[TestClass]
public class ListTest
{
    [TestMethod]
    public void DefaultContructorTest()
    {
        List<int> numbers = new List<int>();

        Assert.AreEqual(0, numbers.Count);
        Assert.AreEqual(default, numbers.Capacity);
        Assert.AreEqual(0, numbers.Capacity);
    }

    [TestMethod]
    public void IEnumerableContructorTest()
    {
        int[] numArray = [1, 2, 3, 4, 5];
        Assert.AreEqual(5, numArray.Count());

        List<int> numList = new(numArray);
        Assert.AreEqual(5, numList.Count);
    }

    [TestMethod]
    public void CapacityContructorTest()
    {
        List<int> numbers = new List<int>();

        Assert.AreEqual(0, numbers.Capacity);

        numbers.AddRange([1, 2, 3, 4, 5]);
        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);

        numbers.Add(10);
        Assert.AreEqual(6, numbers.Count);
        Assert.AreEqual(10, numbers.Capacity);

        numbers.Remove(10);
        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(10, numbers.Capacity);
    }

    [TestMethod]
    public void CapacityPropertyGetSetTest()
    {
        List<int> numbers = new List<int>(5);
        Assert.AreEqual(5, numbers.Capacity);

        numbers.Capacity = 7;
        Assert.AreEqual(7, numbers.Capacity);

        numbers.AddRange([1, 2, 3, 4, 5, 6, 7, 8]);
        Assert.AreEqual(14, numbers.Capacity);

        numbers.Capacity = 8;
        Assert.AreEqual(8, numbers.Capacity);
    }

    [TestMethod]
    public void CountPropertyGetTest()
    {
        List<int> numbers = new List<int>();

        Assert.AreEqual(0, numbers.Count);

        numbers.AddRange([1, 2, 3, 4, 5]);
        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);

        numbers.Add(10);
        Assert.AreEqual(6, numbers.Count);
        Assert.AreEqual(10, numbers.Capacity);

        numbers.Remove(10);
        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(10, numbers.Capacity);
    }

    [TestMethod]
    public void IndexerPropertyTest()
    {
        List<int> numbers = [1, 2, 3, 4, 5];

        Assert.AreEqual(3, numbers[2]);

        numbers.Remove(2);
        Assert.AreEqual(3, numbers[1]);

        numbers.Add(2);
        Assert.AreEqual(2, numbers[4]);
    }

    [TestMethod]
    public void ClearTest()
    {
        List<int> numbers = [1, 2, 3, 4, 5];

        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);

        numbers.Clear();
        Assert.AreEqual(0, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);

        numbers = [1, 2, 3, 4, 5, 6];
        Assert.AreEqual(6, numbers.Count);
        Assert.AreEqual(6, numbers.Capacity);

        numbers = new List<int>();
        Assert.AreEqual(0, numbers.Count);
        Assert.AreEqual(0, numbers.Capacity);
    }

    [TestMethod]
    public void AddTest()
    {
        List<string> tester = new(["Apple", "Banana"]);
        tester.Add("Cherry");
        Assert.AreEqual(3, tester.Count);
    }

    [TestMethod]
    public void AddRangeTest()
    {
        IEnumerable<string> rangeSource = new Stack<string>(["Item3", "Item4", "Item5"]);
        List<string> tester = new(["Apple", "Banana"]);
        tester.AddRange(rangeSource);
        Assert.AreEqual(5, tester.Count);
        Assert.AreEqual("Apple", tester[0]);
        Assert.AreEqual("Item5", tester[2]);
        Assert.AreEqual("Item4", tester[3]);
        Assert.AreEqual("Item3", tester[4]);



        IEnumerable<string> rangeSource2 = new Queue<string>(["Item6", "Item7", "Item8"]);
        tester.AddRange(rangeSource2);
        Assert.AreEqual("Apple", tester[0]);
        Assert.AreEqual("Item6", tester[5]);
        Assert.AreEqual("Item7", tester[6]);
        Assert.AreEqual("Item8", tester[7]);

        IEnumerable<string> rangeSource3 = new List<string>(["Item9", "Item10", "Item11"]);
        tester.AddRange(rangeSource3);
        Assert.AreEqual("Apple", tester[0]);
        Assert.AreEqual("Item9", tester[8]);
        Assert.AreEqual("Item10", tester[9]);
        Assert.AreEqual("Item11", tester[10]);
    }

    [TestMethod]
    public void TrueForAllTests()
    {
        List<int> testList = new List<int>([2, 4, 6, 8, 10]);
        Assert.IsTrue(testList.TrueForAll(n => n % 2 == 0));

        List<int> testList2 = new List<int>([1, 4, 6, 8, 10]);
        Assert.IsFalse(testList2.TrueForAll(n => n % 2 == 0));

        List<int> testList3 = new List<int>();
        Assert.IsTrue(testList3.TrueForAll(n => false));

    }

    [TestMethod]
    public void TrueForAllWithStringTests()
    {
        List<string> testList = new List<string>(["alpha", "bravo", "charlie", "delta", "echo", "foxtrot",]);
        Assert.IsTrue(testList.TrueForAll(w => w.Length > 3));
        Assert.IsTrue(testList.TrueForAll(w => w.Any(c => "aeiouAEIOU".Contains(c))));

    }

    [TestMethod]
    public void EnsureCapacityTest()
    {
        List<int> testList = new List<int>([2, 4, 6, 8, 10]);
        Assert.AreEqual(5, testList.Capacity);
        testList.EnsureCapacity(500);
        Assert.IsTrue(testList.Capacity >= 500);

        int capacity = testList.EnsureCapacity(200);
        Assert.IsTrue(capacity == 500);

        testList.TrimExcess();
        Assert.AreEqual(5, testList.Capacity);
    }

    [TestMethod]
    public void InsertRangeTest()
    {
        List<int> testList = new List<int>([2, 4, 6, 8, 10]);
        List<int> testList2 = new List<int>([1, 4, 6, 8, 10]);
        Assert.AreEqual(5, testList.Count);

        testList.InsertRange(2, testList2);

        Assert.AreEqual(10, testList.Count);
        Assert.AreEqual(1, testList[2]);
        Assert.AreEqual(6, testList[7]);

    }

    [TestMethod]
    public void InsertTest()
    {
        List<string> names = new List<string>(["Jack", "Mitchell", "Bryce"]);

        names.Insert(1, "John");

        Assert.AreEqual(4, names.Count);
        Assert.AreEqual("John", names[1]);

    }

    [TestMethod]
    public void ReverseTest()

    {
        List<string> normalList = new List<string> { "Miata", "Charger", "Mustang" };

        normalList.Reverse();

        Assert.AreEqual("Mustang", normalList[0]);
    }

    [TestMethod]
    public void SliceTest()
    {
        // shallow means the two lists point to the same reference 
        // ref doesn't work on value type variables (numbers)
        List<int> numbers = [1, 2, 3, 4, 5, 6];
        var slicedList = numbers.Slice(2, 2);

        Assert.AreEqual(3, slicedList[0]);
        Assert.AreEqual(2, slicedList.Count);

        slicedList[0] = 30;
        Assert.IsTrue(slicedList[0] == 30);

        Assert.IsTrue(numbers[2] == 3);
    }

    [TestMethod]
    public void AsReadOnlyTest()
    {
        List<string> tester = new(["Papaya", "Avocado"]);
        IList<string> roTester = tester.AsReadOnly();

        tester.Add("Apple");

        Assert.AreEqual("Apple", roTester[2]);
        Assert.ThrowsException<NotSupportedException>(() => roTester[1] = "Cherry");

    }

    [TestMethod]
    public void FindLastTest()
    {
        List<char> testList = ['a', 'b', 'g', 'h', 'a'];
        char? result = testList.FindLast(c => c <= 'b');

        Assert.IsTrue(result.HasValue);
        Assert.IsTrue(result == 'a');

        char? result2 = testList.FindLast(c => c >= 'z');
        Assert.IsTrue(result2 == '\0');
    }

    [TestMethod]
    public void BinarySearchTest()
    {
        // list must be sorted for binary search to work
        List<int> testList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int index = testList.BinarySearch(7);

        Assert.AreEqual(index, 6);
        Assert.IsFalse(index == 35);

        int index2 = testList.BinarySearch(11);

        Assert.AreEqual(-11, index2);

        List<int> testList2 = [5, 8, 1, 4, 100, 60, 56, 198];
        int index3 = testList2.BinarySearch(100);

        Assert.AreEqual(-8, index3);
    }

    [TestMethod]
    public void RemoveRangeTest()
    {
        List<string> animals = ["Polar Bear", "Moose", "Raven", "Wolf", "Loon", "Walleye", "Mountain Lion"];
        Assert.AreEqual(animals[2], "Raven");
        Assert.AreEqual(animals[6], "Mountain Lion");
        Assert.AreEqual(animals.Count, 7);

        animals.RemoveRange(2, 3);
        Assert.AreEqual(animals[2], "Walleye");
        Assert.AreEqual(animals[3], "Mountain Lion");
        Assert.AreEqual(animals.Count, 4);

    }

    [TestMethod]
    public void SortTest()
    {
        List<int> testList = [1, 10, 6, 4, 8, 2, 3, 5, 7, 9];
        testList.Sort(new DescendingComparer());
        testList.ForEach(i => Debug.WriteLine(i));
    }

    class DescendingComparer: IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return -(x.CompareTo(y));
        }
    }
}
