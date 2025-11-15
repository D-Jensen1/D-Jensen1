using MortgageCalcLibrary;

namespace MortgageProjectTests

{
    [TestClass]
    public sealed class MortgageCalcTests
    {
        [TestMethod]
        public void CheckMonthlyPayment()
        {
            decimal homeValue = 400_000m;
            decimal interestRate = 6.55m;
            decimal downPayment = 25000m;
            int loanTermYears = 30;
            decimal principal = homeValue - downPayment;

            double expected = 2382.60;
            double tolerance = 0.01;

            double testMonthlyPayment = Calculations.CalculateMonthlyPayment(principal, interestRate, loanTermYears);

            Assert.IsTrue(Math.Abs(testMonthlyPayment - expected) <= tolerance);
        }
        

        [DataRow(1, 2046.87, 335.72, 374664.28)]
        [DataRow(5, 2039.48, 343.11, 373302.95)]
        [DataRow(359, 25.80, 2356.80, 2369.67)]
        [TestMethod]
        public void CheckPaymentBreakdown(int paymentNumber, double expectedInterest, double expectedPrincipal, double expectedBalance)
        {
            decimal homeValue = 400_000m;
            decimal interestRate = 6.55m;
            decimal downPayment = 25000m;
            int loanTermYears = 30;
            decimal principal = homeValue - downPayment;
            decimal monthlyPayment = (decimal)Calculations.CalculateMonthlyPayment(principal, interestRate, loanTermYears);
            var monthlyBreakdown = Calculations.CalculateMonthlyBreakdown(principal, interestRate,paymentNumber, monthlyPayment);


            Assert.AreEqual((decimal)expectedInterest, Math.Round(monthlyBreakdown[0],2));
            Assert.AreEqual((decimal)expectedPrincipal, Math.Round(monthlyBreakdown[1], 2));
            Assert.AreEqual((decimal)expectedBalance, Math.Round(monthlyBreakdown[2], 2));
        }

        [TestMethod]
        public void GetTotalInterest()
        {
            decimal homeValue = 400_000m;
            decimal downPayment = 25000m;
            decimal principal = homeValue - downPayment;
            int loanTermYears = 30;
            decimal interestRate = 6.55m;

            decimal expectedTotalInterest = 482_735.80m;

            decimal totalInterest = Calculations.TotalInterest(principal,interestRate, loanTermYears);

            Assert.AreEqual(expectedTotalInterest, totalInterest);
        }

        [TestMethod]
        public void BiWeeklyPaymentInterest()
        {
            decimal homeValue = 525_000m;
            decimal downPayment = 76_000m;
            decimal principal = homeValue - downPayment;
            int loanTermYears = 30;
            decimal interestRate = 7.3m;

            double totalBiWeeklyInterest = Calculations.BiWeeklyInterest(principal, interestRate, loanTermYears);

            double expectedTotalBiWeeklyInterest = 489_534.97;

            Assert.AreEqual(expectedTotalBiWeeklyInterest, totalBiWeeklyInterest);

        }

        [TestMethod]
        public void MortgageConstructorTest()
        {
            Mortgage m1 = new Mortgage(400000, 4.5m, 30);
            Mortgage m2 = new Mortgage(300000, 6.2m, 25);

            Assert.AreEqual(360, m1.MonthlyPayments.Count);
            Assert.AreEqual(300, m2.MonthlyPayments.Count);

        }
    }
}
