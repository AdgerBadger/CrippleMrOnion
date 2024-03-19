using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public enum GroupingType
    {
        Invalid = 0,
        Bagel = 1,
        TwoCardOnion = 2,
        BrokenFlush = 3,
        ThreeCardOnion = 4,
        Flush = 5,
        FourCardOnion = 6,
        BrokenRoyal = 7,
        FiveCardOnion = 8,
        Royal = 9,
        SixCardOnion = 10,
        WildRoyal = 11,
        SevenCardOnion = 12,
        DoubleOnion = 13,
        TripleOnion = 14,
        LesserOnion = 15,
        GreaterOnion = 16,
        NineCardRunning = 17,
        TenCardRunning = 18
    }
    public class CardGrouping : IFixedCardCollection
    {
        private GroupingCard[] _cards { get; init; }
        public CardGrouping(params Card[] cards)
        {
            _cards = new GroupingCard[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                _cards[i] = cards[i].ToGroupingCard();
            }
        }

        public CardGrouping(params GroupingCard[] cards)
        {
            _cards = new GroupingCard[cards.Length];
            cards.CopyTo(_cards, 0);
        }

        public Card this[int index]
        {
            get
            {
                return _cards[index].ToCard();
            }
        }

        public int Count
        {
            get { return _cards.Length; }
        }

        public bool Wild = false;

        public GroupingType Type
        {
            get
            {
                // Ten Card Running Flush
                if (Count == 10 && IsAllOfSameSuit && IsRunning)
                {
                    return GroupingType.TenCardRunning;
                }
                // Nine Card Running Flush
                else if (Count == 9 && IsAllOfSameSuit && IsRunning)
                {
                    return GroupingType.NineCardRunning;
                }
                // Greater Onion
                else if (Count == 10 && PictureCards == 5 && CardsOfRank(CardRank.Ace) == 5)
                {
                    return GroupingType.GreaterOnion;
                }
                // Lesser Onion
                else if (Count == 8 && PictureCards == 4 && CardsOfRank(CardRank.Ace) == 4)
                {
                    return GroupingType.LesserOnion;
                }
                // Triple Onion
                else if (Count == 6 && PictureCards == 3 && CardsOfRank(CardRank.Ace) == 3)
                {
                    return GroupingType.TripleOnion;
                }
                // Double Onion
                else if (Count == 4 && PictureCards == 2 && CardsOfRank(CardRank.Ace) == 2)
                {
                    return GroupingType.DoubleOnion;
                }
                // Seven Card Onion
                else if (Count == 7 && CardValue == 21)
                {
                    return GroupingType.SevenCardOnion;
                }
                // Wild Royal
                else if (Count == 3 && Wild && CardsOfRank(CardRank.Eight) == 3)
                {
                    return GroupingType.WildRoyal;
                }
                // Six Card Onion
                else if (Count == 6 && CardValue == 21)
                {
                    return GroupingType.SixCardOnion;
                }
                // Royal
                else if (Count == 3 && CardsOfRank(CardRank.Seven) == 3)
                {
                    return GroupingType.Royal;
                }
                // Five Card Onion
                else if (Count == 5 && CardValue == 21)
                {
                    return GroupingType.FiveCardOnion;
                }
                // Broken Royal
                else if (Count == 3 && CardsOfRank(CardRank.Six) == 1 && CardsOfRank(CardRank.Seven) == 1 && CardsOfRank(CardRank.Eight) == 1)
                {
                    return GroupingType.BrokenRoyal;
                }
                // Four Card Onion
                else if (Count == 4 && CardValue == 21)
                {
                    return GroupingType.FourCardOnion;
                }
                // Flush
                else if (Count >= 3 && CardValue >= 16 && CardValue <= 21 && IsAllOfSameSuit)
                {
                    return GroupingType.Flush;
                }
                // Three Card Onion
                else if (Count == 3 && CardValue == 21)
                {
                    return GroupingType.ThreeCardOnion;
                }
                // Broken Flush
                else if (Count >= 3 && CardValue >= 16 && CardValue <= 21 && CardsOfMostFrequentSuit() == Count - 1)
                {
                    return GroupingType.BrokenFlush;
                }
                // Two Card Onion
                else if (Count == 2 && CardValue == 21)
                {
                    return GroupingType.TwoCardOnion;
                }
                // Bagel
                else if (Count == 2 && CardValue == 20)
                {
                    return GroupingType.Bagel;
                }
                // No Valid Grouping
                else
                {
                    return GroupingType.Invalid;
                }
            }
        }

        public int Value
        {
            get
            {
                return (int)Type;
            }
        }

        public bool IsAllOfSameSuit
        {
            get
            {
                CardSuit suit = CardSuit.Null;
                foreach (Card card in (Card[])this)
                {
                    if (suit == CardSuit.Null) suit = card.Suit;
                    if (card.Suit != suit) return false;
                }
                return true;
            }
        }

        public bool IsRunning
        {
            get
            {
                List<GroupingCard> sortedCards = _cards.ToList();
                sortedCards.Sort();
                int prevRank = (int)sortedCards[0].Rank - 1;
                for (int i = 0; i < sortedCards.Count; i++)
                {
                    if ((int)sortedCards[i].Rank != prevRank + 1)
                    {
                        return false;
                    }
                    else
                    {
                        prevRank = (int)sortedCards[i].Rank;
                    }
                }
                return true;
            }
        }

        public int CardValue
        {
            get
            {
                int totalValue = 0;
                foreach (GroupingCard card in _cards)
                {
                    totalValue += card.Value;
                }
                return totalValue;
            }
        }

        public override string ToString()
        {
            return $"{Type} with value {CardValue} ({Count} cards)";
        }

        public int CardsOfRank(CardRank rank)
        {
            int noOf = 0;
            foreach (GroupingCard card in _cards)
            {
                if (card.Rank == rank) noOf++;
            }
            return noOf;
        }

        public int CardsOfMostFrequentSuit()
        {
            int noOfMostFrequent = 0;
            CardSuit mostFrequentSuit = CardSuit.Null;
            for (int i = 0; i < 8; i++)
            {
                int noOfSuit = CardsOfSuit((CardSuit)(i + 1));
                if (noOfSuit > noOfMostFrequent)
                {
                    mostFrequentSuit = (CardSuit)(i + 1);
                    noOfMostFrequent = noOfSuit;
                }
            }
            return noOfMostFrequent;
        }

        public int CardsOfSuit(CardSuit suit)
        {
            return _cards.Where(x => x.Suit == suit).ToList().Count;
        }

        public bool CanAddTo(int target, int maxDepth = -1)
        {
            if(maxDepth == 0) return false;
            for (int i = 0; i < _cards.Length; i++)
            {
                if (target - _cards[i].Value > 0)
                {
                    Card[] cardArr = new Card[_cards.Length];
                    _cards.CopyTo(cardArr, 0);
                    List<Card> list = cardArr.ToList();
                    list.RemoveAt(i);
                    bool addsWith = canAddTo(target - _cards[i].Value, list, maxDepth - 1);
                    if (addsWith)
                    {
                        return true;
                    }
                }
                else if (target - _cards[i].Value == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool canAddTo(int target, List<Card> currentCards, int maxDepth)
        {
            if (maxDepth == 0) return false;
            for (int i = 0; i < currentCards.Count; i++)
            {
                if (target - currentCards[i].Value > 0)
                {
                    Card[] cardArr = new Card[currentCards.Count];
                    _cards.CopyTo(cardArr, 0);
                    List<Card> list = cardArr.ToList();
                    list.RemoveAt(i);
                    bool addsWith = canAddTo(target - _cards[i].Value, list, maxDepth - 1);
                    if (addsWith)
                    {
                        return true;
                    }
                }
                else if (target - _cards[i].Value == 0)
                {
                    return true;
                }
            }
            return false;

        }

        public CardSuit MostFrequentSuit
        {
            get
            {
                int noOfMostFrequent = 0;
                CardSuit mostFrequentSuit = CardSuit.Null;
                for (int i = 0; i < 8; i++)
                {
                    int noOfSuit = CardsOfSuit((CardSuit)(i + 1));
                    if (noOfSuit > noOfMostFrequent)
                    {
                        mostFrequentSuit = (CardSuit)(i + 1);
                    }
                }
                return mostFrequentSuit;
            }
        }

        public int PictureCards
        {
            get
            {
                int noOf = 0;
                CardRank[] validRanks = new CardRank[] { CardRank.Jack, CardRank.Knight, CardRank.Queen, CardRank.King };
                foreach (Card card in (Card[])this)
                {
                    if (validRanks.Contains(card.Rank))
                    {
                        noOf++;
                    }
                }
                return (noOf);
            }
        }

        public static explicit operator Card[](CardGrouping grouping)
        {
            Card[] returner = new Card[grouping.Count];
            for (int i = 0; i < grouping.Count; i++)
            {
                returner[i] = grouping[i];
            }
            return returner;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return (IEnumerator<Card>)_cards.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }
    }
}
