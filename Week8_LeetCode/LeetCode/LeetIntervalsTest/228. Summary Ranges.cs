namespace LeetIntervalsTest;

[TestClass]
public class _228
{
    public IList<string> SummaryRanges(int[] nums)
    {
        IList<string> resultList = [];

        if (nums.Length == 0) return resultList;
        if (nums.Length == 1)
        {
            resultList.Add(nums[0].ToString());
            return resultList; 
        }

        List<int> group = [];
        int temp = nums[0];

        for (int i = 0; i < nums.Length; i++)
        { 
            if (nums[i] - i == temp) 
            {
                group.Add(nums[i]);
            }
            else if (group.Count == 1)
            {
                resultList.Add(group[0].ToString());
                group = [];
                temp = nums[i] - i;
                if (i != nums.Length) i--;
            }
            else
            {
                resultList.Add($"{group.First()}->{group.Last()}");
                group = [];
                temp = nums[i] - i;
                if (i != nums.Length) i--;
            }

            if (i == nums.Length - 1)
            {
                if (group.Count == 1)
                {
                    resultList.Add(group[0].ToString());
                }
                else
                {
                    resultList.Add($"{group.First()}->{group.Last()}");
                }
            }
        }
        return resultList;
    }

    public IList<string> SummaryRanges2(int[] nums)
    {
        List<string> result = new List<string>();
        
        if (nums.Length == 0)
            return result;
        
        int start = 0; // Start index of current range
        
        for (int i = 0; i < nums.Length; i++)
        {
            // Check if we're at the end or if the next number breaks the sequence
            if (i == nums.Length - 1 || nums[i + 1] != nums[i] + 1)
            {
                // End of a range found
                if (start == i)
                {
                    // Single number range
                    result.Add(nums[start].ToString());
                }
                else
                {
                    // Multi-number range
                    result.Add($"{nums[start]}->{nums[i]}");
                }
                
                // Start new range from next position
                start = i + 1;
            }
        }
        
        return result;
    }

    public IList<string> SummaryRanges3(int[] nums)
    {
        return nums.GroupBy((n) => n - Array.IndexOf(nums, n))
                   .Select(g => g.Count() == 1 ? g.First()
                   .ToString() : $"{g.First()}->{g.Last()}").ToList();
    }
     
    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: nums = [0,1,2,4,5,7] -> ["0->2","4->5","7"]
        int[] nums = { 0, 1, 2, 4, 5, 7 };
        IList<string> expected = new List<string> { "0->2", "4->5", "7" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod2_BasicExample2()
    {
        // Test case: nums = [0,2,3,4,6,8,9] -> ["0","2->4","6","8->9"]
        int[] nums = { 0, 2, 3, 4, 6, 8, 9 };
        IList<string> expected = new List<string> { "0", "2->4", "6", "8->9" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod3_EmptyArray()
    {
        // Test case: nums = [] -> []
        int[] nums = { };
        IList<string> expected = new List<string>();
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod4_SingleElement()
    {
        // Test case: nums = [1] -> ["1"]
        int[] nums = { 1 };
        IList<string> expected = new List<string> { "1" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod5_AllConsecutive()
    {
        // Test case: nums = [1,2,3,4,5] -> ["1->5"]
        int[] nums = { 1, 2, 3, 4, 5 };
        IList<string> expected = new List<string> { "1->5" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod6_NoConsecutive()
    {
        // Test case: nums = [1,3,5,7,9] -> ["1","3","5","7","9"]
        int[] nums = { 1, 3, 5, 7, 9 };
        IList<string> expected = new List<string> { "1", "3", "5", "7", "9" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod7_TwoElements()
    {
        // Test case: nums = [1,2] -> ["1->2"]
        int[] nums = { 1, 2 };
        IList<string> expected = new List<string> { "1->2" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod8_TwoElementsNotConsecutive()
    {
        // Test case: nums = [1,3] -> ["1","3"]
        int[] nums = { 1, 3 };
        IList<string> expected = new List<string> { "1", "3" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod9_NegativeNumbers()
    {
        // Test case: nums = [-3,-2,-1,1,2,3] -> ["-3->-1","1->3"]
        int[] nums = { -3, -2, -1, 1, 2, 3 };
        IList<string> expected = new List<string> { "-3->-1", "1->3" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod10_MixedSingleAndRanges()
    {
        // Test case: nums = [1,3,4,5,7] -> ["1","3->5","7"]
        int[] nums = { 1, 3, 4, 5, 7 };
        IList<string> expected = new List<string> { "1", "3->5", "7" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod11_ZeroIncluded()
    {
        // Test case: nums = [-1,0,1] -> ["-1->1"]
        int[] nums = { -1, 0, 1 };
        IList<string> expected = new List<string> { "-1->1" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod12_LargeNumbers()
    {
        // Test case: nums = [100,101,103] -> ["100->101","103"]
        int[] nums = { 100, 101, 103 };
        IList<string> expected = new List<string> { "100->101", "103" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod13_SingleNegative()
    {
        // Test case: nums = [-5] -> ["-5"]
        int[] nums = { -5 };
        IList<string> expected = new List<string> { "-5" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod14_OnlyZero()
    {
        // Test case: nums = [0] -> ["0"]
        int[] nums = { 0 };
        IList<string> expected = new List<string> { "0" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestMethod15_LargeGaps()
    {
        // Test case: nums = [1,10,20,30] -> ["1","10","20","30"]
        int[] nums = { 1, 10, 20, 30 };
        IList<string> expected = new List<string> { "1", "10", "20", "30" };
        IList<string> actual = SummaryRanges(nums);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }
}
