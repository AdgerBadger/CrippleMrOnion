namespace CrippleMrOnion
{
    public class Game
    {
        Dictionary<State, IState> States = new(new KeyValuePair<State, IState>[]
        {
            new(State.Menu, new States.Menu())
            
        });
        
        public StateTransition Play()
        {
            throw new NotImplementedException();
        }
    }
}
