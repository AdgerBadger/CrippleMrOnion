using CrippleMrOnion.Data;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Controllers
{
    public class Bot : IController
    {
        public Card[] InitialDeal(IEnumerable<Card> cards)
        {
            return Array.Empty<Card>();
        }

        public void FullDeal(IEnumerable<Card> cards)
        {
            return;
        }

        private OwnedBoardState _currentBoardState;

        public void NewTurn(OwnedBoardState state)
        {
            _currentBoardState = state;
        }

        public Move AttemptTurn()
        {
            List<GroupingType> types = SomePossibleGroupingTypes(_currentBoardState).ToList();
            types.Sort((GroupingType a, GroupingType b)=>((int)b)-((int)a));
            GroupingType higherType = GroupingType.Invalid;
            foreach (GroupingType type in types)
            {
                if((int)type > (int)_currentBoardState.LastPlayerGrouping.Type)
                {
                    higherType = type;
                }
            }
            if(higherType == GroupingType.Invalid)
            {
                return new Move
                {
                    Type = MoveType.Fold
                };
            } else if (higherType >= GroupingType.DoubleOnion && higherType <= GroupingType.GreaterOnion)
            {
                return new Move
                {
                    Type = MoveType.Raise,
                    CardsInPlay = new CardGrouping(_currentBoardState.OwnHand
                            .ToArray()
                            .Where(x => x.Rank == CardRank.Ace || x.Rank >= CardRank.Jack)
                            .ToArray()
                        )
                };
            }
        }

        public static GroupingType[] SomePossibleGroupingTypes(OwnedBoardState boardState)
        {
            CardGrouping handAsGroup = new(boardState.OwnHand.ToArray());
            List<GroupingType> validTypes = new();
            if(handAsGroup.PictureCards == 5 && handAsGroup.CardsOfRank(CardRank.Ace)==5)
            {
                validTypes.Add(GroupingType.GreaterOnion);
            }
            if(handAsGroup.PictureCards==4 && handAsGroup.CardsOfRank(CardRank.Ace) == 4)
            {
                validTypes.Add(GroupingType.LesserOnion);
            }
            if (handAsGroup.PictureCards == 3 && handAsGroup.CardsOfRank(CardRank.Ace) == 3)
            {
                validTypes.Add(GroupingType.TripleOnion);
            }
            if (handAsGroup.PictureCards == 2 && handAsGroup.CardsOfRank(CardRank.Ace) == 2)
            {
                validTypes.Add(GroupingType.DoubleOnion);
            }
            if(handAsGroup.CanAddTo(21, 7))
            {
                validTypes.Add(GroupingType.SevenCardOnion);
            }
            if (handAsGroup.CanAddTo(21, 6))
            {
                validTypes.Add(GroupingType.SixCardOnion);
            }
            if (handAsGroup.CardsOfRank(CardRank.Seven) == 3)
            {
                validTypes.Add(GroupingType.Royal);
            }
            if (handAsGroup.CanAddTo(21, 5))
            {
                validTypes.Add(GroupingType.FiveCardOnion);
            }
            if (handAsGroup.CanAddTo(21, 4))
            {
                validTypes.Add(GroupingType.FourCardOnion);
            }
            if (handAsGroup.CanAddTo(21, 3))
            {
                validTypes.Add(GroupingType.ThreeCardOnion);
            }
            if (handAsGroup.CanAddTo(21, 2))
            {
                validTypes.Add(GroupingType.TwoCardOnion);
            }
            if (handAsGroup.CanAddTo(20, 2))
            {
                validTypes.Add(GroupingType.Bagel);
            }

            return validTypes.ToArray();
        }
    }
}
