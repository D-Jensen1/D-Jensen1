using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetArrayTest;

class _169
{
    public int MajorityElement(int[] nums)
    {
        Dictionary<int, int> numsDict = new Dictionary<int, int>();
        int majorityElement = nums[0];
        foreach (var num in nums)
        {
            if (!numsDict.TryAdd(num,1))
            {
                numsDict[num] += 1;
                if (numsDict[num] >= Math.Ceiling((decimal)nums.Count()/2))
                {
                    majorityElement = num;
                    break;
                }
            }
        }
        return majorityElement;
    }

    public int MajorityElement2(int[] nums)
    {
        return nums.GroupBy(x => x)
                  .OrderByDescending(g => g.Count())
                  .First()
                  .Key;
    }

    public int MajorityElement3(int[] nums)
    {
        int count = 0;
        int candidate = 0;

        foreach (int num in nums)
        {
            if (count == 0)
                candidate = num;
            
            if (num == candidate)
                count++;

            else
                count--;
        }
        return candidate;
    }
}

[TestClass]
public class _169Test
{
    private _169 _solution = new _169();

    [TestMethod]
    public void MajorityElement_BasicCase_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {3, 2, 3};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void MajorityElement_LongerArray_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {2, 2, 1, 1, 1, 2, 2};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void MajorityElement_SingleElement_ReturnsThatElement()
    {
        // Arrange
        int[] nums = {1};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void MajorityElement_TwoElementsSame_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {1, 1};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void MajorityElement_AllElementsSame_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {5, 5, 5, 5, 5};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void MajorityElement_NegativeNumbers_WorksCorrectly()
    {
        // Arrange
        int[] nums = {-1, -1, -1, 2, 2};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void MajorityElement_MajorityAtBeginning_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {1, 1, 1, 2, 3};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void MajorityElement_MajorityAtEnd_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {1, 2, 3, 3, 3};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void MajorityElement_AlternatingPattern_ReturnsCorrectElement()
    {
        // Arrange
        int[] nums = {1, 2, 1, 2, 1};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void MajorityElement_LargeNumbers_WorksCorrectly()
    {
        // Arrange
        int[] nums = {1000000, 1000000, 1000000, 999999, 999999};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1000000, result);
    }


    [TestMethod]
    public void MajorityElement_ExactlyHalfPlusOne_ReturnsCorrectElement()
    {
        // Arrange - Array of size 7, majority element appears 4 times (more than 7/2 = 3.5)
        int[] nums = {1, 1, 1, 1, 2, 2, 2};
        
        // Act
        int result = _solution.MajorityElement(nums);
        
        // Assert
        Assert.AreEqual(1, result);
    }
}
