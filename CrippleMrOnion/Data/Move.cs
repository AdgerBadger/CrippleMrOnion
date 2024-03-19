using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public enum MoveType
    {
        Fold,
        Raise,
        Interrupt
    }
    public class Move
    {
        public MoveType Type { get; init; }
        public CardGrouping? CardsInPlay { get; init; }
    }
}
