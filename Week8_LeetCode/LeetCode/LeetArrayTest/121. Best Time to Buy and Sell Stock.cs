using System;
using System.Linq;

namespace LeetArrayTest;

class _121
{
    public int MaxProfit(int[] prices)
    {
        int minPrice = prices[0];

        int maxProfit = 0;


        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] < minPrice)
            {
                minPrice = prices[i];
            }
            if ((prices[i] - minPrice) > maxProfit)
            {
                maxProfit = prices[i] - minPrice;
            }
        }
        return maxProfit;
    }

    public int MaxProfit2(int[] prices)
    {
        int buyDay = 0;
        int sellDay = 0;
        int maxProfit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] < prices[buyDay])
            {
                buyDay = i;
                sellDay = i; // reset sellDay to buyDay

            }
            
            else if (prices[i] > prices[sellDay])
            {
                sellDay = i;
                int currentProfit = prices[sellDay] - prices[buyDay];
                if (currentProfit > maxProfit)
                {
                    maxProfit = currentProfit;
                }
            }
            
        }
        return maxProfit;
    }
    
}

[TestClass]
public class _121Test
{
    private _121 _solution = new _121();

    [TestMethod]
    public void MaxProfit_BasicCase_ReturnsCorrectProfit()
    {
        // Arrange
        int[] prices = [7, 1, 5, 3, 6, 4];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(5, result); // Buy at 1, sell at 6
    }

    [TestMethod]
    public void MaxProfit_NoProfit_ReturnsZero()
    {
        // Arrange
        int[] prices = [7, 6, 4, 3, 1];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(0, result); // Prices only decrease
    }


    [TestMethod]
    public void MaxProfit_SinglePrice_ReturnsZero()
    {
        // Arrange
        int[] prices = [1];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(0, result); // Cannot buy and sell on same day
    }

    [TestMethod]
    public void MaxProfit_TwoPricesIncreasing_ReturnsProfit()
    {
        // Arrange
        int[] prices = [1, 5];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(4, result); // Buy at 1, sell at 5
    }

    [TestMethod]
    public void MaxProfit_TwoPricesDecreasing_ReturnsZero()
    {
        // Arrange
        int[] prices = [5, 1];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(0, result); // Cannot make profit
    }

    [TestMethod]
    public void MaxProfit_AllSamePrices_ReturnsZero()
    {
        // Arrange
        int[] prices = [3, 3, 3, 3, 3];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(0, result); // No profit when prices are same
    }

    [TestMethod]
    public void MaxProfit_MinimumAtEnd_ReturnsZero()
    {
        // Arrange
        int[] prices = [2, 4, 1];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(2, result); // Buy at 2, sell at 4
    }

    [TestMethod]
    public void MaxProfit_MaximumAtBeginning_ReturnsZero()
    {
        // Arrange
        int[] prices = [5, 1, 2, 3];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(2, result); // Buy at 1, sell at 3
    }

    [TestMethod]
    public void MaxProfit_LargeProfit_WorksCorrectly()
    {
        // Arrange
        int[] prices = [1, 100];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(99, result); // Buy at 1, sell at 100
    }

    [TestMethod]
    public void MaxProfit_MultipleHighs_FindsBestProfit()
    {
        // Arrange
        int[] prices = [3, 3, 5, 0, 0, 3, 1, 4];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(4, result); // Buy at 0, sell at 4
    }

    [TestMethod]
    public void MaxProfit_ComplexPattern_FindsOptimalProfit()
    {
        // Arrange
        int[] prices = [2, 1, 2, 1, 0, 1, 2];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(2, result); // Buy at 0, sell at 2
    }

    // Edge cases
    [TestMethod]
    public void MaxProfit_VeryLargePrices_HandlesCorrectly()
    {
        // Arrange
        int[] prices = [1000000, 1, 1000000];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(999999, result); // Buy at 1, sell at 1000000
    }

    [TestMethod]
    public void MaxProfit_ZeroPrices_HandlesCorrectly()
    {
        // Arrange
        int[] prices = [0, 1, 0, 1];
        
        // Act
        int result = _solution.MaxProfit(prices);
        
        // Assert
        Assert.AreEqual(1, result); // Buy at 0, sell at 1
    }
}
