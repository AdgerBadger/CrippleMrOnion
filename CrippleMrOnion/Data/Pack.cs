using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public class Pack : IClosedCardCollection
    {
        private readonly List<Card> _cards = new List<Card>();
        private bool _populated = false;
        public Pack(bool defaultPopulation = true)
        {
            if (defaultPopulation) Populate(FullPackPopulater);
        }

        public Pack(IClosedCardCollection.Populater populater)
        {
            Populate(populater);
        }

        public Card Pick()
        {
            if (_cards.Count > 0)
            {
                Card card = _cards.First();
                _cards.RemoveAt(0);
                return card;
            }
            else
            {
                throw new IndexOutOfRangeException("No cards in pack to draw from.");
            }
        }

        public Card[] Pick(int no)
        {
            Card[] cards = new Card[no];
            for(int i = 0; i < no; i++)
            {
                cards[i] = Pick();
            }
        }

        public void RemoveAt(int index)
        {
            _cards.RemoveAt(index);
        }

        public void Add(Card card)
        {
            if (!Contains(card))
            {
                _cards.Add(card);
            }
            else
            {
                throw new Exception("Attempted to add duplicate card to pack.");
            }
        }

        public bool Contains(Card card)
        {
            return _cards.Contains(card);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public bool Remove(Card card)
        {
            return _cards.Remove(card);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            _cards.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return _cards.Count;
            }
        }

        public bool IsReadOnly { get { return false; } }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        public Card this[int index]
        {
            get
            {
                if (index >= 0 && index < _cards.Count)
                {
                    return _cards[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("No cards in pack to draw from.");
                }
            }
            set
            {
                if (index >= 0 && index < _cards.Count)
                {
                    _cards[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("No cards in pack to draw from.");
                }
            }
        }

        public Card PickAt(int loc)
        {
            if (loc >= 0 && loc < _cards.Count)
            {
                Card card = _cards[loc];
                _cards.RemoveAt(loc);
                return card;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void Populate(IClosedCardCollection.Populater populater)
        {
            if (_populated) return;

            _populated = true;
            Card? card = null;
            int index = 0;
            while (card != null)
            {
                card = populater(index);
                if (card == null) continue;

                if (_cards.Count > index) _cards[index] = card;
                else _cards.Add(card);

                index++;
            }
        }

        public static Card? FullPackPopulater(int index)
        {
            if (index < 112)
            {
                return new Card(index / 14, index % 14);
            }
            else
            {
                return null;
            }
        }
    }
}
