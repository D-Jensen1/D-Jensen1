using System.Diagnostics;
using WalletLibrary.Models;

namespace LearnClassModeling;

[TestClass]
public class WalletICollectionOfBillTest
{
    [TestMethod]
    public void TestIEnumerable()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        int actual = 0;
        foreach (var item in tester)
        {
            actual++;
        }
        foreach (object item in tester)
        {
            Assert.IsTrue(item is Bill);
            if (item is Bill aBill)
            {
                Debug.WriteLine(aBill);
            }
        }
        Assert.AreEqual(3, actual);
    }

    [TestMethod]
    public void TestCountMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        Assert.AreEqual(3, tester.Count);
    }

    [TestMethod]
    public void TestIsReadOnlyMember()
    {
        ICollection<Bill> tester = new WalletBill();
        Assert.AreEqual(true, tester.IsReadOnly);
    }

    [TestMethod]
    public void TestAddMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        Assert.AreEqual(3, tester.Count);
    }

    [TestMethod]
    public void TestContainsMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
       
        Assert.IsTrue(tester.Contains(Bill.Fifty));
        Assert.IsFalse(tester.Contains(Bill.Hundred));

    }

    [TestMethod]
    public void TestClearMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);

        Assert.AreEqual(3, tester.Count);
        Assert.ThrowsException<Exception>(() => tester.Clear());
    }

    [TestMethod]
    public void TestCopyMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.One);
        tester.Add(Bill.Two);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);
        tester.Add(Bill.Fifty);

        Bill[] destination = new Bill[5];

        tester.CopyTo(destination, 0);
        Assert.IsTrue(destination[0] == Bill.One);
        Assert.IsTrue(destination[1] == Bill.Two);
        Assert.IsTrue(destination[2] == Bill.Fifty);
        Assert.IsTrue(destination[4] == Bill.Fifty);
    }

    [TestMethod]
    public void TestRemoveMember()
    {
        ICollection<Bill> tester = new WalletBill();
        tester.Add(Bill.One);
        tester.Add(Bill.Two);
        tester.Add(Bill.Fifty);


        tester.Remove(Bill.One);
        Assert.IsTrue(tester.Count == 2);
        Assert.IsFalse(tester.Remove(Bill.Ten));
        tester.Remove(Bill.Two);
        Assert.IsTrue(tester.Count == 1);
        tester.Remove(Bill.Fifty);
        Assert.IsTrue(tester.Count == 0);
        Assert.IsFalse(tester.Remove(Bill.Ten));

    }
}
