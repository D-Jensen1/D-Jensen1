namespace tests;

[TestClass]
public class SortedDictionaryTest
{
    [TestMethod]
    public void SortedDictionaryAsClusteredIndexTable()
    {
        SortedDictionary<int, (string, int, int)> customer = new();
        //key int is how clustered index organizes table data
        customer.Add(1, ("bob", 35, 10000));
        customer.Add(20, ("alice", 33, 13000));
        customer.Add(14, ("joe", 55, 10000));
        customer.Add(7, ("jim", 45, 12000));

        SortedDictionary<string, int> uniqueIndexOnName = new();
        SortedDictionary<string, List<int>> nonUniqueIndexSalary = new();

        foreach (var item in customer)
        {
            Console.WriteLine($"{item.Key}-{item.Value.Item1}-{item.Value.Item2}-{item.Value.Item3}");
            uniqueIndexOnName.Add(item.Value.Item1, item.Key);
        }

        var bob = customer[uniqueIndexOnName["bob"]];
    }
}
//cluster index determines the order of row storage
//noncluster index looks up location of the data using clustered index value