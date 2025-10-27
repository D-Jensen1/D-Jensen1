namespace LeetLinkedListTest;

[TestClass]
public class _141
{
    // Definition for singly-linked list node
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
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

    // Helper method to create a cycle by connecting tail to a specific position
    private void CreateCycle(ListNode head, int pos)
    {
        if (head == null || pos < 0) return;

        ListNode tail = head;
        ListNode cycleStart = null;
        int index = 0;

        // Find the tail and the node at position 'pos'
        while (tail.next != null)
        {
            if (index == pos)
                cycleStart = tail;
            tail = tail.next;
            index++;
        }

        // If pos is valid, create the cycle
        if (pos < index && cycleStart != null)
        {
            tail.next = cycleStart;
        }
    }

    public bool HasCycle(ListNode head)
    {
        // Runtime: 10.39% at 114 ms

        if (head == null) return false;
        var slow = head;
        var fast = head;

        do
        {
            slow = slow.next;
            fast = fast.next;
            if (fast == null || fast.next == null)
            {
                return false;
            }
            fast = fast.next;

            if (slow == fast)
            {
                return true;
            }
        } while (fast != null || slow != null);
        return false;
    }

    public bool HasCycle2(ListNode head)
    {
        // Runtime: 45.74% at 105 ms

        if (head == null) return false;
        var slow = head;
        var fast = head.next;

        while(slow != fast)
        {
            if (fast == null || fast.next == null)
            {
                return false;
            }

            slow = slow.next;
            fast = fast.next.next;
        }
        return true;
    }

    [TestMethod]
    public void TestMethod1_NoCycle()
    {
        // Test case: [3,2,0,-4] with no cycle -> false
        ListNode head = CreateLinkedList(new int[] { 3, 2, 0, -4 });
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_CycleAtPosition1()
    {
        // Test case: [3,2,0,-4] with cycle at position 1 -> true
        ListNode head = CreateLinkedList(new int[] { 3, 2, 0, -4 });
        CreateCycle(head, 1); // tail connects to index 1 (value 2)
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_CycleAtPosition0()
    {
        // Test case: [1,2] with cycle at position 0 -> true
        ListNode head = CreateLinkedList(new int[] { 1, 2 });
        CreateCycle(head, 0); // tail connects to head
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleNodeNoCycle()
    {
        // Test case: [1] with no cycle -> false
        ListNode head = CreateLinkedList(new int[] { 1 });
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleNodeWithCycle()
    {
        // Test case: [1] with cycle pointing to itself -> true
        ListNode head = new ListNode(1);
        head.next = head; // points to itself
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_EmptyList()
    {
        // Test case: null head -> false
        ListNode head = null;
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_TwoNodesNoCycle()
    {
        // Test case: [1,2] with no cycle -> false
        ListNode head = CreateLinkedList(new int[] { 1, 2 });
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_LongListNoCycle()
    {
        // Test case: [1,2,3,4,5,6,7,8,9,10] with no cycle -> false
        ListNode head = CreateLinkedList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LongListWithCycle()
    {
        // Test case: [1,2,3,4,5,6,7,8,9,10] with cycle at position 5 -> true
        ListNode head = CreateLinkedList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        CreateCycle(head, 5); // tail connects to index 5 (value 6)
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_ThreeNodesWithCycle()
    {
        // Test case: [1,2,3] with cycle at position 0 -> true
        ListNode head = CreateLinkedList(new int[] { 1, 2, 3 });
        CreateCycle(head, 0); // tail connects to head
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ThreeNodesNoCycle()
    {
        // Test case: [1,2,3] with no cycle -> false
        ListNode head = CreateLinkedList(new int[] { 1, 2, 3 });
        bool expected = false;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_CycleInMiddle()
    {
        // Test case: [1,2,3,4,5,6,7] with cycle at position 3 -> true
        ListNode head = CreateLinkedList(new int[] { 1, 2, 3, 4, 5, 6, 7 });
        CreateCycle(head, 3); // tail connects to index 3 (value 4)
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LargeCycle()
    {
        // Test case: Large list with cycle at beginning
        int[] values = new int[1000];
        for (int i = 0; i < 1000; i++)
        {
            values[i] = i;
        }
        ListNode head = CreateLinkedList(values);
        CreateCycle(head, 0); // tail connects to head
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_NegativeValues()
    {
        // Test case: [-1,-2,-3,-4] with cycle at position 1 -> true
        ListNode head = CreateLinkedList(new int[] { -1, -2, -3, -4 });
        CreateCycle(head, 1); // tail connects to index 1 (value -2)
        bool expected = true;
        bool actual = HasCycle(head);
        Assert.AreEqual(expected, actual);
    }
}
