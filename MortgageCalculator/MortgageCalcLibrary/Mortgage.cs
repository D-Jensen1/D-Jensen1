using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalcLibrary
{
    public record class Mortgage(decimal PrincipalAmount, decimal InterestRatePct, int DurationYears)
    {
        //field
        private List<Payment>? _monthlyPayments;
        private List<Payment>? _biWeeklyPayments;
        private double? _monthlyPayment = null;
        private double? _biweeklyPayment = null;
        private MortgageID? _ID;

        //property
        public List<Payment> MonthlyPayments => _monthlyPayments ?? CalculateMonthlyPayments();
        public List<Payment> BiWeeklyPayments => _biWeeklyPayments ?? CalculateBiWeeklyPayments();
        public double? MonthlyPaymentAmount => _monthlyPayment ?? GetMonthlyPaymentAmount();
        public double? BiWeeklyPaymentAmount => _biweeklyPayment ?? GetBiWeeklyPaymentAmount();
        public MortgageID ID => _ID ?? GetID();

        //method
        private List<Payment> CalculateMonthlyPayments()
        {
            _monthlyPayments = new List<Payment>();

            decimal[,] monthlyTable = Calculations.MonthlyAmortization(this.PrincipalAmount, this.InterestRatePct, this.DurationYears);
            for (int i = 0; i < monthlyTable.GetLength(0); i++)
            {
                _monthlyPayments.Add(new Payment(
                    //i,
                    (int)monthlyTable[i, 0],
                    monthlyTable[i, 1],
                    monthlyTable[i, 2],
                    monthlyTable[i, 3],
                    monthlyTable[i, 4],
                    monthlyTable[i, 5]
                    ));
            }
            return _monthlyPayments;
        }

        private List<Payment> CalculateBiWeeklyPayments()
        {
            _biWeeklyPayments = new List<Payment>();
            decimal[,] biweeklyTable = Calculations.BiWeeklyAmortization(this.PrincipalAmount, this.InterestRatePct, this.DurationYears);
            for (int i = 0; i < biweeklyTable.GetLength(0); i++)
            {
                _biWeeklyPayments.Add(new Payment(
                    //i,
                    (int)biweeklyTable[i, 0],
                    biweeklyTable[i, 1],
                    biweeklyTable[i, 2],
                    biweeklyTable[i, 3],
                    biweeklyTable[i, 4],
                    biweeklyTable[i, 5]
                    ));
            }
            return _biWeeklyPayments;
        }

        private double? GetMonthlyPaymentAmount()
        {
            _monthlyPayment = Calculations.CalculateMonthlyPayment(this.PrincipalAmount, this.InterestRatePct, this.DurationYears);
            return _monthlyPayment;
        }
        private double? GetBiWeeklyPaymentAmount()
        {
            _biweeklyPayment = Calculations.BiWeeklyInterest(this.PrincipalAmount, this.InterestRatePct, this.DurationYears);
            return _biweeklyPayment;
        }

        private MortgageID GetID()
        {
            _ID = new MortgageID(this.PrincipalAmount, this.InterestRatePct, this.DurationYears);
            return _ID;
        }
    }
}
