namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _222
{
    // Definition for a binary tree node
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    // Helper method to create a complete binary tree for testing
    private TreeNode CreateCompleteTree(int[] values)
    {
        if (values.Length == 0) return null;
        return CreateCompleteTreeHelper(values, 0);
    }

    private TreeNode CreateCompleteTreeHelper(int[] values, int index)
    {
        if (index >= values.Length)
            return null;

        TreeNode root = new TreeNode(values[index]);
        root.left = CreateCompleteTreeHelper(values, 2 * index + 1);
        root.right = CreateCompleteTreeHelper(values, 2 * index + 2);

        return root;
    }

    public int CountNodes(TreeNode root)
    {
        
    }



    [TestMethod]
    public void TestMethod1_PerfectBinaryTree()
    {
        // Test case: Perfect binary tree with 7 nodes
        //       1
        //      / \
        //     2   3
        //    / \ / \
        //   4 5 6  7
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5, 6, 7 });
        int expected = 7;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_CompleteButNotPerfect()
    {
        // Test case: Complete tree with 6 nodes
        //       1
        //      / \
        //     2   3
        //    / \ /
        //   4 5 6
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5, 6 });
        int expected = 6;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_EmptyTree()
    {
        // Test case: Empty tree
        TreeNode root = null;
        int expected = 0;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleNode()
    {
        // Test case: Single node
        TreeNode root = new TreeNode(1);
        int expected = 1;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_TwoNodes()
    {
        // Test case: Two nodes
        //   1
        //  /
        // 2
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        int expected = 2;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_ThreeNodes()
    {
        // Test case: Three nodes (perfect)
        //   1
        //  / \
        // 2   3
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3 });
        int expected = 3;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_FourNodes()
    {
        // Test case: Four nodes
        //     1
        //    / \
        //   2   3
        //  /
        // 4
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4 });
        int expected = 4;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_FiveNodes()
    {
        // Test case: Five nodes
        //     1
        //    / \
        //   2   3
        //  / \
        // 4   5
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5 });
        int expected = 5;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LargePerfectTree()
    {
        // Test case: Perfect binary tree with 15 nodes (height 4)
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        int expected = 15;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_LargeCompleteTree()
    {
        // Test case: Complete tree with 10 nodes
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        int expected = 10;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_PowerOfTwoNodes()
    {
        // Test case: Exactly 8 nodes (2^3)
        TreeNode root = CreateCompleteTree(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        int expected = 8;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_ManualCompleteTree()
    {
        // Test case: Manually created complete tree
        //       1
        //      / \
        //     2   3
        //    / \ / \
        //   4 5 6  7
        //  / \
        // 8   9
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        root.right.left = new TreeNode(6);
        root.right.right = new TreeNode(7);
        root.left.left.left = new TreeNode(8);
        root.left.left.right = new TreeNode(9);
        
        int expected = 9;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_LargeCompleteTreeManual()
    {
        // Test case: Larger manually created complete tree
        //         1
        //       /   \
        //      2     3
        //     / \   / \
        //    4   5 6   7
        //   / \ / \
        //  8 9 10 11
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        root.right.left = new TreeNode(6);
        root.right.right = new TreeNode(7);
        root.left.left.left = new TreeNode(8);
        root.left.left.right = new TreeNode(9);
        root.left.right.left = new TreeNode(10);
        root.left.right.right = new TreeNode(11);
        
        int expected = 11;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_VeryLargeCompleteTree()
    {
        // Test case: Very large complete tree (31 nodes - perfect tree of height 5)
        int[] values = new int[31];
        for (int i = 0; i < 31; i++)
        {
            values[i] = i + 1;
        }
        TreeNode root = CreateCompleteTree(values);
        int expected = 31;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_NegativeValues()
    {
        // Test case: Complete tree with negative values (values don't matter for counting)
        //    -1
        //   /  \
        //  -2  -3
        //  /
        // -4
        TreeNode root = new TreeNode(-1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(-3);
        root.left.left = new TreeNode(-4);
        
        int expected = 4;
        int actual = CountNodes(root);
        Assert.AreEqual(expected, actual);
    }
}
