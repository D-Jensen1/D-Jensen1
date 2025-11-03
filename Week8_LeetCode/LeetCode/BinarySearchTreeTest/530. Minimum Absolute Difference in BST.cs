namespace BinarySearchTreeTest;

[TestClass]
public class _530
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
    // Helper method to create a BST for testing
    private TreeNode CreateBST(int[] values)
    {
        TreeNode root = null;
        foreach (int val in values)
        {
            root = InsertIntoBST(root, val);
        }
        return root;
    }

    private TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
            return new TreeNode(val);

        if (val < root.val)
            root.left = InsertIntoBST(root.left, val);
        else
            root.right = InsertIntoBST(root.right, val);

        return root;
    }
    /*
        public int GetMinimumDifference(TreeNode root)
        {
            int minDiff = int.MaxValue;
            TreeNode prev = null;

            InOrderTraversal(root, ref prev, ref minDiff);

            return minDiff;
        }

        private void InOrderTraversal(TreeNode node, ref TreeNode prev, ref int minDiff)
        {
            if (node == null)
                return;

            // Traverse left subtree
            InOrderTraversal(node.left, ref prev, ref minDiff);

            // Process current node
            if (prev != null)
            {
                int diff = node.val - prev.val;
                minDiff = Math.Min(minDiff, diff);
            }
            prev = node;

            // Traverse right subtree
            InOrderTraversal(node.right, ref prev, ref minDiff);
        }
    */

    private TreeNode prevNode = null;
    int minDiff = int.MaxValue;
    public int GetMinimumDifference(TreeNode root)
    {
        prevNode = root;
        InOrderTraversal(root);
        return minDiff;
    }

    private void InOrderTraversal(TreeNode node)
    {
        if (node == null) return;

        InOrderTraversal(node.left);

        if (prevNode != node)
            minDiff = Math.Min(Math.Abs(prevNode.val - node.val),minDiff);
        
        prevNode = node;

        InOrderTraversal(node.right);
    }

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: BST = [4,2,6,1,3] -> 1
        //     4
        //    / \
        //   2   6
        //  / \
        // 1   3
        // In-order: [1,2,3,4,6] -> differences: [1,1,1,2] -> min = 1
        TreeNode root = new TreeNode(4);
        root.left = new TreeNode(2);
        root.right = new TreeNode(6);
        root.left.left = new TreeNode(1);
        root.left.right = new TreeNode(3);
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SecondExample()
    {
        // Test case: BST = [1,0,48,null,null,12,49] -> 1
        //      1
        //     / \
        //    0  48
        //      /  \
        //     12  49
        // In-order: [0,1,12,48,49] -> differences: [1,11,36,1] -> min = 1
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(0);
        root.right = new TreeNode(48);
        root.right.left = new TreeNode(12);
        root.right.right = new TreeNode(49);
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_TwoNodes()
    {
        // Test case: BST = [1,null,3] -> 2
        //   1
        //    \
        //     3
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(3);
        
        int expected = 2;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_LeftSkewedBST()
    {
        // Test case: Left-skewed BST [5,3,1]
        //   5
        //  /
        // 3
        ///
        //1
        TreeNode root = new TreeNode(5);
        root.left = new TreeNode(3);
        root.left.left = new TreeNode(1);
        
        int expected = 2;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_RightSkewedBST()
    {
        // Test case: Right-skewed BST [1,3,5]
        // 1
        //  \
        //   3
        //    \
        //     5
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(3);
        root.right.right = new TreeNode(5);
        
        int expected = 2;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_ConsecutiveNumbers()
    {
        // Test case: BST with consecutive numbers [1,2,3,4,5]
        TreeNode root = CreateBST(new int[] { 3, 1, 4, 2, 5 });
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_LargeGaps()
    {
        // Test case: BST with large gaps [1,10,20]
        //   10
        //   / \
        //  1  20
        TreeNode root = new TreeNode(10);
        root.left = new TreeNode(1);
        root.right = new TreeNode(20);
        
        int expected = 9;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_SingleNode()
    {
        // Test case: Single node BST
        // Note: This test case might not be valid as per problem constraints
        // (need at least 2 nodes), but testing for robustness
        TreeNode root = new TreeNode(1);
        
        // With single node, there's no other node to compare
        // The algorithm should handle this gracefully
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(int.MaxValue, actual);
    }

    [TestMethod]
    public void TestMethod9_PerfectBST()
    {
        // Test case: Perfect BST [4,2,6,1,3,5,7]
        //       4
        //      / \
        //     2   6
        //    / \ / \
        //   1 3 5  7
        TreeNode root = CreateBST(new int[] { 4, 2, 6, 1, 3, 5, 7 });
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_UnbalancedBST()
    {
        // Test case: Unbalanced BST
        //     10
        //    /  \
        //   5   15
        //  /   /  \
        // 3   12  17
        TreeNode root = new TreeNode(10);
        root.left = new TreeNode(5);
        root.right = new TreeNode(15);
        root.left.left = new TreeNode(3);
        root.right.left = new TreeNode(12);
        root.right.right = new TreeNode(17);
        
        int expected = 2;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LargeNumbers()
    {
        // Test case: BST with large numbers
        //    1000
        //   /    \
        // 500   1500
        TreeNode root = new TreeNode(1000);
        root.left = new TreeNode(500);
        root.right = new TreeNode(1500);
        
        int expected = 500;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_SmallDifferences()
    {
        // Test case: BST with very small differences
        //    100
        //   /   \
        //  99   101
        TreeNode root = new TreeNode(100);
        root.left = new TreeNode(99);
        root.right = new TreeNode(101);
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_ComplexBST()
    {
        // Test case: More complex BST structure
        //        20
        //       /  \
        //      10   30
        //     / \   / \
        //    5  15 25 35
        //   /     \
        //  1      18
        TreeNode root = new TreeNode(20);
        root.left = new TreeNode(10);
        root.right = new TreeNode(30);
        root.left.left = new TreeNode(5);
        root.left.right = new TreeNode(15);
        root.right.left = new TreeNode(25);
        root.right.right = new TreeNode(35);
        root.left.left.left = new TreeNode(1);
        root.left.right.right = new TreeNode(18);
        
        int expected = 2;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_IdenticalSubtrees()
    {
        // Test case: BST where minimum difference occurs in different subtrees
        //      50
        //     /  \
        //   25    75
        //  / \   / \
        // 24 26 74 76
        TreeNode root = new TreeNode(50);
        root.left = new TreeNode(25);
        root.right = new TreeNode(75);
        root.left.left = new TreeNode(24);
        root.left.right = new TreeNode(26);
        root.right.left = new TreeNode(74);
        root.right.right = new TreeNode(76);
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_PowerOfTwoValues()
    {
        // Test case: BST with power of 2 values [1,2,4,8,16]
        TreeNode root = CreateBST(new int[] { 8, 4, 16, 2, 1 });
        
        int expected = 1;
        int actual = GetMinimumDifference(root);
        Assert.AreEqual(expected, actual);
    }
}
