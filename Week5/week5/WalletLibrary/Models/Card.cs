using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public abstract class Card
    {
        protected WalletCard? _walletCard;
        protected DateOnly _expirationDate;
        protected decimal? _balance;
        protected string? _accountID;

        public virtual bool Expired => DateOnly.FromDateTime(DateTime.Now) > _expirationDate;
        public virtual string? AccountID => _accountID;
        public WalletCard? Wallet => _walletCard;
        //concrete
        public void AddToWallet(WalletCard w)
        {
            w.AddCard(this);
            this._walletCard = w;
        }


        //abstract
        public abstract bool MakePayment(decimal amountToPay);
        public abstract decimal? GetBalance();
        public abstract Purpose GetPurpose();
    }
    [Flags]
    public enum Purpose
    {
        Payment = 1,
        Transit = 2,
        Access = 4,
        Cash = 8
    }

    //Team One
    public class CreditCard : Card
    {
        public int CreditLimit { get; init; }
        public CreditCard(string accountID, int creditLimit, DateOnly expirationDate)
        {
            this._accountID = accountID;
            this._expirationDate = expirationDate;
            CreditLimit = creditLimit;
        }

        public override decimal? GetBalance()
        {
            return this._balance;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Payment;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            if (amountToPay <= 0) return false;
            if (this._balance + amountToPay > CreditLimit) return false;

            this._balance += amountToPay;
            return true;
        }

    }
    public class BankCard : Card
    {
        public BankCard(string accountID, DateOnly expirationDate, decimal initialBalance = 0)
        {
            _accountID = accountID;
            _expirationDate = expirationDate;
            _balance = initialBalance;
        }

        public override decimal? GetBalance()
        {
            return _balance;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Payment | Purpose.Cash;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            if (amountToPay <= 0) return false;
            if (_balance < amountToPay) return false;

            _balance -= amountToPay;
            return true;
        }
    }

    //Team Two
    public class DebitCard : Card
    {
        public DebitCard(string accountID, DateOnly expirationDate, decimal initialBalance = 0)
        {
            _accountID = accountID;
            _expirationDate = expirationDate;
            _balance = initialBalance;
        }

        public override decimal? GetBalance()
        {
            return _balance;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Payment | Purpose.Cash;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            if (amountToPay <= 0) return false;
            if (_balance < amountToPay) return false;

            _balance -= amountToPay;
            return true;
        }
    }
    public class GiftCard : Card
    {
        public GiftCard(decimal initialBalance, DateOnly expirationDate)
        {
            _balance = initialBalance;
            _expirationDate = expirationDate;
        }

        public override bool Expired => DateOnly.FromDateTime(DateTime.Now) > _expirationDate.AddDays(30);

        public override decimal? GetBalance()
        {
            return _balance;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Payment;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            if (amountToPay <= 0) return false;
            if (_balance < amountToPay) return false;
            if (Expired) return false;

            _balance -= amountToPay;
            return true;
        }
    }

    //Team Three
/*
    public class TransitCard : Card
    {
        public TransitCard(string accountID, DateOnly expirationDate, decimal initialBalance = 0)
        {
            _accountID = accountID;
            _expirationDate = expirationDate;
            _balance = initialBalance;
        }
        public override bool Expired => DateOnly.FromDateTime(DateTime.Now) > _expirationDate.AddYears(5);

        public override decimal? GetBalance()
        {
            return _balance;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Transit;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            if (amountToPay <= 0) return false;
            if (_balance < amountToPay) return false;

            _balance -= amountToPay;
            return true;
        }
    }
    public class MembershipCard : Card
    {
        public MembershipCard(string accountID, DateOnly expirationDate)
        {
            _accountID = accountID;
            _expirationDate = expirationDate;
        }
        public override decimal? GetBalance()
        {
            return null;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Access;
        }

        public override bool MakePayment(decimal amountToPay)
        {
            return false;
        }
    }
*/
}
