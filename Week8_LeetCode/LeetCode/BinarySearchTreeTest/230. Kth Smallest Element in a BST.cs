namespace BinarySearchTreeTest;

[TestClass]
public class _230
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
        #region Traverse Entire Tree
        public int KthSmallest(TreeNode? root, int k)
        {
            List<int> allNodeValues = new();
            PopulateList(allNodeValues, root);
            allNodeValues.Sort();
            return allNodeValues[k - 1];
        }
        private void PopulateList(List<int> list, TreeNode node)
        {
            if (node == null) return;
            list.Add(node.val);
            if (node.left != null) PopulateList(list, node.left);
            if (node.right != null) PopulateList(list, node.right);
        }
        #endregion
    */

    #region Traverse Tree Until Found
    public int KthSmallest(TreeNode? root, int k)
    {
        int count = 0;
        int result = 0;
        
        void InOrder(TreeNode? node)
        {
            if (node == null || count >= k) return;
            
            InOrder(node.left);     // Visit left subtree
            
            count++;                // Process current node
            if (count == k)
            {
                result = node.val;
                return;
            }
            
            InOrder(node.right);    // Visit right subtree
        }
        
        InOrder(root);
        return result;
    }

    #endregion
    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: BST = [3,1,4,null,2], k = 1 -> 1
        //   3
        //  / \
        // 1   4
        //  \
        //   2
        // In-order: [1,2,3,4] -> 1st smallest = 1
        TreeNode root = new TreeNode(3);
        root.left = new TreeNode(1);
        root.right = new TreeNode(4);
        root.left.right = new TreeNode(2);
        
        int k = 1;
        int expected = 1;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SecondExample()
    {
        // Test case: BST = [5,3,6,2,4,null,null,1], k = 3 -> 3
        //       5
        //      / \
        //     3   6
        //    / \
        //   2   4
        //  /
        // 1
        // In-order: [1,2,3,4,5,6] -> 3rd smallest = 3
        TreeNode root = new TreeNode(5);
        root.left = new TreeNode(3);
        root.right = new TreeNode(6);
        root.left.left = new TreeNode(2);
        root.left.right = new TreeNode(4);
        root.left.left.left = new TreeNode(1);
        
        int k = 3;
        int expected = 3;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_FirstSmallest()
    {
        // Test case: k = 1 (smallest element)
        TreeNode root = CreateBST(new int[] { 5, 3, 7, 1, 9 });
        
        int k = 1;
        int expected = 1;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_LastElement()
    {
        // Test case: k equals total number of nodes (largest element)
        TreeNode root = CreateBST(new int[] { 5, 3, 7, 1, 9 });
        
        int k = 5;
        int expected = 9;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleNode()
    {
        // Test case: Single node BST, k = 1
        TreeNode root = new TreeNode(42);
        
        int k = 1;
        int expected = 42;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_TwoNodes()
    {
        // Test case: Two nodes, different k values
        //   2
        //  /
        // 1
        TreeNode root = new TreeNode(2);
        root.left = new TreeNode(1);
        
        int k1 = 1;
        int expected1 = 1;
        int actual1 = KthSmallest(root, k1);
        Assert.AreEqual(expected1, actual1);
        
        int k2 = 2;
        int expected2 = 2;
        int actual2 = KthSmallest(root, k2);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void TestMethod8_RightSkewedBST()
    {
        // Test case: Right-skewed BST
        // 1
        //  \
        //   2
        //    \
        //     3
        //      \
        //       4
        //        \
        //         5
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        root.right.right = new TreeNode(3);
        root.right.right.right = new TreeNode(4);
        root.right.right.right.right = new TreeNode(5);
        
        int k = 4;
        int expected = 4;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
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
        
        int k = 4;
        int expected = 4;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_MiddleElement()
    {
        // Test case: Finding middle element in odd-sized BST
        TreeNode root = CreateBST(new int[] { 10, 5, 15, 3, 7, 12, 18 });
        
        int k = 4; // Middle of 7 elements
        int expected = 10;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_LargeNumbers()
    {
        // Test case: BST with large numbers
        TreeNode root = CreateBST(new int[] { 1000, 500, 1500, 250, 750, 1250, 1750 });
        
        int k = 5;
        int expected = 1250;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_NegativeNumbers()
    {
        // Test case: BST with negative numbers
        TreeNode root = CreateBST(new int[] { 0, -5, 5, -10, -2, 2, 10 });
        
        int k = 2;
        int expected = -5;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_MixedNumbers()
    {
        // Test case: BST with mixed positive/negative numbers
        TreeNode root = CreateBST(new int[] { 1, -1, 3, -3, 0, 2, 4 });
        
        int k = 3;
        int expected = 0;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_ConsecutiveNumbers()
    {
        // Test case: BST with consecutive numbers
        TreeNode root = CreateBST(new int[] { 5, 4, 6, 3, 7, 2, 8, 1, 9 });
        
        int k = 6;
        int expected = 6;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_ComplexBST()
    {
        // Test case: More complex BST structure
        //        50
        //       /  \
        //     30    70
        //    / \   / \
        //  20  40 60 80
        //  /   /     \
        // 10  35     90
        TreeNode root = new TreeNode(50);
        root.left = new TreeNode(30);
        root.right = new TreeNode(70);
        root.left.left = new TreeNode(20);
        root.left.right = new TreeNode(40);
        root.right.left = new TreeNode(60);
        root.right.right = new TreeNode(80);
        root.left.left.left = new TreeNode(10);
        root.left.right.left = new TreeNode(35);
        root.right.right.right = new TreeNode(90);
        
        int k = 7;
        int expected = 60;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_PowerOfTwoValues()
    {
        // Test case: BST with power of 2 values
        TreeNode root = CreateBST(new int[] { 8, 4, 16, 2, 6, 12, 24, 1, 3, 5, 7 });
        
        int k = 8;
        int expected = 8;
        int actual = KthSmallest(root, k);
        Assert.AreEqual(expected, actual);
    }
}
