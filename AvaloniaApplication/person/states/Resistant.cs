namespace AvaloniaApplication.person.states;

public class Resistant:IState
{
    public IState getState()
    {
        return this;
    }
    
    public void handle(Person person)
    {
        
    }
}