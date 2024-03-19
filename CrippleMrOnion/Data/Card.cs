using CrippleMrOnion.Display;
using CrippleMrOnion.Display.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public enum CardSuit
    {
        Null = 0,
        Staves = 1, // Black
        Swords = 2, // Red
        Coins = 3, // Black
        Cups = 4, // Red
        Elephants = 5, // Black
        Octograms = 6, // Red
        Terrapins = 7, // Black
        Crowns = 8, // Red
    }

    public enum CardRank
    {
        Null = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Knight = 12,
        Queen = 13,
        King = 14,
    }
    public abstract class BaseCard : IGraphical, IComparable<BaseCard>, IEquatable<BaseCard>
    {
        public CardSuit Suit { get; protected init; }
        public CardRank Rank { get; protected init; }
        public BaseCard(int suit, int rank)
        {
            if (suit < 0 || suit > 8 || rank < 1 || rank > 14)
            {
                throw new ArgumentException($"Invalid rank or suit: S: {suit}, R: {rank} are not valid");
            }
            Suit = (CardSuit)suit;
            Rank = (CardRank)rank;
        }

        public BaseCard(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        protected BaseCard()
        {
            Suit = CardSuit.Null;
            Rank = CardRank.Null;
        }

        public abstract int Value { get; }

        public static string[] AbbreviatedRankLookup = new string[]
        {
            "A",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "N",
            "Q",
            "K"
        };

        public GroupingCard ToGroupingCard(bool high = false, bool wild = false)
        {
            return new GroupingCard(Suit, Rank, high, wild);
        }

        public IDrawable ToGraphic()
        {
            Sprite returner = new(10, 8, new Colour(5, 5, 5), new Colour(0, 0, 0, foreground: true));
            switch (Suit)
            {
                case CardSuit.Staves:
                    {
                        returner.BaseForegroundColour = new Colour(0, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} |".PadRight(10);
                        returner += $"          ";
                        returner += @"    ()    ";
                        returner += @"    ||    ";
                        returner += @"    ||    ";
                        returner += @"    ()    ";
                        returner += $"          ";
                        returner += $"| {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Swords:
                    {
                        returner.BaseForegroundColour = new Colour(5, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} +-".PadRight(10);
                        returner += $"          ";
                        returner += @"    /\    ";
                        returner += @"    ||    ";
                        returner += @"   _||_   ";
                        returner += @"    ][    ";
                        returner += $"          ";
                        returner += $"-+ {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }

                case CardSuit.Coins:
                    {
                        returner.BaseForegroundColour = new Colour(0, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} ∙".PadRight(10);
                        returner += $"          ";
                        returner += @"   .--.   ";
                        returner += @"  /    \  ";
                        returner += @"  \    /  ";
                        returner += @"   '--'   ";
                        returner += $"          ";
                        returner += $"∙ {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Cups:
                    {
                        returner.BaseForegroundColour = new Colour(5, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} #".PadRight(10);
                        returner += $"   ,__,   ";
                        returner += @"   |  |   ";
                        returner += @"   \__/   ";
                        returner += @"    ][    ";
                        returner += @"   {__}   ";
                        returner += $"          ";
                        returner += $"# {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Elephants:
                    {
                        returner.BaseForegroundColour = new Colour(0, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} M&".PadRight(10);
                        returner += $"          ";
                        returner += @"          ";
                        returner += @"  ,----,  ";
                        returner += @" /|,__,@. ";
                        returner += @"  []  []| ";
                        returner += $"          ";
                        returner += $"&W {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Octograms:
                    {
                        returner.BaseForegroundColour = new Colour(5, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} {{}}".PadRight(10);
                        returner += $"          ";
                        returner += @"   ,__,   ";
                        returner += @"  |_\/_|  ";
                        returner += @"  | /\ |  ";
                        returner += @"   '--'   ";
                        returner += $"          ";
                        returner += $"{{}} {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Terrapins:
                    {
                        returner.BaseForegroundColour = new Colour(0, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} n∙".PadRight(10);
                        returner += $"          ";
                        returner += @"   ,__,   ";
                        returner += @"  |####|  ";
                        returner += @"  -,-,-=∙ ";
                        returner += @"          ";
                        returner += $"          ";
                        returner += $"∙u {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Crowns:
                    {
                        returner.BaseForegroundColour = new Colour(5, 0, 0, foreground: true);
                        returner += $"{AbbreviatedRankLookup[(int)Rank - 1]} XX".PadRight(10);
                        returner += $"          ";
                        returner += @"          ";
                        returner += @"  |\/\/|  ";
                        returner += @"  |_@@_|  ";
                        returner += @"          ";
                        returner += $"          ";
                        returner += $"XX {AbbreviatedRankLookup[(int)Rank - 1]}".PadLeft(10);
                        return returner;
                    }
                case CardSuit.Null:
                    {
                        returner.BaseForegroundColour = new Colour(0, 0, 0, foreground: true);
                        returner.BaseBackgroundColour = new Colour(2, 2, 2);
                        returner += @"+--------+";
                        returner += @"|#= {} =#|";
                        returner += @"|+-    --|";
                        returner += @"|   M&   |";
                        returner += @"|   n∙   |";
                        returner += @"| #    ∙ |";
                        returner += @"|#= XX =#|";
                        returner += @"+--------+";
                        return returner;
                    }
                default:
                    throw new Exception("Invalid Card Suit");
            }
        }

        public int CompareTo(BaseCard? that)
        {
            if (that == null) throw new ArgumentNullException();
            if (that.Value > this.Value) return -1;
            else if (that.Value < this.Value) return 1;
            else return 0;
        }

        public bool Equals(BaseCard? that)
        {
            if (that == null) throw new ArgumentNullException();
            if (that.Value == this.Value && that.Suit == this.Suit) return true;
            else return false;
        }
    }

    public class Card : BaseCard
    {
        public Card(int _, int __) : base(_, __) { }
        public Card(CardSuit _, CardRank __) : base(_, __) { }
        private Card()
        {
            Suit = CardSuit.Null;
            Rank = CardRank.Null;
        }
        public override int Value
        {
            get
            {
                return Math.Clamp((int)Rank, 1, 10);
            }
        }
        public static Card Null()
        {
            return new Card();
        }
    }

    public class GroupingCard : BaseCard, IEquatable<GroupingCard>, IComparable<GroupingCard>
    {
        public GroupingCard(CardSuit suit, CardRank rank, bool high = false, bool wild = false) : base(suit, rank)
        {
            High = high;
            Wild = wild;
        }
        public override int Value
        {
            get
            {
                if (High && Rank == CardRank.Ace) return 11;
                return Math.Clamp((int)Rank, 1, 10);
            }
        }

        public Card ToCard()
        {
            return new Card(Suit, Rank);
        }

        public bool Equals(GroupingCard? that)
        {
            if (that == null) throw new ArgumentNullException();
            if (that.Value == this.Value && that.Suit == this.Suit && that.High == this.High && (that.Wild == this.Wild || true)) return true;
            else return false;
        }

        public int CompareTo(GroupingCard? that)
        {
            if (that == null) throw new ArgumentNullException();
            if (that.Value > this.Value) return -1;
            else if (that.Value < this.Value) return 1;
            else return 0;
        }

        public bool High;
        public bool Wild;
    }
}
