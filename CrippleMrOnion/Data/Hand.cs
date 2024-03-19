using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrippleMrOnion.Display;

namespace CrippleMrOnion.Data
{
    public class Hand : IMixedCardCollection, IGraphical
    {
        public List<Card> HiddenCards;
        public List<Card> PublicCards;
        public bool IsReadOnly { get { return false; } }
        public Hand(IEnumerable<Card>? hiddenCards = null, IEnumerable<Card>? publicCards = null)
        {
            HiddenCards = hiddenCards != null ? new List<Card>(hiddenCards) : new List<Card>();
            PublicCards = publicCards != null ? new List<Card>(publicCards) : new List<Card>();
        }

        public int Count
        {
            get { return HiddenCards.Count + PublicCards.Count; }
        }

        public Card this[int index]
        {
            get
            {
                return index < HiddenCards.Count ? HiddenCards[index] : PublicCards[index - HiddenCards.Count];
            }
            set
            {
                HiddenCards[index] = value;
            }
        }

        public void Add(Card card, bool hidden = true)
        {
            if (hidden)
            {
                HiddenCards.Add(card);
            }
            else
            {
                PublicCards.Add(card);
            }

        }

        public void Add(Card card)
        {
            Add(card, true);
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            foreach( Card card in cards)
            {
                Add(card);
            }
        }

        public bool Remove(Card card)
        {
            if (HiddenCards.Contains(card))
            {
                HiddenCards.Remove(card);
                return true;
            }
            else if (PublicCards.Contains(card))
            {
                PublicCards.Remove(card);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(Card card)
        {
            return HiddenCards.Contains(card) || PublicCards.Contains(card);
        }

        public void RemoveAt(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Index {index} not in range of Hand.");
            if (index < HiddenCards.Count)
            {
                HiddenCards.RemoveAt(index);
            }
            else if (index - HiddenCards.Count < PublicCards.Count)
            {
                PublicCards.RemoveAt(index - HiddenCards.Count);
            } else
            {
                throw new IndexOutOfRangeException($"Index {index} not in range of Hand.");
            }
        }

        public void Clear()
        {
            HiddenCards.Clear();
            PublicCards.Clear();
        }
        public void CopyTo(Card[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((List<Card>)this).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((List<Card>)this).GetEnumerator();
        }


        public IDrawable ToGraphic()
        {
            DrawableCollection output = new();
            for(int i = 0; i < PublicCards.Count; i++)
            {
                IDrawable cardGraphic = PublicCards[i].ToGraphic();
                Located<IDrawable> locCardGraphic = new(cardGraphic, i * (cardGraphic.Width + 2), 1);
                output.Add(locCardGraphic);
                Program.Debug += $"P{i}"+locCardGraphic.ToString()+ "\n";
            }

            for(int i = 0; i < HiddenCards.Count ; i++)
            {
                IDrawable cardGraphic = HiddenCards[i].ToGraphic();
                Located<IDrawable> locCardGraphic = new(cardGraphic, i * (cardGraphic.Width + 2), cardGraphic.Height + 2);
                output.Add(locCardGraphic);
                Program.Debug += $"C{i}" + locCardGraphic.ToString() + "\n";
            }
            return output;
        }

        public static explicit operator PartialCardSet(Hand hand)
        {
            return new PartialCardSet
            {
                Size = hand.Count,
                VisibleCards = hand.PublicCards.ToArray()
            };
        }

        public static explicit operator List<Card> (Hand hand)
        {
            List<Card> allCards = new List<Card>();
            allCards.AddRange(hand.HiddenCards.ToArray());
            allCards.AddRange(hand.PublicCards.ToArray());
            return allCards;
        }
    }

}
