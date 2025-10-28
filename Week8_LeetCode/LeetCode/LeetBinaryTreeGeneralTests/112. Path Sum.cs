namespace LeetBinaryTreeGeneralTests;

[TestClass]
public class _112
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

    public bool HasPathSum(TreeNode root, int targetSum)
    {
        // Base case: empty tree
        if (root == null) return false;

        // If this is a leaf node, check if remaining sum equals node value
        if (root.left == null && root.right == null) return root.val == targetSum;

        // Recursively check left and right subtrees with updated target sum
        int remainingSum = targetSum - root.val;
        return HasPathSum(root.left, remainingSum) || HasPathSum(root.right, remainingSum);
    }

    [TestMethod]
    public void TestMethod1_BasicExampleTrue()
    {
        // Test case: root = [5,4,8,11,null,13,4,7,2,null,null,null,1], targetSum = 22 -> true
        // Path: 5 -> 4 -> 11 -> 2 = 22
        TreeNode root = CreateTree(new int?[] { 5, 4, 8, 11, null, 13, 4, 7, 2, null, null, null, 1 });
        int targetSum = 22;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_BasicExampleFalse()
    {
        // Test case: root = [1,2,3], targetSum = 5 -> false
        // No path sums to 5
        TreeNode root = CreateTree(new int?[] { 1, 2, 3 });
        int targetSum = 5;
        bool expected = false;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_EmptyTree()
    {
        // Test case: root = null, targetSum = 0 -> false
        TreeNode root = null;
        int targetSum = 0;
        bool expected = false;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_SingleNodeTrue()
    {
        // Test case: root = [1], targetSum = 1 -> true
        TreeNode root = new TreeNode(1);
        int targetSum = 1;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_SingleNodeFalse()
    {
        // Test case: root = [1], targetSum = 2 -> false
        TreeNode root = new TreeNode(1);
        int targetSum = 2;
        bool expected = false;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_OnlyLeftPath()
    {
        // Test case: Left-skewed tree
        //   1
        //  /
        // 2
        //  \
        //   3
        // Path: 1 -> 2 -> 3 = 6
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.right = new TreeNode(3);
        int targetSum = 6;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_OnlyRightPath()
    {
        // Test case: Right-skewed tree
        // 1
        //  \
        //   2
        //    \
        //     3
        // Path: 1 -> 2 -> 3 = 6
        TreeNode root = new TreeNode(1);
        root.right = new TreeNode(2);
        root.right.right = new TreeNode(3);
        int targetSum = 6;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_MultiplePathsOneValid()
    {
        // Test case: Multiple paths, one matches target
        //     1
        //    / \
        //   2   3
        //  /   /
        // 4   5
        // Paths: 1->2->4=7, 1->3->5=9
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.right.left = new TreeNode(5);
        int targetSum = 7;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_NegativeNumbers()
    {
        // Test case: Tree with negative numbers
        //    -1
        //   /  \
        //  -2  -3
        // Paths: -1->-2=-3, -1->-3=-4
        TreeNode root = new TreeNode(-1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(-3);
        int targetSum = -3;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_ZeroSum()
    {
        // Test case: Target sum is zero
        //   0
        //  / \
        // 1  -1
        // Paths: 0->1=1, 0->-1=-1
        TreeNode root = new TreeNode(0);
        root.left = new TreeNode(1);
        root.right = new TreeNode(-1);
        int targetSum = -1;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod11_ZeroNodes()
    {
        // Test case: Tree with zero values
        //   0
        //  / \
        // 0   0
        // All paths sum to 0
        TreeNode root = new TreeNode(0);
        root.left = new TreeNode(0);
        root.right = new TreeNode(0);
        int targetSum = 0;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod12_LargeNumbers()
    {
        // Test case: Tree with large numbers
        //    1000
        //   /    \
        // 500    200
        // Paths: 1000->500=1500, 1000->200=1200
        TreeNode root = new TreeNode(1000);
        root.left = new TreeNode(500);
        root.right = new TreeNode(200);
        int targetSum = 1200;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod13_DeepTree()
    {
        // Test case: Deeper tree
        //       1
        //      / \
        //     2   3
        //    /   / \
        //   4   5   6
        //  /
        // 7
        // Path: 1->2->4->7 = 14
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.right.left = new TreeNode(5);
        root.right.right = new TreeNode(6);
        root.left.left.left = new TreeNode(7);
        int targetSum = 14;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod14_NoValidPath()
    {
        // Test case: No path matches target
        //   1
        //  / \
        // 2   3
        // Paths: 1->2=3, 1->3=4
        // Target: 10 (no match)
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        int targetSum = 10;
        bool expected = false;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod15_PartialPathMatch()
    {
        // Test case: Partial path might equal target but full path doesn't
        //     5
        //    / \
        //   4   8
        //  /   / \
        // 1   3   6
        // Path 5->4 = 9, but full path 5->4->1 = 10
        TreeNode root = new TreeNode(5);
        root.left = new TreeNode(4);
        root.right = new TreeNode(8);
        root.left.left = new TreeNode(1);
        root.right.left = new TreeNode(3);
        root.right.right = new TreeNode(6);
        int targetSum = 9; // Only partial path, not full to leaf
        bool expected = false;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod16_NegativeTarget()
    {
        // Test case: Negative target sum
        //     1
        //    / \
        //   -2  3
        // Path: 1->-2 = -1
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(-2);
        root.right = new TreeNode(3);
        int targetSum = -1;
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod17_ComplexTree()
    {
        // Test case: More complex tree structure
        //        1
        //       / \
        //      2   3
        //     / \   \
        //    4   5   6
        //   /     \   \
        //  7       8   9
        // Various paths with different sums
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        root.right.right = new TreeNode(6);
        root.left.left.left = new TreeNode(7);
        root.left.right.right = new TreeNode(8);
        root.right.right.right = new TreeNode(9);
        int targetSum = 16; // Path: 1->2->5->8 = 16
        bool expected = true;
        bool actual = HasPathSum(root, targetSum);
        Assert.AreEqual(expected, actual);
    }
}
