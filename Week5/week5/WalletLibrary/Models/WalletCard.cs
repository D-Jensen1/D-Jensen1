using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public partial class WalletCard : ICollection<Card>
    {
        #region Fields
        private const int MAX_CARDS = 20;

        private List<Card> _cards = new();

        private Dictionary<Card, int> _cardCounter = new();
        #endregion

        #region Properties
        public ImmutableList<Card> Cards => this._cards.ToImmutableList(); 

        public int CardCount { get => this._cards.Count; }

        public int this[Card card]
        {
            get
            {
                if (_cardCounter.ContainsKey(card))
                    return _cardCounter[card];
                else
                    return 0;
            }
        }
        #endregion

        #region Constructors
        public WalletCard( List<Card>? cards = null)
        {
            this._cards = cards ?? new List<Card>();
        }
         #endregion

        #region Methods
        private void addCard(Card card)
        {
            this.CardAdding?.Invoke(this, card);
            this._cards.Add(card);
            this.CardAdded?.Invoke(this, card);
        }
        public void AddCard(params IEnumerable<Card> cards)
        {
            if ((cards.Count() + this._cards.Count) > MAX_CARDS) throw new InvalidOperationException($"Your wallet can only hold {MAX_CARDS} cards.");
            foreach (var item in cards)
            {
                this.addCard(item);
            }
        }

        private void removeCard(Card card)
        {
            if (this._cards.Count(x => x == card) == 0)
                throw new ArgumentException($"You don't have a {card} card.");
            this._cards.Remove(card);
            this.CardRemoved?.Invoke(this, card);
        }
        public void RemoveCard(params IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                this.removeCard(card);
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
        public event EventHandler<Card> CardAdding;
        public event EventHandler<Card> CardAdded;

        public event EventHandler<Card>? CardRemoving;
        public event EventHandler<Card>? CardRemoved;
        #endregion


        #region IEnumerable - using System.Collections
        IEnumerator IEnumerable.GetEnumerator() => this.Cards.GetEnumerator();
        #endregion

        #region IEnumerable<Bill>  - using System.Collections.Generic
        IEnumerator<Card> IEnumerable<Card>.GetEnumerator() => this.Cards.GetEnumerator();
        #endregion

        #region ICollection<Bill>
        int ICollection<Card>.Count => this._cards.Count;
        bool ICollection<Card>.IsReadOnly => true;
        void ICollection<Card>.Add(Card item)
        {
            this.AddCard(item);
        }
        void ICollection<Card>.Clear()
        {
            throw new Exception("We have been robbed.");
        }
        bool ICollection<Card>.Contains(Card card)
        {
            return this.Cards.Contains(card);
        }
        void ICollection<Card>.CopyTo(Card[] array, int arrayIndex) => this.Cards.CopyTo(array, arrayIndex);
        bool ICollection<Card>.Remove(Card item)
        {
            try
            {
                this.RemoveCard(item);
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
