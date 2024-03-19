using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.States
{
    public class Menu : IState
    {
        public StateTransition Run(StateTransition transition)
        {
            return new StateTransition
            {
                NextState = State.Play
            };
        }
    }
}
