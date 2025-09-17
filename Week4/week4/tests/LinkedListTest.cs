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

    [TestMethod]
    public void ReverseLinkedListChallenge()
    {
        //cannot use stack
        //cannot duplicate LinkedListNode
        //cannot create a wrapper

        LinkedList<string> starterList = new LinkedList<string>(["apple", "banana", "cherry", "pear"]);
/*        LinkedListNode<string> firstNode = starterList.First;
        //LinkedListNode<string> lastNode = starterList.Last;

        //starterList.Remove(lastNode);
        //starterList.AddFirst(lastNode);
        do
        {
            LinkedListNode<string> nextNode = firstNode.Next;
            starterList.AddBefore(starterList.First, nextNode);

        } while (starterList.Last != firstNode);
*/
        LinkedList<string> reversedStarterList = new LinkedList<string>();

        for (int i = 0; i < starterList.Count; i++)
        {
            reversedStarterList.AddLast(starterList.Last.Value);

            starterList.AddFirst(starterList.Last);
        }
/*
        foreach (var node in starterList)
        {
            reversedStarterList.AddFirst(node);
        }
*/
        // starterList.First == pear
        // starterLast.Last == apple
        PrintList<string>(reversedStarterList);
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
