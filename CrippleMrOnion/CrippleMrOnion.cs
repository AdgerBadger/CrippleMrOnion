namespace CrippleMrOnion
{
    public class CrippleMrOnion
    {
        Dictionary<State, IState> States = new(new KeyValuePair<State, IState>[]
        {
            new(State.Menu, new States.Menu()),
            new(State.Game, new States.Game())
        });

        public CrippleMrOnion() { }
        
        public StateTransition Play()
        {
            throw new NotImplementedException();
        }
    }
}
