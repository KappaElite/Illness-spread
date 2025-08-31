namespace AvaloniaApplication.person;

public interface IState
{
    public IState getState();

    public void handle(Person person);
}