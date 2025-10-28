namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _101
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

    public bool IsSymmetric(TreeNode root)
    {
        if (root == null) return true;
        return CheckNode(root.left, root.right);
    }
    public bool CheckNode(TreeNode left, TreeNode right)
    {
        if (left == null && right == null) return true;
        if (left == null || right == null) return false;

        return left.val == right.val
               && CheckNode(left.left, right.right)
               && CheckNode(left.right, right.left);
    }


    [TestMethod]
    public void TestMethod1_SymmetricTree()
    {
        // Test case: root = [1,2,2,3,4,4,3] -> true
        //     1
        //    / \
        //   2   2
        //  / \ / \
        // 3  4 4  3
        TreeNode root = CreateTree(new int?[] { 1, 2, 2, 3, 4, 4, 3 });
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_AsymmetricTree()
    {
        // Test case: root = [1,2,2,null,3,null,3] -> false
        //     1
        //    / \
        //   2   2
        //    \   \
        //     3   3
        TreeNode root = CreateTree(new int?[] { 1, 2, 2, null, 3, null, 3 });
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_SingleNode()
    {
        // Test case: root = [1] -> true
        TreeNode root = new TreeNode(1);
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_EmptyTree()
    {
        // Test case: root = null -> true
        TreeNode root = null;
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_TwoNodesSymmetric()
    {
        // Test case: root = [1,2,2] -> true
        //   1
        //  / \
        // 2   2
        TreeNode root = CreateTree(new int?[] { 1, 2, 2 });
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_TwoNodesAsymmetric()
    {
        // Test case: root = [1,2,3] -> false
        //   1
        //  / \
        // 2   3
        TreeNode root = CreateTree(new int?[] { 1, 2, 3 });
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_OnlyLeftChild()
    {
        // Test case: root = [1,2,null] -> false
        //   1
        //  /
        // 2
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_OnlyRightChild()
    {
        // Test case: root = [1,null,2] -> false
        //   1
        //    \
        //     2
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_DeepSymmetricTree()
    {
        // Test case: Deeper symmetric tree
        //       1
        //      / \
        //     2   2
        //    / \ / \
        //   3  4 4  3
        //  /        \
        // 5          5
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.left = new TreeNode(3);
        root.left.right = new TreeNode(4);
        root.right.left = new TreeNode(4);
        root.right.right = new TreeNode(3);
        root.left.left.left = new TreeNode(5);
        root.right.right.right = new TreeNode(5);
        
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_DeepAsymmetricTree()
    {
        // Test case: Deeper asymmetric tree (missing one node)
        //       1
        //      / \
        //     2   2
        //    / \ / \
        //   3  4 4  3
        //  /      \
        // 5        5
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.left = new TreeNode(3);
        root.left.right = new TreeNode(4);
        root.right.left = new TreeNode(4);
        root.right.right = new TreeNode(3);
        root.left.left.left = new TreeNode(5);
        root.right.right.left = new TreeNode(5); // Wrong position!
        
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_SymmetricWithNulls()
    {
        // Test case: Symmetric tree with null nodes
        //     1
        //    / \
        //   2   2
        //  /     \
        // 3       3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.left = new TreeNode(3);
        root.right.right = new TreeNode(3);
        
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_AsymmetricWithNulls()
    {
        // Test case: Asymmetric tree with null nodes
        //     1
        //    / \
        //   2   2
        //    \   \
        //     3   3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.right = new TreeNode(3);
        root.right.right = new TreeNode(3); // Should be root.right.left for symmetry
        
        bool expected = false;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_NegativeValues()
    {
        // Test case: Symmetric tree with negative values
        //    -1
        //   /  \
        //  -2  -2
        TreeNode root = new TreeNode(-1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(-2);
        
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_ZeroValues()
    {
        // Test case: Symmetric tree with zero values
        //   0
        //  / \
        // 0   0
        TreeNode root = new TreeNode(0);
        root.left = new TreeNode(0);
        root.right = new TreeNode(0);
        
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ComplexSymmetricPattern()
    {
        // Test case: Complex symmetric pattern
        //       1
        //      / \
        //     2   2
        //    /|   |\
        //   3 4   4 3
        //   |       |
        //   5       5
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(2);
        root.left.left = new TreeNode(3);
        root.left.right = new TreeNode(4);
        root.right.left = new TreeNode(4);
        root.right.right = new TreeNode(3);
        root.left.left.left = new TreeNode(5);
        root.right.right.right = new TreeNode(5);
        
        bool expected = true;
        bool actual = IsSymmetric(root);
        Assert.AreEqual(expected, actual);
    }
}
