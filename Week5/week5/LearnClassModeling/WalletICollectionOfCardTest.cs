using System.Diagnostics;
using WalletLibrary.Models;

namespace LearnClassModeling;

[TestClass]
public class WalletICollectionOfCardTest
{
    [TestMethod]
    public void TestIEnumerable()
    {
        ICollection<Card> tester = InitializeCards();
        int count = 0;

        foreach (var item in tester)
        {
            count++;
        }
        Assert.AreEqual(5, count);
    }

    [TestMethod]
    public void TestCountMember()
    {
        ICollection<Card> tester = InitializeCards();

        Assert.AreEqual(5, tester.Count);
    }

    [TestMethod]
    public void TestIsReadOnlyMember()
    {
        ICollection<Card> tester = InitializeCards();
        Assert.AreEqual(true, tester.IsReadOnly);
    }

    [TestMethod]
    public void TestAddMember()
    {
        ICollection<Card> tester = InitializeCards();
        Assert.AreEqual(5, tester.Count);

        tester.Add(new GiftCard(50, DateOnly.FromDateTime(DateTime.Now.AddMonths(12))));
        Assert.AreEqual(6, tester.Count);

    }

    [TestMethod]
    public void TestContainsMember()
    {
        ICollection<Card> tester = InitializeCards();
       
        Assert.IsTrue(tester.Contains(new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)))));
        Assert.IsFalse(tester.Contains(new MembershipCard("Gym", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1554")));

    }

    [TestMethod]
    public void TestClearMember()
    {
        ICollection<Card> tester = InitializeCards();

        Assert.AreEqual(5, tester.Count);
        Assert.ThrowsException<Exception>(() => tester.Clear());
    }

    [TestMethod]
    public void TestCopyMember()
    {
        ICollection<Card> tester = InitializeCards();

        Card[] destination = new Card[5];

        tester.CopyTo(destination, 0);
        Assert.IsTrue(destination[0] == new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2))));
        Assert.IsTrue(destination[1] == new DebitCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000));
    }

    [TestMethod]
    public void TestRemoveMember()
    {
        ICollection<Card> tester = InitializeCards();


        tester.Remove(new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2))));
        Assert.IsTrue(tester.Count == 4);
        Assert.IsFalse(tester.Remove(new MembershipCard("Gym", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1554")));
        tester.Remove(new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50));
        Assert.IsTrue(tester.Count == 3);
    }

    private Wallet InitializeCards()
    {
        var creditCard = new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2)));
        var debitCard = new DebitCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000);
        var giftCard = new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
        var transitCard = new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50);
        var membershipCard = new MembershipCard("Library", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1121");
        var wallet = new Wallet([creditCard,debitCard,giftCard,transitCard,membershipCard]);

        return wallet;
    }
}
