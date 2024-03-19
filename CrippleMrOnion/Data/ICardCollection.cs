using System.Runtime.CompilerServices;

namespace CrippleMrOnion.Data
{
    public interface ICardCollection : ICollection<Card>
    {
        public Card this[int index] { get; set; }
        public void RemoveAt(int index);
    }

    public interface IOpenCardCollection : ICardCollection
    {
        
    }

    public interface IMixedCardCollection : ICardCollection
    {
        public void Add(Card card, bool hidden);
    }

    public interface IClosedCardCollection : ICardCollection
    {
        public delegate Card? Populater(int index);
        public void Populate(Populater populater);
        public Card Pick();
        public Card PickAt(int index);
    }

    public interface IFixedCardCollection : IEnumerable<Card>
    {
        public Card this[int index] { get; }
        public int Count { get; }
    }
}