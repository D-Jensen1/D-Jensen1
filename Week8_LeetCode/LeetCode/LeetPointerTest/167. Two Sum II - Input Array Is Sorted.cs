namespace LeetPointerTest;

[TestClass]
public class _167
{
    public int[] TwoSum(int[] numbers, int target)
    {
        int ptrLeft = 0;
        int ptrRight = numbers.Length - 1;

        while (ptrLeft < ptrRight)
        {
            int currentSum = numbers[ptrLeft] + numbers[ptrRight];

            if (currentSum > target)
            {
                ptrRight--;
            }
            else if (currentSum < target)
            {
                ptrLeft++;
            }
            else
            {
                return [ptrLeft + 1, ptrRight + 1];
            }
        }
        return [];
    }

    public int[] TwoSum2(int[] numbers, int target)
    {
        int pointer = 0;
        int result = 0;

        while (pointer < numbers.Length)
        {
            result = Array.IndexOf(numbers, target - numbers[pointer], pointer + 1);
            if (result == -1)
            {
                pointer++;
            }
            else
            {
                return [pointer + 1, result + 1];
            }
        }
        return [];
    }


    [TestMethod]
    public void TestMethod1_BasicExample()
    {
        // Test case: numbers = [2,7,11,15], target = 9 -> [1,2]
        int[] numbers = { 2, 7, 11, 15 };
        int target = 9;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_SecondExample()
    {
        // Test case: numbers = [2,3,4], target = 6 -> [1,3]
        int[] numbers = { 2, 3, 4 };
        int target = 6;
        int[] expected = { 1, 3 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_ThirdExample()
    {
        // Test case: numbers = [-1,0], target = -1 -> [1,2]
        int[] numbers = { -1, 0 };
        int target = -1;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_AdjacentElements()
    {
        // Test case: numbers = [1,2,3,4], target = 3 -> [1,2]
        int[] numbers = { 1, 2, 3, 4 };
        int target = 3;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_FarApartElements()
    {
        // Test case: numbers = [1,3,5,7,9], target = 10 -> [1,5]
        int[] numbers = { 1, 3, 5, 7, 9 };
        int target = 10;
        int[] expected = { 1, 5 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_NegativeNumbers()
    {
        // Test case: numbers = [-3,-1,2,4], target = 1 -> [2,3]
        int[] numbers = { -3, -2, 2, 4 };
        int target = 1;
        int[] expected = { 1, 4 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod7_AllNegativeNumbers()
    {
        // Test case: numbers = [-5,-3,-1], target = -8 -> [1,2]
        int[] numbers = { -5, -3, -1 };
        int target = -8;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod8_DuplicateNumbers()
    {
        // Test case: numbers = [1,2,2,3], target = 4 -> [2,4]
        int[] numbers = { 5,25,75 };
        int target = 100;
        int[] expected = { 2, 3 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod9_LargeTarget()
    {
        // Test case: numbers = [1,2,3,4,5,6], target = 11 -> [5,6]
        int[] numbers = { 1, 2, 3, 4, 5, 6 };
        int target = 11;
        int[] expected = { 5, 6 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod10_MinimumArray()
    {
        // Test case: numbers = [1,2], target = 3 -> [1,2]
        int[] numbers = { 1, 2 };
        int target = 3;
        int[] expected = { 1, 2 };
        int[] actual = TwoSum(numbers, target);
        CollectionAssert.AreEqual(expected, actual);
    }
}
