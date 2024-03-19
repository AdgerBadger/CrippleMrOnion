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
        public State Run(StateTransition transition)
        {
            Players = new PlayerWrapper[NoOfPlayers];
        }
    }
}
