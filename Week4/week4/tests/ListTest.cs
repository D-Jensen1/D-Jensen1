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
    public void ClearMethodTest()
    {
        List<int> numbers = [1, 2, 3, 4, 5];

        Assert.AreEqual(5, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);

        numbers.Clear();
        Assert.AreEqual(0, numbers.Count);
        Assert.AreEqual(5, numbers.Capacity);
    }
}
