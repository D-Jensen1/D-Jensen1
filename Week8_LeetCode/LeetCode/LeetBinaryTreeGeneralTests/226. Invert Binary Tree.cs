namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _226
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

    // Helper method to compare two trees for equality
    private bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;
        return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }

    #region Solution
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null) return null;
        if (root.left == null && root.right == null) return root;

        TreeNode left = root.left;
        TreeNode right = root.right;

        root.left = right;
        root.right = left;

        InvertTree(root.left);
        InvertTree(root.right);
        
        return root;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: Tree = [4,2,7,1,3,6,9] -> [4,7,2,9,6,3,1]
        //     4           4
        //    / \         / \
        //   2   7  =>   7   2
        //  / \ / \     / \ / \
        // 1  3 6  9   9  6 3  1
        TreeNode root = CreateTree(new int?[] { 4, 2, 7, 1, 3, 6, 9 });
        TreeNode expected = CreateTree(new int?[] { 4, 7, 2, 9, 6, 3, 1 });
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod2_TwoNodeTree()
    {
        // Test case: Tree = [2,1,3] -> [2,3,1]
        //   2     2
        //  / \ => / \
        // 1   3  3   1
        TreeNode root = CreateTree(new int?[] { 2, 1, 3 });
        TreeNode expected = CreateTree(new int?[] { 2, 3, 1 });
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod3_EmptyTree()
    {
        // Test case: Tree = [] -> []
        TreeNode root = null;
        TreeNode expected = null;
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod4_SingleNode()
    {
        // Test case: Tree = [1] -> [1]
        TreeNode root = new TreeNode(1);
        TreeNode expected = new TreeNode(1);
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod5_OnlyLeftChild()
    {
        // Test case: Tree = [1,2] -> [1,null,2]
        //   1     1
        //  /  =>   \
        // 2         2
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        
        TreeNode expected = new TreeNode(1);
        expected.right = new TreeNode(2);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod6_OnlyRightChild()
    {
        // Test case: Tree = [1,null,2] -> [1,2]
        //   1       1
        //    \  =>  /
        //     2    2
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(2);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod7_LeftSkewedTree()
    {
        // Test case: Left-skewed tree becomes right-skewed
        //   1       1
        //  /         \
        // 2     =>    2
        ///             \
        //3               3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.left = new TreeNode(3);
        
        TreeNode expected = new TreeNode(1);
        expected.right = new TreeNode(2);
        expected.right.right = new TreeNode(3);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod8_RightSkewedTree()
    {
        // Test case: Right-skewed tree becomes left-skewed
        // 1         1
        //  \       /
        //   2  => 2
        //    \   /
        //     3 3
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        root.right.right = new TreeNode(3);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(2);
        expected.left.left = new TreeNode(3);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod9_AsymmetricTree()
    {
        // Test case: Asymmetric tree
        //     1           1
        //    / \         / \
        //   2   3  =>   3   2
        //  /             \
        // 4               4
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(3);
        expected.right = new TreeNode(2);
        expected.right.right = new TreeNode(4);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod10_ComplexTree()
    {
        // Test case: More complex tree
        //       1               1
        //      / \             / \
        //     2   3    =>     3   2
        //    / \   \         /   / \
        //   4   5   6       6   5   4
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        root.right.right = new TreeNode(6);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(3);
        expected.right = new TreeNode(2);
        expected.left.left = new TreeNode(6);
        expected.right.left = new TreeNode(5);
        expected.right.right = new TreeNode(4);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod11_NegativeValues()
    {
        // Test case: Tree with negative values
        //    -1        -1
        //   /  \  =>  /  \
        //  -2  -3   -3  -2
        TreeNode root = new TreeNode(-1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(-3);
        
        TreeNode expected = new TreeNode(-1);
        expected.left = new TreeNode(-3);
        expected.right = new TreeNode(-2);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod12_PerfectBinaryTree()
    {
        // Test case: Perfect binary tree
        //       1               1
        //      / \             / \
        //     2   3    =>     3   2
        //    / \ / \         / \ / \
        //   4 5 6  7        7 6 5  4
        TreeNode root = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7 });
        TreeNode expected = CreateTree(new int?[] { 1, 3, 2, 7, 6, 5, 4 });
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod13_UnbalancedTree()
    {
        // Test case: Unbalanced tree
        //     1           1
        //    / \         / \
        //   2   3  =>   3   2
        //  /               /
        // 4               4
        //  \               /
        //   5             5
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.left.right = new TreeNode(5);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(3);
        expected.right = new TreeNode(2);
        expected.right.right = new TreeNode(4);
        expected.right.right.left = new TreeNode(5);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod14_DeepTree()
    {
        // Test case: Deep tree with alternating structure
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.right = new TreeNode(3);
        root.left.right.left = new TreeNode(4);
        root.left.right.left.right = new TreeNode(5);
        
        TreeNode expected = new TreeNode(1);
        expected.right = new TreeNode(2);
        expected.right.left = new TreeNode(3);
        expected.right.left.right = new TreeNode(4);
        expected.right.left.right.left = new TreeNode(5);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }

    [TestMethod]
    public void TestMethod15_SymmetricTree()
    {
        // Test case: Tree that's already symmetric
        //     1       1
        //    / \     / \
        //   2   2 => 2   2
        //  / \ / \ / \ / \
        // 3 4 4 3 3 4 4 3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.left = new TreeNode(3);
        root.left.right = new TreeNode(4);
        root.right.left = new TreeNode(4);
        root.right.right = new TreeNode(3);
        
        TreeNode expected = new TreeNode(1);
        expected.left = new TreeNode(2);
        expected.right = new TreeNode(2);
        expected.left.left = new TreeNode(3);
        expected.left.right = new TreeNode(4);
        expected.right.left = new TreeNode(4);
        expected.right.right = new TreeNode(3);
        
        TreeNode actual = InvertTree(root);
        Assert.IsTrue(IsSameTree(expected, actual));
    }
}
