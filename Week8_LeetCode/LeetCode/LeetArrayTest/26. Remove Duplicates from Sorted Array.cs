using System;
using System.Linq;

namespace LeetArrayTest;

class _26
{
    
    public int RemoveDuplicates(int[] nums)
    {
        HashSet<int> uniqueNums = new(nums);
        Array.Copy(uniqueNums.ToArray(), nums, uniqueNums.Count);
        return uniqueNums.Count();
    }

    public int RemoveDuplicates2(int[] nums)
    {
        int count = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] != nums[i])
            {
                nums[count++] = nums[i];
            }
        }
        return count;
    }
}

[TestClass]
public class _26Test
{
    private _26 _solution = new _26();

    [TestMethod]
    public void RemoveDuplicates_BasicCase_RemovesDuplicatesCorrectly()
    {
        // Arrange
        int[] nums = [1, 1, 2];
        int[] expectedFirstK = [1, 2];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(2, k);
        CollectionAssert.AreEqual(expectedFirstK, nums.Take(k).ToArray());
    }

    [TestMethod]
    public void RemoveDuplicates_MultipleDuplicates_RemovesAllDuplicates()
    {
        // Arrange
        int[] nums = [0, 0, 1, 1, 1, 2, 2, 3, 3, 4];
        int[] expectedFirstK = [0, 1, 2, 3, 4];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(5, k);
        CollectionAssert.AreEqual(expectedFirstK, nums.Take(k).ToArray());
    }

    [TestMethod]
    public void RemoveDuplicates_EmptyArray_ReturnsZero()
    {
        // Arrange
        int[] nums = [];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(0, k);
    }

    [TestMethod]
    public void RemoveDuplicates_SingleElement_ReturnsOne()
    {
        // Arrange
        int[] nums = [1];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(1, k);
        Assert.AreEqual(1, nums[0]);
    }

    [TestMethod]
    public void RemoveDuplicates_AllElementsSame_ReturnsOne()
    {
        // Arrange
        int[] nums = [1, 1, 1, 1, 1];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(1, k);
        Assert.AreEqual(1, nums[0]);
    }

    [TestMethod]
    public void RemoveDuplicates_AllElementsUnique_ReturnsOriginalLength()
    {
        // Arrange
        int[] nums = [1, 2, 3, 4, 5];
        int[] expected = [1, 2, 3, 4, 5];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(5, k);
        CollectionAssert.AreEqual(expected, nums.Take(k).ToArray());
    }

    [TestMethod]
    public void RemoveDuplicates_TwoElements_Identical_ReturnsOne()
    {
        // Arrange
        int[] nums = [1, 1];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(1, k);
        Assert.AreEqual(1, nums[0]);
    }

    [TestMethod]
    public void RemoveDuplicates_TwoElements_Different_ReturnsTwo()
    {
        // Arrange
        int[] nums = [1, 2];
        int[] expected = [1, 2];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(2, k);
        CollectionAssert.AreEqual(expected, nums.Take(k).ToArray());
    }

    [TestMethod]
    public void RemoveDuplicates_NegativeNumbers_WorksCorrectly()
    {
        // Arrange
        int[] nums = [-3, -1, -1, 0, 0, 0, 1, 1];
        int[] expectedFirstK = [-3, -1, 0, 1];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(4, k);
        CollectionAssert.AreEqual(expectedFirstK, nums.Take(k).ToArray());
    }

    [TestMethod]
    public void RemoveDuplicates_LargeNumbers_WorksCorrectly()
    {
        // Arrange
        int[] nums = [1000, 1000, 2000, 3000, 3000, 4000];
        int[] expectedFirstK = [1000, 2000, 3000, 4000];

        // Act
        int k = _solution.RemoveDuplicates(nums);

        // Assert
        Assert.AreEqual(4, k);
        CollectionAssert.AreEqual(expectedFirstK, nums.Take(k).ToArray());
    }
}
