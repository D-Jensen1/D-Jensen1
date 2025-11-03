namespace DivideAndConquer;

[TestClass]
public class _108
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

    // Helper method to convert tree to array for easier testing
    private List<int?> TreeToArray(TreeNode root)
    {
        if (root == null) return new List<int?>();
        
        List<int?> result = new List<int?>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        
        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();
            if (node == null)
            {
                result.Add(null);
            }
            else
            {
                result.Add(node.val);
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }
        }
        
        // Remove trailing nulls
        while (result.Count > 0 && result[result.Count - 1] == null)
        {
            result.RemoveAt(result.Count - 1);
        }
        
        return result;
    }

    // Helper method to check if tree is height-balanced BST
    private bool IsValidBST(TreeNode root, int? min = null, int? max = null)
    {
        if (root == null) return true;
        
        if ((min.HasValue && root.val <= min.Value) || 
            (max.HasValue && root.val >= max.Value))
            return false;
            
        return IsValidBST(root.left, min, root.val) && 
               IsValidBST(root.right, root.val, max);
    }

    private bool IsBalanced(TreeNode root)
    {
        return CheckHeight(root) != -1;
    }

    private int CheckHeight(TreeNode root)
    {
        if (root == null) return 0;
        
        int leftHeight = CheckHeight(root.left);
        if (leftHeight == -1) return -1;
        
        int rightHeight = CheckHeight(root.right);
        if (rightHeight == -1) return -1;
        
        if (Math.Abs(leftHeight - rightHeight) > 1) return -1;
        
        return Math.Max(leftHeight, rightHeight) + 1;
    }

/*
    #region Solution
    public TreeNode SortedArrayToBST(int[] nums)
    {
        
        return Helper(nums, 0, nums.Length - 1);
    }
    public TreeNode Helper(int[] nums, int start, int end)
    {
        if (start > end) return null;
        int mid = start + (end - start) / 2;
        TreeNode root = new TreeNode(nums[mid]);
        root.left = Helper(nums, start, mid - 1);
        root.right = Helper(nums, mid + 1, end);

        return root;
    }
    #endregion
*/

    #region Solution
    public TreeNode SortedArrayToBST(int[] nums)
    {

        return Helper(nums, 0, nums.Length - 1);
    }
    public TreeNode Helper(int[] nums, int start, int end)
    {
        if (start > end) return null;
        int mid = start + (end - start) / 2;
        TreeNode root = new TreeNode(nums[mid]);
        root.left = Helper(nums, start, mid - 1);
        root.right = Helper(nums, mid + 1, end);

        return root;
    }
    #endregion

    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: nums = [-10,-3,0,5,9]
        // Expected: A height-balanced BST, one possible answer is [0,-3,9,-10,null,5]
        //     0
        //    / \
        //  -3   9
        //  /   /
        // -10 5
        int[] nums = [-10, -3, 0, 5, 9];
        TreeNode result = SortedArrayToBST(nums);
        
        // Verify it's a valid BST
        Assert.IsTrue(IsValidBST(result));
        
        // Verify it's height-balanced
        Assert.IsTrue(IsBalanced(result));
        
        // Verify root is middle element
        Assert.AreEqual(0, result.val);
    }

    [TestMethod]
    public void TestMethod2_SimpleArray()
    {
        // Test case: nums = [1,3]
        // Expected: A height-balanced BST, possible answers are [3,1] or [1,null,3]
        //   3     or    1
        //  /              \
        // 1                3
        int[] nums = [1, 3];
        TreeNode result = SortedArrayToBST(nums);
        
        // Verify it's a valid BST
        Assert.IsTrue(IsValidBST(result));
        
        // Verify it's height-balanced
        Assert.IsTrue(IsBalanced(result));
        
        // Should have 2 nodes
        Assert.IsNotNull(result);
        Assert.IsTrue((result.left != null && result.right == null) || 
                     (result.left == null && result.right != null));
    }

    [TestMethod]
    public void TestMethod3_SingleElement()
    {
        // Test case: nums = [1]
        // Expected: [1]
        int[] nums = [1];
        TreeNode result = SortedArrayToBST(nums);
        
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.val);
        Assert.IsNull(result.left);
        Assert.IsNull(result.right);
    }

    [TestMethod]
    public void TestMethod4_EmptyArray()
    {
        // Test case: nums = []
        // Expected: null
        int[] nums = [];
        TreeNode result = SortedArrayToBST(nums);
        
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TestMethod5_LargerArray()
    {
        // Test case: nums = [1,2,3,4,5,6,7]
        // Expected: A height-balanced BST
        //       4
        //     /   \
        //    2     6
        //   / \   / \
        //  1   3 5   7
        int[] nums = [1, 2, 3, 4, 5, 6, 7];
        TreeNode result = SortedArrayToBST(nums);
        
        // Verify it's a valid BST
        Assert.IsTrue(IsValidBST(result));
        
        // Verify it's height-balanced
        Assert.IsTrue(IsBalanced(result));
        
        // Verify root is middle element
        Assert.AreEqual(4, result.val);
    }

    [TestMethod]
    public void TestMethod6_NegativeNumbers()
    {
        // Test case: nums = [-5,-3,-1,0,2,4,6]
        // Expected: A height-balanced BST
        int[] nums = [-5, -3, -1, 0, 2, 4, 6];
        TreeNode result = SortedArrayToBST(nums);
        
        // Verify it's a valid BST
        Assert.IsTrue(IsValidBST(result));
        
        // Verify it's height-balanced
        Assert.IsTrue(IsBalanced(result));
        
        // Verify root is middle element
        Assert.AreEqual(0, result.val);
    }

    [TestMethod]
    public void TestMethod7_TwoElements()
    {
        // Test case: nums = [0,1]
        // Expected: A height-balanced BST
        int[] nums = [0, 1];
        TreeNode result = SortedArrayToBST(nums);
        
        // Verify it's a valid BST
        Assert.IsTrue(IsValidBST(result));
        
        // Verify it's height-balanced
        Assert.IsTrue(IsBalanced(result));
        
        // Should be either [0,null,1] or [1,0]
        Assert.IsTrue((result.val == 0 && result.right?.val == 1) ||
                     (result.val == 1 && result.left?.val == 0));
    }
}
