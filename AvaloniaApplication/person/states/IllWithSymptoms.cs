namespace AvaloniaApplication.person.states;

public class IllWithSymptoms:IState
{
    public IState getState()
    {
        return this;
    }
    
    public void handle(Person person)
    {
        if (person.getTimeOfInfection() > 20)
        {
            person.setState(new Resistant());
        }
    }
}