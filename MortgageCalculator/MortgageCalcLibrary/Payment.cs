using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalcLibrary
{
    public record class Payment
    (
        int PaymentNumber,
        decimal PrincipalAmount,
        decimal InterestAmount,
        decimal RemainingPrincipal,
        decimal AccumulatedInterestPaid,
        decimal AccumulatedPrincipalPaid
    )
    { }

    public record class MortgageID
    (
        decimal PrincipalAmount,
        decimal InterestRateInPercent,
        int DurationYears
    ) : IComparable<MortgageID>
    {
        public int CompareTo(MortgageID? other)
        {
            return this.PrincipalAmount.CompareTo(other!.PrincipalAmount);
        }
    }
}
