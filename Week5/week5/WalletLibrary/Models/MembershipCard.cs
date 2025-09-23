using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public class MembershipCard : Card
    {
        protected string _organization;

        public MembershipCard(string organization, DateOnly expirationDate, string accoundID)
        {
            _organization = organization;
            _expirationDate = expirationDate;
            _accountID = accoundID;
        }

        public override decimal? GetBalance()
        {
            throw new NotImplementedException("This is a membership card, there is no balance.");
        }

        public override bool MakePayment(decimal amount) 
        {
            return false;
        }

        public override Purpose GetPurpose()
        {
            return Purpose.Access;
        }


    }

    public class GymMembershipCard : MembershipCard
    {
        public GymMembershipCard(string organization, DateOnly expirationDate, string accoundID, Gyms gyms) : base(organization,expirationDate,accoundID)
        {
            
        }

        internal Gyms _gyms;
        public Gyms Gyms => _gyms;
    }

    public enum Gyms
    {

    }
}
