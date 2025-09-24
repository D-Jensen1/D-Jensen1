using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models // namespace follows folder structure
{
    public class WalletBill : ICollection<Bill>
    {
        #region Fields
        private const int MAX_CARDS = 20;
        private const int MAX_BILLS = 100;
        private const int MAX_IDS = 10;


        // fields represent data - internal data, encapsulation
        private List<Bill> _bills = new();
        private List<ID> _ids = new();
        private List<Card> _cards = new();

        private Dictionary<Bill, int> _billCounter = new();
        #endregion

        #region Properties
        // property - get/set method to access data

        public ImmutableList<Bill> Bills => this._bills.ToImmutableList(); // make the property return an immutable list so it cannot
                                                                           // be used to modify wallet's _bill internal state
        public List<ID> IDs => this._ids;
        public List<Card> Cards => this._cards;

        public int CardCount { get => this._cards.Count; }

        // indexer property, uses bill to get number of bills for that denomination
        public int this[Bill b]
        {
            get
            {
                if (_billCounter.ContainsKey(b))
                    return _billCounter[b];
                else
                    return 0;
            }
        }

        public int BillTotal
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
        #endregion

        #region Constructors
        public WalletBill()
        {
            // wire up event handlers
            this.BillAdded += (w, b) =>
            {
                if (_billCounter.ContainsKey(b)) _billCounter[b] += 1;
                else _billCounter.Add(b, 1);
                _bills.Sort();
            };

            this.BillRemoved += (w, b) =>
            {
                if (_billCounter.ContainsKey(b)) _billCounter[b] -= 1;
            };

        /*
            internal Wallet(List<Bill>? bills = null, List<ID>? ids = null, List<Card>? cards = null,)
            {
                this._bills = bills ?? new List<Bill>();
                this._ids = ids ?? new List<ID>();
                this._cards = cards ?? new List<Card>();

            }
        */

        }

        public WalletBill(params IEnumerable<Bill> bills) : this() // Constructor chaining
        {
            this.AddBill(bills);
        }
        #endregion

        #region Methods
        private void addBill(Bill bill)
        {
            this._bills.Add(bill);
            this.BillAdded?.Invoke(this, bill);
        }

        public void AddBill(params IEnumerable<Bill> bills) // params will turn any input into IEnumerable<Bill>
        {
            if (this._bills.Count + bills.Count() >= MAX_BILLS) throw new ArgumentException("Stack is too fat");
            //this._bills.AddRange(bills);
            foreach (var bill in bills)
            {
                this.addBill(bill);
            }
        }

        private void removeBill(Bill bill)
        {
            if (this._bills.Count(x => x == bill) == 0)
                throw new ArgumentException($"You don't have a {bill.Amount:C} bill.");
            this._bills.Remove(bill);
            this.BillRemoved?.Invoke(this, bill);
        }

        public void RemoveBill(params IEnumerable<Bill> bills)
        {
            foreach (var bill in bills)
            {
                this.removeBill(bill);
            }
        }
        private void addCard(Card card)
        {
            this.CardAdding?.Invoke(this, card);
            this._cards.Add(card);
            this.CardAdded?.Invoke(this, card);
        }

        public void AddCard(params IEnumerable<Card> cards)
        {
            if ((cards.Count() + this._cards.Count) > MAX_CARDS) throw new InvalidOperationException("Wallet is full");
            foreach (var item in cards)
            {
                this.addCard(item);
            }
        }

        public IEnumerable<Card> FindCardByPurpose(Purpose p)
        {
            foreach (var item in _cards)
            {
                if (item.GetPurpose().HasFlag(p))
                {
                    yield return item;
                }
            }
        }
        #endregion

        #region Events
        // event
        //public event Action<Bill>? BillAdded;
        public event EventHandler<Bill> BillAdded; // EventHandler must pass (who it is(sender), information)
        public event EventHandler<Card> CardAdding;
        public event EventHandler<Card> CardAdded;
        // Action and function are two generic delegates that can model any event 
        public event EventHandler<Bill>? BillRemoved;
        public event EventHandler<Card>? CardRemoving;
        public event EventHandler<Card>? CardRemoved;
        // Action and function are two generic delegates that can model any event 
        // EventHandler<T> can model any event 
        #endregion


        #region IEnumerable - using System.Collections
        IEnumerator IEnumerable.GetEnumerator() => this.Bills.GetEnumerator();
        #endregion

        #region IEnumerable<Bill>  - using System.Collections.Generic
        IEnumerator<Bill> IEnumerable<Bill>.GetEnumerator() => this.Bills.GetEnumerator();
        #endregion

        #region ICollection<Bill>
        int ICollection<Bill>.Count => this._bills.Count;
        bool ICollection<Bill>.IsReadOnly => true;
        void ICollection<Bill>.Add(Bill item)
        {
            this.AddBill(item);
        }
        void ICollection<Bill>.Clear()
        {
            throw new Exception("We have been robbed.");
        }
        bool ICollection<Bill>.Contains(Bill bill)
        {
            return this.Bills.Contains(bill);
        }
        void ICollection<Bill>.CopyTo(Bill[] array, int arrayIndex) => this.Bills.CopyTo(array, arrayIndex);
        bool ICollection<Bill>.Remove(Bill item)
        {
            try
            {
                this.RemoveBill(item);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion
    }
}
