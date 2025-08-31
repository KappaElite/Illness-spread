using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication.Simulation;

public class Caretaker
{
    private List<Memento> _mementos = new List<Memento>();

    public void addMemento(Memento memento)
    {
        _mementos.Add(memento);
    }
    public Memento getMemento(int index)
    {
        return _mementos[index];
    }
    
    public Memento getLast()
    {
        if (_mementos.Any())
        {
            return _mementos.Last();
        }
        return null;
    }
}