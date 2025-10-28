namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _100
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

    /*
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            if (p.val != q.val) return false;

            return CompareTrees(p, q, true);
        }

        public bool CompareTrees(TreeNode p, TreeNode q, bool result)
        {
            if (result == false) return false;
            if (p.val != q.val) return result = false;
            if ((p.left != null && q.left == null) || 
                 q.left != null && p.left == null) return false;
            if ((p.right != null && q.right == null) ||
                 q.right != null && p.right == null) return false;

            if (p.left != null) result = CompareTrees(p.left, q.left, result);
            if (p.right != null) result = CompareTrees(p.right, q.right, result);
    
            return result;
        }
    */

    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;

        // Both nodes exist: check if values are equal and recursively check subtrees
        return p.val == q.val && 
               IsSameTree(p.left, q.left) && 
               IsSameTree(p.right, q.right);
    }

    [TestMethod]
    public void TestMethod1_IdenticalTrees()
    {
        // Test case: p = [1,2,3], q = [1,2,3] -> true
        //   1       1
        //  / \     / \
        // 2   3   2   3
        TreeNode p = CreateTree(new int?[] { 1, 2, 3 });
        TreeNode q = CreateTree(new int?[] { 1, 2, 3 });
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_DifferentStructure()
    {
        // Test case: p = [1,2], q = [1,null,2] -> false
        //   1     1
        //  /       \
        // 2         2
        TreeNode p = new TreeNode(1);
        p.left = new TreeNode(2);
        
        TreeNode q = new TreeNode(1);
        q.right = new TreeNode(2);
        
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_DifferentValues()
    {
        // Test case: p = [1,2,1], q = [1,1,2] -> false
        //   1       1
        //  / \     / \
        // 2   1   1   2
        TreeNode p = CreateTree(new int?[] { 1, 2, 1 });
        TreeNode q = CreateTree(new int?[] { 1, 1, 2 });
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_BothEmpty()
    {
        // Test case: p = null, q = null -> true
        TreeNode p = null;
        TreeNode q = null;
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_OneEmpty()
    {
        // Test case: p = [1], q = null -> false
        TreeNode p = new TreeNode(1);
        TreeNode q = null;
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_SingleNodeSame()
    {
        // Test case: p = [1], q = [1] -> true
        TreeNode p = new TreeNode(1);
        TreeNode q = new TreeNode(1);
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_SingleNodeDifferent()
    {
        // Test case: p = [1], q = [2] -> false
        TreeNode p = new TreeNode(1);
        TreeNode q = new TreeNode(2);
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_ComplexIdentical()
    {
        // Test case: Complex identical trees
        //       1
        //      / \
        //     2   3
        //    / \   \
        //   4   5   6
        TreeNode p = CreateTree(new int?[] { 1, 2, 3, 4, 5, null, 6 });
        TreeNode q = CreateTree(new int?[] { 1, 2, 3, 4, 5, null, 6 });
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_DifferentDepth()
    {
        // Test case: Different depths
        //   1     1
        //  /     / \
        // 2     2   3
        TreeNode p = new TreeNode(1);
        p.left = new TreeNode(2);
        
        TreeNode q = CreateTree(new int?[] { 1, 2, 3 });
        
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_MissingSubtree()
    {
        // Test case: One tree has missing subtree
        //     1         1
        //    / \       / \
        //   2   3     2   3
        //  /         /
        // 4         4
        //            \
        //             5
        TreeNode p = CreateTree(new int?[] { 1, 2, 3, 4 });
        
        TreeNode q = new TreeNode(1);
        q.left = new TreeNode(2);
        q.right = new TreeNode(3);
        q.left.left = new TreeNode(4);
        q.left.left.right = new TreeNode(5);
        
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_NegativeValues()
    {
        // Test case: Trees with negative values
        //    -1      -1
        //   /  \    /  \
        //  -2  -3  -2  -3
        TreeNode p = CreateTree(new int?[] { -1, -2, -3 });
        TreeNode q = CreateTree(new int?[] { -1, -2, -3 });
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_SkewedTrees()
    {
        // Test case: Right-skewed trees
        // 1     1
        //  \     \
        //   2     2
        //    \     \
        //     3     3
        TreeNode p = new TreeNode(1);
        p.right = new TreeNode(2);
        p.right.right = new TreeNode(3);
        
        TreeNode q = new TreeNode(1);
        q.right = new TreeNode(2);
        q.right.right = new TreeNode(3);
        
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_DifferentSkew()
    {
        // Test case: Left vs Right skewed
        // 1     1
        //  \   /
        //   2 2
        TreeNode p = new TreeNode(1);
        p.right = new TreeNode(2);
        
        TreeNode q = new TreeNode(1);
        q.left = new TreeNode(2);
        
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_LargeIdenticalTrees()
    {
        // Test case: Larger identical trees
        TreeNode p = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        TreeNode q = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        bool expected = true;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_SubtleStructuralDifference()
    {
        // Test case: Very similar trees with one structural difference
        //     1           1
        //    / \         / \
        //   2   3       2   3
        //  /           / \
        // 4           4   null
        TreeNode p = new TreeNode(1);
        p.left = new TreeNode(2);
        p.right = new TreeNode(3);
        p.left.left = new TreeNode(4);
        
        TreeNode q = new TreeNode(1);
        q.left = new TreeNode(2);
        q.right = new TreeNode(3);
        q.left.left = new TreeNode(4);
        q.left.right = new TreeNode(5); // Extra node!
        
        bool expected = false;
        bool actual = IsSameTree(p, q);
        Assert.AreEqual(expected, actual);
    }
}
