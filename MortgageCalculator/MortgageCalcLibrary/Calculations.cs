using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MortgageCalcLibrary
{
    public class Calculations
    {
        public static double CalculateMonthlyPayment(decimal principal, decimal interestRate, int yearTerm)
        {
            double n = (double)yearTerm * 12;
            double i = (double)(interestRate / 100) / 12;

            double top = i * Math.Pow((1 + i), n);
            double bottom = Math.Pow((1 + i), n) - 1;
            decimal monthlyPayment = principal * (decimal)(top / bottom);

            double doubleMonthlyPayment = Decimal.ToDouble(monthlyPayment);
            return doubleMonthlyPayment;
        }

        public static decimal[] CalculateMonthlyBreakdown(decimal principal, decimal interestRate, int paymentNumber, decimal monthlyPayment)
        {
            decimal monthlyInterestRate = (interestRate / 100) / 12;
            decimal interestPaid = 0m;
            decimal principalLeftover = principal;
            decimal principalPaid = 0m;
            for (int i = 0; i < paymentNumber; i++)
            {
                interestPaid = principalLeftover * monthlyInterestRate;
                principalPaid = monthlyPayment - interestPaid;
                principalLeftover -= principalPaid;
            }


            return [interestPaid, principalPaid, principalLeftover];

        }
    
        public static decimal TotalInterest(decimal principal, decimal interestRate, int yearTerm)
        {
            decimal monthlyPayments = (decimal)CalculateMonthlyPayment(principal, interestRate, yearTerm);
            return Math.Round((monthlyPayments * (yearTerm * 12)) - principal,2);
        }

        public static double BiWeeklyInterest(decimal principal, decimal interestRate, int yearTerm)
        {
            decimal biWeeklyInterestRate = (interestRate / 100 / 26);
            decimal biWeeklyPayment = (decimal) CalculateMonthlyPayment(principal, interestRate, yearTerm) / 2;
            decimal totalInterest = 0m;


            while (principal > 0)
            {
                decimal biWeeklyInterest = principal * biWeeklyInterestRate;
                totalInterest += biWeeklyInterest;
                principal += biWeeklyInterest;

                principal -= biWeeklyPayment;
                if (principal < 0) principal = 0; // Avoid negative balance

            }
            double totalInterestDouble = Decimal.ToDouble(totalInterest);
            return Math.Round(totalInterestDouble,2);
        }

        public static decimal[,] MonthlyAmortization(decimal principal, decimal interestRate, int yearTerm)
        {
            int totalNumberOfPayments = yearTerm * 12;
            int paymentNumber = 0;
            decimal monthlyPayment = (decimal)CalculateMonthlyPayment(principal, interestRate, yearTerm);

            decimal monthlyInterestRate = (interestRate / 100) / 12;
            decimal interestPaid = 0m;
            decimal accumulatedInterest = 0;

            decimal principalLeftover = principal;
            decimal principalPaid = 0m;
            decimal accumulatedPrincipal = 0m;

            decimal[,] table = new decimal[totalNumberOfPayments, 6];

            //object[,] table = new object[totalNumberOfPayments, 5];
            //DateTime today = DateTime.Now;
            //DateTime paymentDate = new(today.Year, today.Month, 1);

            for (int i = 0; i < totalNumberOfPayments; i++)
            {
                paymentNumber++;
                //paymentDate = paymentDate.AddMonths(1);

                interestPaid = principalLeftover * monthlyInterestRate;
                accumulatedInterest += interestPaid;

                principalPaid = monthlyPayment - interestPaid;
                principalLeftover -= principalPaid;
                accumulatedPrincipal += principalPaid;

                //table[i, 0] = paymentDate.ToString("MMM yyyy"); // Payment month/year
                table[i, 0] = paymentNumber; // Payment month/year
                table[i, 1] = principalPaid; // Principal paid
                table[i, 2] = interestPaid; // Interest paid
                table[i, 3] = principalLeftover; // Principal left
                table[i, 4] = accumulatedInterest; // Interest accumulated
                table[i, 5] = accumulatedPrincipal; // Interest accumulated

                principal -= monthlyPayment;
            }
            return table;
        }
        public static decimal[,] BiWeeklyAmortization(decimal principal, decimal interestRate, int yearTerm)
        {
            int paymentNumber = 0;
            int monthNumber = 0;
            decimal accumulatedInterest = 0;

            decimal biWeeklyInterestPaid = 0m;
            decimal monthlyInterestPaid = 0m;

            decimal biWeeklyPrincipalPaid = 0m;
            decimal monthlyPrincipalPaid = 0m;
            decimal principalRemaining = principal;
            decimal accumulatedPrincipal = 0m;

            decimal biWeeklyInterestRate = (interestRate / 100 / 26);
            decimal biWeeklyPayment = (decimal)CalculateMonthlyPayment(principal, interestRate, yearTerm) / 2;

            var monthlyPaymentsList = new List<decimal[]>();

            //DateTime today = DateTime.Now;
            //DateTime paymentDate = new(today.Year,today.Month,1);

            while (principalRemaining > 0)
            {
                paymentNumber++;
                
                biWeeklyInterestPaid = principalRemaining * biWeeklyInterestRate;
                monthlyInterestPaid += biWeeklyInterestPaid;
                accumulatedInterest += biWeeklyInterestPaid;

                biWeeklyPrincipalPaid = biWeeklyPayment - biWeeklyInterestPaid;
                monthlyPrincipalPaid += biWeeklyPrincipalPaid;
                principalRemaining -= biWeeklyPrincipalPaid;
                accumulatedPrincipal += biWeeklyPrincipalPaid;
                if (principalRemaining < 0) principalRemaining = 0; // Avoid negative balance

                if (paymentNumber % 2 == 0 || principalRemaining <= 0)
                {
                    //paymentDate = paymentDate.AddMonths(1);
                    monthNumber++;

                    monthlyPaymentsList.Add(
                        [
                            //paymentDate.ToString("MMM yyyy"),
                            monthNumber,
                            monthlyPrincipalPaid,
                            monthlyInterestPaid,
                            principalRemaining,
                            accumulatedInterest,
                            accumulatedPrincipal
                        ]);
                    monthlyPrincipalPaid = 0;
                    monthlyInterestPaid = 0;
                }
                
            }
            
            decimal[,] table = new decimal[monthlyPaymentsList.Count, 5];
            for (int i = 0; i < monthlyPaymentsList.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    table[i, j] = monthlyPaymentsList[i][j];
                }
            }

            return table;
        }
    }
}
