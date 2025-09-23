using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public class TransitCard : Card
    {
        public TransitCard(DateOnly expirationDate, string accountID, decimal balance)
        {
            _expirationDate = expirationDate;
            _accountID = accountID;
            _balance = balance;
        }

        public override bool Expired => DateOnly.FromDateTime(DateTime.Now) > _expirationDate.AddDays(365);
        
        public override decimal? GetBalance()
        {
            return 0;
        }

        public override bool MakePayment(decimal amount)
        {
            return true;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Transit;
        }
    }
}
