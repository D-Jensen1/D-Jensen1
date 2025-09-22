using LearnClassModeling.Models;

namespace LearnClassModeling
{
    [TestClass]
    public sealed class BillTest
    {
        [TestMethod]
        [DataRow(Denomination.One, 1)]
        [DataRow(Denomination.Two, 2)]
        [DataRow(Denomination.Five, 5)]
        [DataRow(Denomination.Ten, 10)]
        [DataRow(Denomination.Twenty, 20)]
        [DataRow(Denomination.Fifty, 50)]
        [DataRow(Denomination.Hundred, 100)]
        public void TestBillCreation(Denomination _denom, int value)
        {
            Bill aBill = new(_denom);
            Assert.AreEqual(value, aBill.Amount);
        }

        [TestMethod]
        public void TestBillPortrait()
        {
            Bill portrait = new Bill(Denomination.Hundred);
            Assert.AreEqual("Benjamin Franklin", portrait.Portrait);
        }

        [TestMethod]
        public void TestBillForComparison()
        {
            Bill one = new Bill(Denomination.One);
            Bill five = new Bill(Denomination.Five);
            Bill ten = new Bill(Denomination.Ten);
            // Test for IComparable so List.Sort works
            List<Bill> bills = [five, one, ten];
            bills.Sort();

            Assert.AreSame(one, bills[0]);
            Assert.AreSame(five, bills[1]);
            Assert.AreSame(ten, bills[2]);

            // Test for "Operator Overloading" boolean operator
            // bool < >, <= >=, ==, !=
            //whatever + - * / 
            Assert.IsTrue(five > one);
            Assert.IsTrue(five < ten);
            Assert.IsTrue(ten == ten);

            Bill anotherOne = new Bill(Denomination.One);
            // bill should be fungible, meaning one and anotherOne should == true
            Assert.IsTrue(one == anotherOne);
            Assert.IsTrue(one <= anotherOne);
            Assert.IsTrue(one >= anotherOne);

            // HashSet uses GetHashcode internally, so make sure it works correctly by adding 2 ones and 2 fives
            HashSet<Bill> billSet= [one, anotherOne, five, new Bill(Denomination.Five)];
            Assert.AreEqual(2, billSet.Count);
            
            // Overloaded * operator
            List<Bill> multipleResult = five * 10;
            Assert.AreEqual(10, multipleResult.Count);
            Assert.AreEqual(50, multipleResult.Sum(bill => bill.Amount));

            // Overloaded + operator
            List<Bill> additionResult = five + one;
            Assert.AreEqual(2, additionResult.Count);
            Assert.AreEqual(five, additionResult[0]);
            Assert.AreEqual(one, additionResult[1]);

            additionResult = additionResult + ten; // Overload List<Bill> + Bill
            Assert.AreEqual(3, additionResult.Count);
            Assert.AreEqual(ten, additionResult[2]);

            // Overloaded + operator - with Merge Bill magic -- credit AB
            List<Bill> merge = ten + ten;
            Assert.AreEqual(1, merge.Count);
            Assert.AreEqual(new Bill(Denomination.Twenty), merge[0]);
        }
    }
}
