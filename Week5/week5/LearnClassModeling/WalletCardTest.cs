using WalletLibrary.Models;

namespace LearnClassModeling;

[TestClass]
public class WalletCardsTest
{
    [TestMethod]
    public void TestAdd5CardsToWalletWithCard()
    {
        //Arrange
        var wallet = new WalletBill();
        var creditCard = new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2)));
        var debitCard = new DebitCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000);
        var giftCard = new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
        var transitCard = new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50);
        var membershipCard = new MembershipCard("Library", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1121");

        //Act
        creditCard.AddToWallet(wallet);
        debitCard.AddToWallet(wallet);
        giftCard.AddToWallet(wallet);
        transitCard.AddToWallet(wallet);
        membershipCard.AddToWallet(wallet);

        //Assert
        Assert.AreEqual(5, wallet.CardCount);
    }

    [TestMethod]
    public void TestAdd5CardsToWalletWithWallet()
    {
        //Arrange
        var wallet = new WalletBill();
        var creditCard = new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2)));
        var debitCard = new DebitCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000);
        var giftCard = new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
        var transitCard = new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50);
        var membershipCard = new MembershipCard("Library", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1121");

        //Act
        wallet.AddCard(creditCard, debitCard, giftCard);
        wallet.AddCard(transitCard);
        wallet.AddCard(membershipCard);

        //Assert
        Assert.AreEqual(5, wallet.CardCount);
    }

    [TestMethod]
    public void TestFalgEnum()
    {
        //Arrange
        var wallet = new WalletBill();
        var creditCard = new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2)));
        var debitCard = new DebitCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000);
        var giftCard = new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
        var transitCard = new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50);
        var membershipCard = new MembershipCard("Library", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1121");

        //Act
        wallet.AddCard(creditCard, debitCard, giftCard);
        wallet.AddCard(transitCard);
        wallet.AddCard(membershipCard);

        Assert.IsTrue(debitCard.GetPurpose().HasFlag(Purpose.Cash));
        Assert.IsTrue(debitCard.GetPurpose().HasFlag(Purpose.Payment));
        Assert.IsFalse(debitCard.GetPurpose().HasFlag(Purpose.Access));
        Assert.IsFalse(debitCard.GetPurpose().HasFlag(Purpose.Transit));

    }

    [TestMethod]
    public void TestFindCardsByPurpose()
    {
        //Arrange
        var wallet = new WalletBill();
        var creditCard = new CreditCard("1234", 5000, DateOnly.FromDateTime(DateTime.Now.AddYears(2)));
        var bankCard = new BankCard("5678", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), 1000);
        var giftCard = new GiftCard(100, DateOnly.FromDateTime(DateTime.Now.AddMonths(6)));
        var transitCard = new TransitCard(DateOnly.FromDateTime(DateTime.Now.AddYears(5)), "9101", 50);
        var membershipCard = new MembershipCard("Library", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), "1121");

        creditCard.AddToWallet(wallet);
        bankCard.AddToWallet(wallet);
        giftCard.AddToWallet(wallet);
        transitCard.AddToWallet(wallet);
        membershipCard.AddToWallet(wallet);

        //Act
        var paymentCards = wallet.FindCardByPurpose(Purpose.Payment);
        var transitCards = wallet.FindCardByPurpose(Purpose.Transit);
        var accessCards = wallet.FindCardByPurpose(Purpose.Access);
        var cashCards = wallet.FindCardByPurpose(Purpose.Cash);

        //Assert
        Assert.AreEqual(3, paymentCards.Count());
        Assert.IsTrue(paymentCards.Any(c => c is CreditCard));
        Assert.IsTrue(paymentCards.Any(c => c is BankCard));
        Assert.IsTrue(paymentCards.Any(c => c is GiftCard));

        Assert.AreEqual(1, transitCards.Count());
        Assert.IsTrue(transitCards.Single() is TransitCard);

        Assert.AreEqual(1, accessCards.Count());
        Assert.IsTrue(accessCards.Single() is MembershipCard);

        Assert.AreEqual(1, cashCards.Count());
        Assert.IsTrue(cashCards.Single() is BankCard);
    }



    public enum UnixPermission
    {
        None = 0,
        Execute = 1,
        Write = 2,
        WriteExecute = Write | Execute,
        Read = 4,
        ReadExecute = Read | Execute,
        ReadWrite = Read | Write,
        All = Read | Write | Execute
    }

}
