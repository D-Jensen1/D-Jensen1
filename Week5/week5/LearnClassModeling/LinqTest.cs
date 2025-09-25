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

    }

    [TestMethod]
    public void SelectTest()
    {
        ICollection<string> fruits = ["Apple", "Banana", "Orange", "Grapes", "Strawberry", "Blueberry", "Mango", "Pineapple", "Watermelon", "Peach"];
        // (Apple, 5, A, e)
        // (Banana, 6, B, a)

        var myProjection = fruits.Select(s =>
            new // new anonymous class
            {
                FruitName = s,
                NameLength = s.Length,
                FirstLetter = s[0],
                MiddleLetter = s[s.Length/2],
                LastLetter = s[^1]
            }
        );

        foreach (var item in myProjection)
        {
            Debug.WriteLine($"{item.FruitName} has {item.NameLength} letters, first: {item.FirstLetter}, middle:{item.MiddleLetter}, last:{item.LastLetter}");
        }
    }


    [TestMethod]
    public void OrderByTest()
    {
        ICollection<string> fruits = ["Apple", "Banana", "Orange", "Grapes", "Strawberry", "Blueberry", "Mango", "Pineapple", "Watermelon", "Peach"];
        ICollection<string> expected = ["Apple",
                                        "Mango",
                                        "Peach",
                                        "Banana",
                                        "Orange",
                                        "Grapes",
                                        "Blueberry",
                                        "Pineapple",
                                        "Strawberry",
                                        "Watermelon"];
        CollectionAssert.AreEqual(expected.ToList(), fruits.OrderBy(s => s.Length).ToList());
        Assert.AreEqual(10, fruits.OrderBy(s => s.Length).Debug().Count());
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

        IEnumerable<int> nums = Enumerable.Range(1,1000);
        IEnumerable<string> names = nums.Select(n => n.ToString());

        var result = nums.Zip(names);
        // Zip combines nums and names into KeyValuePair with nums as keys and names as values
        var finalResult = result.Take(5).Select(t => $"number {t.First} is zipped to lookup to {t.Second}");
        finalResult.DebugPrint();
        //var listResult = result.Debug().Skip(30).Take(5).ToList();
        
        //Assert.AreEqual((1, "one"), result.First());
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

    [TestMethod]
    public void DateGenerationTest()
    {
        IEnumerable<int> nums = Enumerable.Range(1, 1000);
        IEnumerable<DateTime> dates = nums.Select(n => DateTime.Now.AddDays(n));

        var result = nums.Zip(dates);
        var finalResult = result.Take(10).Select(t => $"Number {t.First} is zipped to the date {t.Second}");
        finalResult.DebugPrint();


    }

    [TestMethod]
    public void GroupingTestWithInt()
    {
        IEnumerable<int> nums = Enumerable.Range(1, 1000);

        // groups numbers into 100 number intervals (0-99 = 0, 100-199 = 1)
        var groupResult = nums.GroupBy(n => n / 100);
        foreach (IGrouping<int,int> aGroup in groupResult)
        {
            Debug.Print($"The key is {aGroup.Key}, Avg: {aGroup.Average()}, Min: {aGroup.Min()}");
            
            //foreach (var memeber in aGroup)
            //{
            //    Debug.Print($"\t{memeber}");
            //}
        }
    }

    [TestMethod]
    public void GroupingTestWithString()
    {
        ICollection<string> fruits = ["Apple", "Banana", "Orange", "Grapes", "Strawberry", "Blueberry", "Mango", "Pineapple", "Watermelon", "Peach"];

        // grouping our elements by length, print members of each group in order of first letter
        var groupedFruit = fruits.OrderBy(f => f[0]).GroupBy(f => f.Length);

        foreach (var fruitGroupedByLength in groupedFruit)
        {
            foreach (var item in fruitGroupedByLength)
            {
                Debug.WriteLine($"{fruitGroupedByLength.Key.ToString()} - {item}");
            }
        }

        Debug.WriteLine(new string('*', 40));

        foreach (var fruitGroupedByLength in fruits.GroupBy(f => f.Length).OrderBy(g => g.Key))
        {
            foreach (var item in fruitGroupedByLength.OrderBy(s => s[0]))
            {
                Debug.WriteLine($"{fruitGroupedByLength.Key.ToString()} - {item}");
            }
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

    public static IEnumerable<T> Debug<T>(this IEnumerable<T> items,string caption = "")
    {
        System.Diagnostics.Debug.WriteLine(new string('*', 30) + caption + new string('*', 30));
        foreach (var item in items)
        {
            System.Diagnostics.Debug.WriteLine(item);
            yield return item;
        }
        System.Diagnostics.Debug.WriteLine(new string('*', 80));
        yield break;
    }

    public static IEnumerable<T> DebugPrint<T>(this IEnumerable<T> items, string caption = "")
    {
        var listReturn = new List<T>();
        var iterator = items.GetEnumerator();
        while (iterator.MoveNext())
        {
            System.Diagnostics.Debug.WriteLine(iterator.Current);
            listReturn.Add(iterator.Current);
        }
        return listReturn;
    }
}

