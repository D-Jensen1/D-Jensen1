namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _104
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
    // Helper method to create a binary tree for testing
    private TreeNode CreateTree(int?[] values, int index = 0)
    {
        if (index >= values.Length || values[index] == null)
            return null;

        TreeNode root = new TreeNode(values[index].Value);
        root.left = CreateTree(values, 2 * index + 1);
        root.right = CreateTree(values, 2 * index + 2);

        return root;
    }

    #region SolutionOne
    int maxDepth = 0;
    public int MaxDepth(TreeNode root)
    {
        if (root == null) return 0;

        FindDepth(root, 1);

        return maxDepth;
    }
    public void FindDepth(TreeNode node, int currentLevel)
    {
        if (currentLevel > maxDepth) maxDepth = currentLevel;

        if (node.left == null && node.right == null) return;

        currentLevel++;
        if (node.left != null) FindDepth(node.left, currentLevel);
        if (node.right != null) FindDepth(node.right, currentLevel);
    }
    #endregion

    public int MaxDepth2(TreeNode root)
    {
        // Base case: if the node is null, depth is 0
        if (root == null)
            return 0;

        // Recursively find the depth of left and right subtrees
        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);

        // Return the maximum of left and right subtree depths, plus 1 for current node
        return Math.Max(leftDepth, rightDepth) + 1;
    }

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: Tree = [3,9,20,null,null,15,7] -> 3
        //     3
        //    / \
        //   9  20
        //     /  \
        //    15   7
        TreeNode root = CreateTree(new int?[] { 3, 9, 20, null, null, 15, 7 });
        int expected = 3;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SingleNode()
    {
        // Test case: Tree = [1] -> 1
        TreeNode root = new TreeNode(1);
        int expected = 1;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_EmptyTree()
    {
        // Test case: Tree = [] -> 0
        TreeNode root = null;
        int expected = 0;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_OnlyLeftChild()
    {
        // Test case: Tree = [1,2] -> 2
        //   1
        //  /
        // 2
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        int expected = 2;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_OnlyRightChild()
    {
        // Test case: Tree = [1,null,2] -> 2
        //   1
        //    \
        //     2
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        int expected = 2;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_BalancedTree()
    {
        // Test case: Perfect binary tree with depth 3
        //       1
        //      / \
        //     2   3
        //    / \ / \
        //   4 5 6  7
        TreeNode root = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7 });
        int expected = 3;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_LeftSkewedTree()
    {
        // Test case: Left-skewed tree
        //   1
        //  /
        // 2
        ///
        //3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.left = new TreeNode(3);
        int expected = 3;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_RightSkewedTree()
    {
        // Test case: Right-skewed tree
        // 1
        //  \
        //   2
        //    \
        //     3
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        root.right.right = new TreeNode(3);
        int expected = 3;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_UnbalancedTree()
    {
        // Test case: Unbalanced tree
        //     1
        //    / \
        //   2   3
        //  //
        // 4
        //  \
        //   5
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.left.right = new TreeNode(5);
        int expected = 4;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_DeepTree()
    {
        // Test case: Deep tree with depth 5
        TreeNode root = new TreeNode(1);
        TreeNode current = root;
        for (int i = 2; i <= 5; i++)
        {
            current.left = new TreeNode(i);
            current = current.left;
        }
        int expected = 5;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_ComplexTree()
    {
        // Test case: More complex tree structure
        //       1
        //      / \
        //     2   3
        //    /   / \
        //   4   5   6
        //  /       /
        // 7       8
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.right.left = new TreeNode(5);
        root.right.right = new TreeNode(6);
        root.left.left.left = new TreeNode(7);
        root.right.right.left = new TreeNode(8);
        int expected = 4;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_LargeBalancedTree()
    {
        // Test case: Larger balanced tree with depth 4
        TreeNode root = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        int expected = 4;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_NegativeValues()
    {
        // Test case: Tree with negative values
        //    -1
        //   /  \
        //  -2  -3
        TreeNode root = new TreeNode(-1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(-3);
        int expected = 2;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_SingleBranch()
    {
        // Test case: Only right branches
        //   1
        //    \
        //     2
        //      \
        //       3
        //        \
        //         4
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        root.right.right = new TreeNode(3);
        root.right.right.right = new TreeNode(4);
        int expected = 4;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ZigZagTree()
    {
        // Test case: Zigzag pattern
        //   1
        //  /
        // 2
        //  \
        //   3
        //  /
        // 4
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.right = new TreeNode(3);
        root.left.right.left = new TreeNode(4);
        int expected = 4;
        int actual = MaxDepth(root);
        Assert.AreEqual(expected, actual);
    }
}
