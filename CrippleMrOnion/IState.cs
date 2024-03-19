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
        Play,
        End
    }

    public struct StateTransition
    {
        public State NextState;

    }
    public interface IState
    {
        public StateTransition Run(StateTransition transition);
    }
}
