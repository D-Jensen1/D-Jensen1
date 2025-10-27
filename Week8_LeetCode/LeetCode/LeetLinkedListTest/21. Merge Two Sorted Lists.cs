namespace LeetLinkedListTest;

[TestClass]
public class _21
{
    // Definition for singly-linked list node
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    // Helper method to create a linked list from an array
    private ListNode CreateLinkedList(int[] values)
    {
        if (values.Length == 0) return null;

        ListNode head = new ListNode(values[0]);
        ListNode current = head;

        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
            current = current.next;
        }

        return head;
    }

    // Helper method to convert linked list to array for easy comparison
    private int[] LinkedListToArray(ListNode head)
    {
        List<int> result = new List<int>();
        ListNode current = head;

        while (current != null)
        {
            result.Add(current.val);
            current = current.next;
        }

        return result.ToArray();
    }

    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Create a dummy node to simplify edge cases
        ListNode dummy = new ListNode(0);
        ListNode current = dummy;

        // Compare and merge nodes while both lists have remaining nodes
        while (list1 != null && list2 != null)
        {
            if (list1.val <= list2.val)
            {
                current.next = list1;
                list1 = list1.next;
            }
            else
            {
                current.next = list2;
                list2 = list2.next;
            }
            current = current.next;
        }

        // Attach remaining nodes from the non-empty list
        if (list1 != null)
        {
            current.next = list1;
        }
        else
        {
            current.next = list2;
        }

        // Return the merged list (skip dummy node)
        return dummy.next;
    }


    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: list1 = [1,2,4], list2 = [1,3,4] -> [1,1,2,3,4,4]
        ListNode list1 = CreateLinkedList(new int[] { 1, 2, 4 });
        ListNode list2 = CreateLinkedList(new int[] { 1, 3, 4 });
        int[] expected = { 1, 1, 2, 3, 4, 4 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_EmptyLists()
    {
        // Test case: list1 = [], list2 = [] -> []
        ListNode list1 = null;
        ListNode list2 = null;
        int[] expected = { };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_OneEmptyList()
    {
        // Test case: list1 = [], list2 = [0] -> [0]
        ListNode list1 = null;
        ListNode list2 = CreateLinkedList(new int[] { 0 });
        int[] expected = { 0 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SecondListEmpty()
    {
        // Test case: list1 = [1,2,3], list2 = [] -> [1,2,3]
        ListNode list1 = CreateLinkedList(new int[] { 1, 2, 3 });
        ListNode list2 = null;
        int[] expected = { 1, 2, 3 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleNodes()
    {
        // Test case: list1 = [1], list2 = [2] -> [1,2]
        ListNode list1 = CreateLinkedList(new int[] { 1 });
        ListNode list2 = CreateLinkedList(new int[] { 2 });
        int[] expected = { 1, 2 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SingleNodesReversed()
    {
        // Test case: list1 = [2], list2 = [1] -> [1,2]
        ListNode list1 = CreateLinkedList(new int[] { 2 });
        ListNode list2 = CreateLinkedList(new int[] { 1 });
        int[] expected = { 1, 2 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_AllElementsFromFirstList()
    {
        // Test case: list1 = [1,2,3], list2 = [4,5,6] -> [1,2,3,4,5,6]
        ListNode list1 = CreateLinkedList(new int[] { 1, 2, 3 });
        ListNode list2 = CreateLinkedList(new int[] { 4, 5, 6 });
        int[] expected = { 1, 2, 3, 4, 5, 6 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_AllElementsFromSecondList()
    {
        // Test case: list1 = [4,5,6], list2 = [1,2,3] -> [1,2,3,4,5,6]
        ListNode list1 = CreateLinkedList(new int[] { 4, 5, 6 });
        ListNode list2 = CreateLinkedList(new int[] { 1, 2, 3 });
        int[] expected = { 1, 2, 3, 4, 5, 6 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_InterleavedElements()
    {
        // Test case: list1 = [1,3,5], list2 = [2,4,6] -> [1,2,3,4,5,6]
        ListNode list1 = CreateLinkedList(new int[] { 1, 3, 5 });
        ListNode list2 = CreateLinkedList(new int[] { 2, 4, 6 });
        int[] expected = { 1, 2, 3, 4, 5, 6 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_DuplicateValues()
    {
        // Test case: list1 = [1,1,2], list2 = [1,3,4] -> [1,1,1,2,3,4]
        ListNode list1 = CreateLinkedList(new int[] { 1, 1, 2 });
        ListNode list2 = CreateLinkedList(new int[] { 1, 3, 4 });
        int[] expected = { 1, 1, 1, 2, 3, 4 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_NegativeNumbers()
    {
        // Test case: list1 = [-3,-1,1], list2 = [-2,0,2] -> [-3,-2,-1,0,1,2]
        ListNode list1 = CreateLinkedList(new int[] { -3, -1, 1 });
        ListNode list2 = CreateLinkedList(new int[] { -2, 0, 2 });
        int[] expected = { -3, -2, -1, 0, 1, 2 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_DifferentLengths()
    {
        // Test case: list1 = [1,2,3,4,5], list2 = [6] -> [1,2,3,4,5,6]
        ListNode list1 = CreateLinkedList(new int[] { 1, 2, 3, 4, 5 });
        ListNode list2 = CreateLinkedList(new int[] { 6 });
        int[] expected = { 1, 2, 3, 4, 5, 6 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_AllSameValues()
    {
        // Test case: list1 = [1,1,1], list2 = [1,1,1] -> [1,1,1,1,1,1]
        ListNode list1 = CreateLinkedList(new int[] { 1, 1, 1 });
        ListNode list2 = CreateLinkedList(new int[] { 1, 1, 1 });
        int[] expected = { 1, 1, 1, 1, 1, 1 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LargeNumbers()
    {
        // Test case: list1 = [100,200], list2 = [150,250] -> [100,150,200,250]
        ListNode list1 = CreateLinkedList(new int[] { 100, 200 });
        ListNode list2 = CreateLinkedList(new int[] { 150, 250 });
        int[] expected = { 100, 150, 200, 250 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ZeroValues()
    {
        // Test case: list1 = [0,0,0], list2 = [0,0] -> [0,0,0,0,0]
        ListNode list1 = CreateLinkedList(new int[] { 0, 0, 0 });
        ListNode list2 = CreateLinkedList(new int[] { 0, 0 });
        int[] expected = { 0, 0, 0, 0, 0 };
        
        ListNode result = MergeTwoLists(list1, list2);
        int[] actual = LinkedListToArray(result);
        
        CollectionAssert.AreEqual(expected, actual);
    }
}
