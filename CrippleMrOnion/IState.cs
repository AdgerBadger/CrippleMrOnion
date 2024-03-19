using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion
{
    public enum State
    {
        Menu,
        Game,
        End
    }

    public abstract class StateTransition
    {
        public State NextState;
        public State PreviousState;
    }
    public interface IState
    {
        public StateTransition Run(StateTransition transition);
    }
}
