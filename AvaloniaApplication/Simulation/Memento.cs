using System.Collections.Generic;
using AvaloniaApplication.person;
namespace AvaloniaApplication.Simulation;

public class Memento
{
    private List<Person> _population;
    private double _timeElapsed;
    private bool _isSymulationComplete;
    
    public Memento(List<Person> population, double timeElapsed, bool isSymulationComplete)
    {
        _population = new List<Person>(population.ConvertAll(person => person.clone()));;
        _timeElapsed = timeElapsed;
        _isSymulationComplete = isSymulationComplete;
    }
    
    public List<Person> getPopulation()
    {
        return new List<Person>(_population.ConvertAll(person => person.clone()));
    }
    public double getTimeElapsed()
    {
        return _timeElapsed;
    }

    public bool getIsSymulationComplete()
    {
        return _isSymulationComplete;
    }
    



}