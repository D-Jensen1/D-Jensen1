using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace tests;

[TestClass]
public class LinkedListTest
{
    LinkedList<string> testList;
    [TestInitialize]
    public void Init()
    {
        testList = new();
        LinkedListNode<string> aNode = new("The");
        testList.AddFirst(aNode);
        testList.AddLast(new LinkedListNode<string>("Lazy"));
        testList.AddAfter(aNode, new LinkedListNode<string>("Not"));
        if(testList.Last is not null)
            testList.AddBefore(testList.Last, "Very");

    }

    [TestMethod]
    public void AddRemoveInsertIntoLinkedList()
    {
        // The Not Very Lazy
        
        var newNode = new LinkedListNode<string>("LastOne");
        testList.AddAfter(testList.Last, newNode);

        Assert.AreSame(newNode, testList.Last);
        Assert.AreSame(newNode.List, testList);
        PrintList(testList);

        testList.RemoveLast();
        testList.AddFirst(newNode);
        PrintList(testList);

        LinkedListNode<string>? lazyNode = testList.Find("Lazy");
        if (lazyNode is not null) Debug.WriteLine("Found Lazy node.");

        Debug.Print(lazyNode!.Previous!.Previous!.Value);
    }

    private void PrintList<T>(LinkedList<T> theList)
    {
        if (theList.First is null) return;

        LinkedListNode<T> aNode = theList.First;
        do
        {
            Debug.WriteLine(aNode.Value!.ToString());
            aNode = aNode.Next!;
        } while (aNode is not null);
    }
}
