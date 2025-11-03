using System.Xml.Linq;

namespace BinaryTreeBFSTest;

[TestClass]
public class _637
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


    public IList<double> AverageOfLevels(TreeNode root)
    {
        List<double> result = new();

        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        while (queue.Count != 0)
        {
            double sum = 0;
            int queueCount = queue.Count;

            for (int i = 0; i < queueCount; i++)
            {

                TreeNode node = queue.Dequeue();
                sum += node.val;

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }

            result.Add(sum / queueCount);
        }

        return result;
    }



    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: root = [3,9,20,null,null,15,7] -> [3.0, 14.5, 11.0]
        //     3
        //    / \
        //   9  20
        //     /  \
        //    15   7
        // Level 0: [3] -> average = 3.0
        // Level 1: [9, 20] -> average = 14.5
        // Level 2: [15, 7] -> average = 11.0
        TreeNode root = CreateTree(new int?[] { 3, 9, 20, null, null, 15, 7 });
        IList<double> expected = new List<double> { 3.0, 14.5, 11.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod2_SingleNode()
    {
        // Test case: root = [1] -> [1.0]
        TreeNode root = new TreeNode(1);
        IList<double> expected = new List<double> { 1.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod4_TwoLevels()
    {
        // Test case: root = [1,2,3] -> [1.0, 2.5]
        //   1
        //  / \
        // 2   3
        TreeNode root = CreateTree(new int?[] { 1, 2, 3 });
        IList<double> expected = new List<double> { 1.0, 2.5 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod5_LeftSkewedTree()
    {
        // Test case: Left-skewed tree
        //   1
        //  |
        // 2
        // |
        // 3
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.left.left = new TreeNode(3);
        IList<double> expected = new List<double> { 1.0, 2.0, 3.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod6_RightSkewedTree()
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
        IList<double> expected = new List<double> { 1.0, 2.0, 3.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod7_PerfectBinaryTree()
    {
        // Test case: Perfect binary tree
        //       1
        //      / \
        //     2   3
        //    / \ / \
        //   4 5 6  7
        TreeNode root = CreateTree(new int?[] { 1, 2, 3, 4, 5, 6, 7 });
        IList<double> expected = new List<double> { 1.0, 2.5, 5.5 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod8_NegativeNumbers()
    {
        // Test case: Tree with negative numbers
        //    -1
        //   /  \
        //  -2  -3
        TreeNode root = CreateTree(new int?[] { -1, -2, -3 });
        IList<double> expected = new List<double> { -1.0, -2.5 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod9_ZeroValues()
    {
        // Test case: Tree with zero values
        //   0
        //  / \
        // 0   1
        TreeNode root = CreateTree(new int?[] { 0, 0, 1 });
        IList<double> expected = new List<double> { 0.0, 0.5 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod10_LargeNumbers()
    {
        // Test case: Tree with large numbers
        //    1000
        //   /    \
        // 2000   3000
        TreeNode root = CreateTree(new int?[] { 1000, 2000, 3000 });
        IList<double> expected = new List<double> { 1000.0, 2500.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod11_UnbalancedTree()
    {
        // Test case: Unbalanced tree
        //     1
        //    / \
        //   2   3
        //  /     \
        // 4       5
        //        /
        //       6
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.right.right = new TreeNode(5);
        root.right.right.left = new TreeNode(6);
        
        IList<double> expected = new List<double> { 1.0, 2.5, 4.5, 6.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod12_SingleChildNodes()
    {
        // Test case: Tree with single child nodes
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
        
        IList<double> expected = new List<double> { 1.0, 2.0, 3.0, 4.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod13_IntegerOverflowPrevention()
    {
        // Test case: Large values that could cause integer overflow
        //     2147483647
        //    /          \
        // 2147483647  2147483647
        TreeNode root = new TreeNode(int.MaxValue);
        root.left = new TreeNode(int.MaxValue);
        root.right = new TreeNode(int.MaxValue);
        
        IList<double> expected = new List<double> { (double)int.MaxValue, (double)int.MaxValue };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod14_MixedPositiveNegative()
    {
        // Test case: Mix of positive and negative numbers
        //     5
        //    / \
        //  -3   9
        //  / \   \
        // 2  -1   7
        TreeNode root = new TreeNode(5);
        root.left = new TreeNode(-3);
        root.right = new TreeNode(9);
        root.left.left = new TreeNode(2);
        root.left.right = new TreeNode(-1);
        root.right.right = new TreeNode(7);
        
        IList<double> expected = new List<double> { 5.0, 3.0, 8.0/3.0 };
        IList<double> actual = AverageOfLevels(root);
        
        // Check each value with tolerance for floating point precision
        Assert.AreEqual(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], actual[i], 0.000001);
        }
    }

    [TestMethod]
    public void TestMethod15_DeepTree()
    {
        // Test case: Deeper tree structure
        //         1
        //       /   \
        //      2     3
        //     / \   / \
        //    4   5 6   7
        //   /
        //  8
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        root.right.left = new TreeNode(6);
        root.right.right = new TreeNode(7);
        root.left.left.left = new TreeNode(8);
        
        IList<double> expected = new List<double> { 1.0, 2.5, 5.5, 8.0 };
        IList<double> actual = AverageOfLevels(root);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }
}
