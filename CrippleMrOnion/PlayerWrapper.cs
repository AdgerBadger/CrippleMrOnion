using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrippleMrOnion.Data;

namespace CrippleMrOnion
{
    public interface IController
    {
        public Card[] InitialDeal(IEnumerable<Card> cards);
        public void FullDeal(IEnumerable<Card> cards);
        public void NewTurn(OwnedBoardState boardState);
        public Move AttemptTurn();
        public Move? OtherPlayTurn(int playerNo, Move otherPlayerPlay);
    }
    public class PlayerWrapper
    {
        public IController Controller { get; init; }
        public Hand Hand = new();
        public int TriesTillInvalid = 3;
        public PlayerWrapper(IController controller)
        {
            Controller = controller;
        }

        public Card[] InitialDeal(IEnumerable<Card> cards)
        {
            Hand.AddRange(cards);
            for (int triesLeft = TriesTillInvalid; triesLeft != 0; triesLeft--)
            {
                Card[] cardsToReplace = Controller.InitialDeal(cards);
                bool valid = true;
                for (int i = 0; i < cardsToReplace.Length && valid; i++)
                {
                    valid = Hand.Contains(cardsToReplace[i]);
                }

                if (valid)
                {
                    return cardsToReplace;
                }
            }

            return Array.Empty<Card>();
        }

        public void SecondDeal(IEnumerable<Card> cards)
        {
            Hand.AddRange(cards);
            Controller.FullDeal((List<Card>)Hand);
        }

        public Move Turn(OwnedBoardState boardState)
        {
            Controller.NewTurn(boardState);
            for (int triesLeft = TriesTillInvalid; triesLeft != 0; triesLeft--)
            {
                Move turnAttempt = Controller.AttemptTurn();
                if (turnAttempt.Type == MoveType.Fold)
                {
                    return turnAttempt;
                }
                else if (turnAttempt.Type == MoveType.Raise && turnAttempt.CardsInPlay != null)
                {
                    return turnAttempt;
                }
            }
            return new Move
            {
                Type = MoveType.Fold,
            };
        }

        public Move? OtherPlayerTurn(int playerNo, Move otherPlayerPlay)
        {
            for (int triesLeft = TriesTillInvalid; triesLeft != 0; triesLeft--)
            {
                Move? turnAttempt = Controller.OtherPlayTurn(playerNo, otherPlayerPlay);
                if (
                    turnAttempt != null &&
                    otherPlayerPlay.Type == MoveType.Raise &&
                    turnAttempt.Type == MoveType.Interrupt &&
                    (
                        (
                            (
                                otherPlayerPlay.CardsInPlay!.Type == GroupingType.LesserOnion ||
                                otherPlayerPlay.CardsInPlay!.Type == GroupingType.GreaterOnion ||
                                otherPlayerPlay.CardsInPlay!.Type == GroupingType.NineCardRunning
                            ) &&
                            turnAttempt.CardsInPlay!.Type != GroupingType.TenCardRunning
                        ) ||
                        (
                            otherPlayerPlay.CardsInPlay!.Type == GroupingType.TenCardRunning &&
                            turnAttempt.CardsInPlay!.Type != GroupingType.NineCardRunning
                        )
                    )
                    )
                {
                    return turnAttempt;
                }
            }
            return null;
        }
    }
}
