using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public abstract class Card
    {
        protected Wallet? _wallet;
        protected DateOnly ExpirationDate;
        protected decimal? _balance;
        protected string? _accountID;
        public virtual bool Expired => DateOnly.FromDateTime(DateTime.Now) > ExpirationDate;
        public virtual string? AccoundID => _accountID;

        // concrete
        public void AddToWallet(Wallet w)
        {
            w.AddCard(this);
            this._wallet = w;
        }

        // abstract
        public abstract bool MakePayment(decimal amountToPay);
        public abstract decimal GetBalance();
        public abstract Purpose GetPurpose();
    }

    public enum Purpose
    {
        Payment,
        Transit,
        Access,
        Cash
    }
    // Team One
    public class CreditCard : Card
    {

    }

    public class BankCard : Card
    {

    }

    //Team Two
    public class DebitCard : Card
    {

    }

    public class GiftCard : Card
    {
        public override bool Expired => DateOnly.FromDateTime(DateTime.Now) > ExpirationDate.AddDays(30);

    }

    // Team Three
    /// <summary>
    /// Must derive another class i.e credit card has rewards card subclass
    /// Transit card may have unlimited transit card (no limit until it expires)
    /// Membership with higher tier or perks
    /// </summary>
    public class TransitCard : Card
    {
        public override bool Expired => DateOnly.FromDateTime(DateTime.Now) > ExpirationDate.AddDays(365);

    }

    public class MembershipCard : Card
    {
        public override decimal GetBalance()
        {
            throw new NotImplementedException("This is a membership card, there is no balance.");
        }

    }
}
