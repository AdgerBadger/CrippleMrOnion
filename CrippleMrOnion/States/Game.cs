using CrippleMrOnion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.States
{
    public class Game : IState
    {
        public PlayerWrapper[] Players;
        public int NoOfPlayers = 4;
        public Pack FullPack = new();
        public StateTransition Run(StateTransition transition)
        {
            Players = new PlayerWrapper[NoOfPlayers];
            Players[0] = new PlayerWrapper(new Controllers.Human());
            for(int i = 1; i < Players.Length; i++)
            {
                Players[i] = new PlayerWrapper(new Controllers.Bot());
            }
            for(int i = 0; i < Players.Length; i++)
            {
                Card[] deal = FullPack.Pick(5);
                Card[] reDraw = Players[i].InitialDeal(deal);

            }
            return new StateTransitions.ToEndState
            {
                NextState = State.End,
                PreviousState = State.Game
            };
        }
    }
}
