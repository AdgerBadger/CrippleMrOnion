using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public class BoardState
    {
        public PartialCardSet[] PublicPlayerHands { get; init; }
        public int PlayerNo { get { return PublicPlayerHands.Length; } }
        public CardGrouping LastPlayerGrouping { get; set; }

        public BoardState(PartialCardSet[] hands, CardGrouping lastPlayerGrouping)
        {
            PublicPlayerHands = hands;
            LastPlayerGrouping = lastPlayerGrouping;
        }
    }

    public class OwnedBoardState : BoardState
    {
        public Hand OwnHand { get; init; }
        int OwnIndex { get; init; }
        public OwnedBoardState(PartialCardSet[] hands, CardGrouping lastPlayerGrouping, Hand ownHand, int self) : base(hands, lastPlayerGrouping)
        {
            OwnHand = ownHand;
            OwnIndex = self;
        }
    }
}
