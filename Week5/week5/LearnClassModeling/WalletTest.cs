using LearnClassModeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnClassModeling
{
    internal class WalletTest
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
    }
}