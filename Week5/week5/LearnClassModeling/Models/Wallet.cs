using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LearnClassModeling.Models // namespace follows folder structure
{
    internal class Wallet
    {
        private const int MAX_CARDS = 20;
        private const int MAX_BILLS = 100; 
        private const int MAX_IDS = 10;


        // fields represent data - internal data, encapsulation
        private List<Bill> _bills = new();
        private List<ID> _ids;
        private List<Card> _cards;

        private Dictionary<Bill, int> _billCounter = new();

        // property - get/set method to access data
        internal List<Bill> Bills => this._bills; // This is actually a leaky encapsulation
        internal List<ID> IDs => this._ids;
        internal List<Card> Cards => this._cards;

        public int this[Bill b]
        {
            get
            {
                if (_billCounter.ContainsKey(b)) return _billCounter[b];
                else return 0;
            }
        }

        internal int BillTotal
        {
            get
            {
                int total = 0;
                foreach (var bill in Bills)
                {
                    total += bill.Amount;
                }
                return total;
            }
            
        }
    /*
        internal Wallet(List<Bill>? bills = null, List<ID>? ids = null, List<Card>? cards = null,)
        {
            this._bills = bills ?? new List<Bill>();
            this._ids = ids ?? new List<ID>();
            this._cards = cards ?? new List<Card>();

        }
    */
        // method
        public Wallet() 
        {
            // wire up event handlers
            this.BillAdded += (w, b) =>
            {
                if (_billCounter.ContainsKey(b)) _billCounter[b] += 1;
                else _billCounter.Add(b, 1);
            };

            this.BillRemoved += (w, b) =>
            {
                if (_billCounter.ContainsKey(b)) _billCounter[b] -= 1;
            };
        }

        public Wallet(IEnumerable<Bill> bills) : this() // Constructor chaining
        {
            foreach (var bill in bills)
            {
                this.AddBill(bill);
            }
        }


        internal void AddBill(Bill bill)
        {
            if (this._bills.Count >= MAX_BILLS) throw new ArgumentException("Stack is too fat");
            this._bills.Add(bill);
            this.BillAdded?.Invoke(this, bill);
        }

        internal void AddBills(IEnumerable<Bill> bills)
        {
            if (this._bills.Count + bills.Count() >= MAX_BILLS) throw new ArgumentException("Stack is too fat");
            //this._bills.AddRange(bills);
            foreach (var bill in bills)
            {
                this.AddBill(bill);
            }
        }

        internal void SortBill()
        {
            this._bills.Sort();
        }

        internal void RemoveBill(Bill bill)
        {
            this._bills.Remove(bill);
            this.BillRemoved?.Invoke(this, bill);

        }

        // event
        //public event Action<Bill>? BillAdded;
        public event EventHandler<Bill> BillAdded; // EventHandler must pass (who it is(sender), information)
        // Action and function are two generic delegates that can model any event 
        public event EventHandler<Bill>? BillRemoved;
        // Action and function are two generic delegates that can model any event 
        // EventHandler<T> can model any event
    }
}
