using System.Diagnostics;

namespace tests;

[TestClass]
public class DictionaryTest
{
    [TestMethod]
    public void DictionaryTests()
    {
        Dictionary<string, string> urbanDictionary = new Dictionary<string, string>();
        urbanDictionary.Add("Youthquake", "A peaceful, digital-driven revolution by the youth of a country in protest against dictators who suppress democracy.");
        urbanDictionary.Add("Nopology", "a non-regretful acknowledgement of an offense or failure");
        
        KeyValuePair<string, string> entry = new KeyValuePair<string, string>("pebbles", "small rocks");
        urbanDictionary.Add(entry.Key,entry.Value);

        foreach (KeyValuePair<string, string> item in urbanDictionary)
        {
            Debug.WriteLine($"{item.Key} - {item.Value}");
        }
    }
    [TestMethod]
    public void DictionaryDuplicationTests()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("Key1", "test");
        //dictionary.Add("Key1", "test");

        Assert.ThrowsException<ArgumentException>(() => dictionary.Add("Key1", "test"));
        Assert.IsFalse(dictionary.TryAdd("Key1", "test"));
        Assert.IsTrue(dictionary.ContainsKey("Key1"));

        dictionary.Add("bob", "123 main st.");
        if (dictionary.ContainsKey("bob"))
        {
            dictionary["bob"] = "234 main st.";
        }
        else
        {
            dictionary.Add("bob", "123 main st.");
        }

    }

    [TestMethod]
    public void DictionaryPropertyTests()
    {
        Dictionary<string, string> dictionary = new(
            [
                new KeyValuePair<string,string> ("Apple","Fruit"),
                new ("Bob","Name"),
                new ("Chair","Furniture"),
                new ("Keyboard","PC Parts"),
            ]
            );

        foreach (var item in dictionary.Keys)
        {
            Debug.WriteLine(item);
        }

        foreach (var item in dictionary.Values)
        {
            Debug.WriteLine(item);
        }
        
        Assert.AreEqual("Name", dictionary["Bob"]);
    }
}
