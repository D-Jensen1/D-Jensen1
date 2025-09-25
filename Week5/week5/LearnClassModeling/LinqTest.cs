using System.Diagnostics;
using System.IO;
using System.Linq;
namespace LearnClassModeling;

[TestClass]
public class LinqTest
{
    [TestMethod]
    public void WhereTest()
    {
        IEnumerable<int> numbers = Enumerable.Range(100, 1000);
        //IEnumerable<int> result = numbers.Where(i => i % 7 == 0);
        IEnumerable<int> result = numbers.VcWhere(i => i % 7 == 0);

        foreach (var item in result)
        {
            Debug.WriteLine(item);
        }
    }

    [TestMethod]
    public void WhereChallenge()
    {
        IEnumerable<string> fruits = ["Apple", "Banana", "Orange", "Grapes", "Strawberry", "Blueberry", "Mango", "Pineapple", "Watermelon", "Peach"];
        
        IEnumerable<string> result = fruits.Where(f => f.Length >= 8);
        Assert.AreEqual(4, result.Count());

        IEnumerable<string> result2 = fruits.Where(f => f.ToLower().StartsWith('a')|| f.ToLower().StartsWith('b'));
        Assert.AreEqual(3, result2.Count());

        foreach (var item in result2)
        {
            Debug.WriteLine(item);
        }
    }

    [TestMethod]
    public void DistinctByTest()
    {
        IEnumerable<int> test = [1, 2, 3, 3, 4, 4, 4, 4, 5, 5, 4];
        Assert.AreEqual(5, test.Distinct().Count());

        var result = test.DistinctBy(x => x / 2);

        Assert.AreEqual(3, result.Count());
        foreach (var item in result)
        {
            Debug.WriteLine(item);
        }

        IEnumerable<string> fruits = ["Apple", "Banana", "Banana", "Apple", "Strawberry", "Blueberry", "Apple", "Pineapple", "Strawberry", "Peach", "Peach"];
        Assert.AreEqual(6, fruits.Distinct().Count());

        var result2 = fruits.DistinctBy(f => f[0]);
        Assert.AreEqual(4, result2.Count());
        foreach (var item in result2)
        {
            Debug.WriteLine(item);
        }

    }

    [TestMethod]
    public void CountByTest()
    {
        IEnumerable<string> fruits = ["Apple", "Banana", "Banana", "Apple", "Strawberry", "Blueberry", "Apple", "Pineapple", "Strawberry", "Peach", "Peach"];
        var result = fruits.CountBy(f => f.StartsWith('B')).ToList();

        Assert.AreEqual(true, result[1].Key);
        Assert.AreEqual(3, result[1].Value);
        foreach (var item in result)
        {
            Debug.WriteLine(item);
        }
    }

    [TestMethod]
    public void OrderByTest()
    {

    }

    [TestMethod]
    public void TakeTest()
    {
        IEnumerable<int> test = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        Assert.AreEqual(9, test.Count());

        var result = test.Take(4);

        Assert.AreEqual(4, result.Count());
        foreach (var item in result)
        {
            Debug.WriteLine(item);
        }

    }

    [TestMethod]
    public void ZipTest()
    {

    }

    [TestMethod]
    public void ExceptByTest()
    {
        IEnumerable<string> fruits = ["Apple", "Banana", "Orange", "Grapes", "Guava"];
        Assert.AreEqual(5, fruits.Count());

        IEnumerable<char> excludeInitials = ['A', 'G'];

        var result = fruits.ExceptBy(excludeInitials, f => f[0]).ToArray();

        Assert.AreEqual(2, result.Count());
        foreach (var item in result)
        {
            Debug.WriteLine(item);
        }


        IEnumerable<char> excludeLetters = ['a', 'n'];

        var result2 = fruits.ExceptBy(excludeLetters, f => f[2]).ToArray();

        Assert.AreEqual(1, result2.Count());
        foreach (var item in result2)
        {
            Debug.WriteLine(item);
        }
    }
}

public static class MyLinq
{
    public static IEnumerable<T> VcWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        foreach (var item in items)
        {
            if (predicate.Invoke(item))
                yield return item;
            else
                continue;
        }
    }
}

