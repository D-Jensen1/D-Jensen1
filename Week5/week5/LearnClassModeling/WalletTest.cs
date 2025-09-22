using LearnClassModeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnClassModeling
{
    [TestClass]
    public class WalletTest
    {
        [TestMethod]
        public void DefaultConstructorHas0Bills()
        {

            Wallet w = new();
            Assert.AreEqual(0, w.Bills.Count);
        }

        [TestMethod]
        public void ConstructorAcceptsListOfBills()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);
            Assert.AreEqual(2, w.Bills.Count);
        }

        [TestMethod]
        public void WalletComputesBillTotal()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);
            Assert.AreEqual(30, w.BillTotal);
        }

        [TestMethod]
        public void WalletAddBill()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);

            w.AddBill(new Bill(Denomination.Fifty));
            Assert.AreEqual(3, w.Bills.Count);
        }

        [TestMethod]
        public void WalletAddListOfBill()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);
            w.AddBills([new Bill(Denomination.Ten), new Bill(Denomination.Twenty)]);
            Assert.AreEqual(4, w.Bills.Count);
        }

        [TestMethod]
        public void WalletCanSortBill()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);
            w.AddBill(new Bill(Denomination.Fifty));
            w.AddBill(new Bill(Denomination.Five));
            w.SortBill();
            Assert.AreEqual(new Bill(Denomination.Five), w.Bills[0]);
            Assert.AreEqual(new Bill(Denomination.Ten), w.Bills[1]);
        }
        [TestMethod]
        public void WalletCanRemoveBill()
        {
            List<Bill> bills = [new Bill(Denomination.Ten), new Bill(Denomination.Twenty)];
            Wallet w = new(bills);
            w.RemoveBill(new Bill(Denomination.Ten));
            Assert.AreEqual(new Bill(Denomination.Twenty), w.Bills[0]);
        }

        [TestMethod]
        public void HandleWalletEventTest()
        {
            int testAmount = 0;
            Wallet w = new();
            // This wires W_BillAdded to respond the BillAdded event of w
            //w.BillAdded += W_BillAdded; 
            w.BillAdded += (aWallet, bill) => testAmount += bill.Amount; //Lemda shorthand to avoid outside method
            
            w.BillRemoved += (aWallet, bill) => testAmount -= bill.Amount;
            w.AddBill(new Bill(Denomination.Five));
            Assert.AreEqual(5, testAmount);

            w.AddBill(new Bill(Denomination.Five));
            Assert.AreEqual(10, testAmount);

            w.RemoveBill(new Bill(Denomination.Five));
            Assert.AreEqual(5, testAmount);
        }
        /*
                private int test = 0;
                private void W_BillRemoved(Bill bill)
                {
                    test -= bill.Amount;

                }

                private void W_BillAdded(Bill bill)
                {
                    test += bill.Amount;
                }
        */
        [TestMethod]
        public void GenericDelegateTest()
        {
            // Final parameter of Func<> is the return type
            Func<int, int, string> tester =
                (x, y) => (x + y).ToString();

            DemoMethod(tester);
            Assert.IsTrue("10" == tester.Invoke(5, 5));
        }

        private void DemoMethod(Func<int, int, string> tester)
        {
            Assert.AreEqual("8", tester.Invoke(5, 3));
        }

        [TestMethod]
        public void DictionaryIndexTest()
        {

        }
    }
}