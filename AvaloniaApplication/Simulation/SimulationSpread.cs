using AvaloniaApplication.person;
using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaApplication.person.states;

namespace AvaloniaApplication.Simulation;

public class SimulationSpread
{
    private Random random;
    private double _timeElapsed = 0;
    private int _n;
    private int _m;
    private int _amountOfPopulation;
    private List<Person> _population;
    private int _timeOfSimulation;
    private double deltaStep = 1.0 / 25;
    public bool IsSimulationComplete { get; private set; }
   

    public SimulationSpread(int n, int m, int amountOfPopulation, int timeOfSimulation)
    {
        _timeOfSimulation = timeOfSimulation;
        _n = n;
        _m = m;
        _amountOfPopulation = amountOfPopulation;
        _population = new List<Person>();
        random = new Random();
        initializePopulation();
    }
    
    private void initializePopulation()
    {
        
        double startingX;
        double startingY;
        double startingVelocityX;
        double startingVelocityY;
        
        for (int i = 0; i < _amountOfPopulation; i++)
        {
            startingX = random.NextDouble() * _n;
            startingY = random.NextDouble() * _m;
            startingVelocityX = (random.NextDouble() * 4) - 2;
            startingVelocityY = (random.NextDouble() * 4) - 2;
            while (Math.Sqrt(Math.Pow(startingVelocityX, 2) + Math.Pow(startingVelocityY, 2)) > 2.5 || Math.Sqrt(Math.Pow(startingVelocityX, 2) + Math.Pow(startingVelocityY, 2)) < -2.5)
            {
                startingVelocityX = (random.NextDouble() * 4) - 2;
                startingVelocityY = (random.NextDouble() * 4) - 2;
            }
            _population.Add(new Person(startingX, startingY, startingVelocityX, startingVelocityY, new Healthy()));
        }
    }

    private void updateSimulationOnDeltaTime()
    {
        
        double oldX;
        double oldY;
        double newX;
        double newY;
        for (int i = _population.Count - 1; i >= 0; i--)
        {
            var person = _population[i];
            oldX = person.getPosition().X;
            oldY = person.getPosition().Y;
            newX = oldX + person.getVelocity().X * deltaStep;
            newY = oldY + person.getVelocity().Y * deltaStep;
            
            if (newX < 0 || newX > _n || newY < 0 || newY > _m)
            {
                bool isComingBack = random.Next(2) == 0;
                if (isComingBack) 
                {
                    
                    if (newX < 0 || newX > _n)
                        person.setVelocity(-person.getVelocity().X, person.getVelocity().Y);
                    if (newY < 0 || newY > _m)
                        person.setVelocity(person.getVelocity().X, -person.getVelocity().Y);

                    
                    newX = Math.Max(0, Math.Min(newX, _n));
                    newY = Math.Max(0, Math.Min(newY, _m));
                }
                else
                {
                    
                    _population.RemoveAt(i);
                    continue;
                }
                
            }
            person.setPosition(newX, newY);
        }

        int chanceOfNewPerson = 40;
        int chanceOfIllness = 10;
        bool ifNewPerson = random.Next(100) < chanceOfNewPerson;
        bool ifIllness = random.Next(100) < chanceOfIllness;
        if (ifNewPerson)
        {
            int chanceOfXAxisStart = 50;
            int chanceOfStartAxis = 50;
            
            bool ifXAxis = random.Next(100) < chanceOfXAxisStart;
            bool ifStartAxis = random.Next(100) < chanceOfStartAxis;
            
            double startingX;
            double startingY;
            double startingVelocityX;
            double startingVelocityY;

            if (ifXAxis)
            {
                if (ifStartAxis)
                {
                    startingY = 0;
                    startingX = random.NextDouble() * _n;
                    
                    startingVelocityY = Math.Abs((random.NextDouble() * 2) + 0.5); 
                }
                else
                {
                    startingY = _m;
                    startingX = random.NextDouble() * _n;
                    
                    startingVelocityY = -Math.Abs((random.NextDouble() * 2) + 0.5); 
                }
                
                startingVelocityX = (random.NextDouble() * 4) - 2; 
            }
            else
            {
                if (ifStartAxis)
                {
                    startingY = random.NextDouble() * _m;
                    startingX = 0;
                    
                    startingVelocityX = Math.Abs((random.NextDouble() * 2) + 0.5); 
                }
                else
                {
                    startingY = random.NextDouble() * _m;
                    startingX = _n;
                    startingVelocityX = -Math.Abs((random.NextDouble() * 2) + 0.5); 
                }
                
                startingVelocityY = (random.NextDouble() * 4) - 2; 
            }
            
            while (Math.Sqrt(Math.Pow(startingVelocityX, 2) + Math.Pow(startingVelocityY, 2)) > 2.5 || Math.Sqrt(Math.Pow(startingVelocityX, 2) + Math.Pow(startingVelocityY, 2)) < -2.5)
            {
               startingVelocityX = (random.NextDouble() * 4) - 2;
               startingVelocityY = (random.NextDouble() * 4) - 2;
            }

            if (ifIllness)
            {
                _population.Add(new Person(startingX,startingY,startingVelocityX,startingVelocityY,new IllWithSymptoms()) );
            }
            else
            {
                _population.Add(new Person(startingX, startingY, startingVelocityX, startingVelocityY, new Healthy()));
            }
        }


    }

    private void updateSimulationOnSecond()
    {
        int chanceOfChangingVelocity = 5;
        bool ifChangingVelocity;
        double newVelocityX;
        double newVelocityY;
        
        foreach (var person in _population)
        {
            ifChangingVelocity = random.Next(100) < chanceOfChangingVelocity;
            if (ifChangingVelocity)
            {
                newVelocityX = (random.NextDouble() * 4) - 2;
                newVelocityY = (random.NextDouble() * 4) - 2;
                while (Math.Sqrt(Math.Pow(newVelocityX, 2) + Math.Pow(newVelocityY, 2)) > 2.5 || Math.Sqrt(Math.Pow(newVelocityX, 2) + Math.Pow(newVelocityY, 2)) < -2.5)
                {
                    newVelocityX = (random.NextDouble() * 4) - 2;
                    newVelocityY = (random.NextDouble() * 4) - 2;
                }
                person.setVelocity(newVelocityX,newVelocityY);
            }
        }
    }


    private void updateCloseContactTime()
    {
        var sickPeople = _population.Where(person => person.getState() is IllWithSymptoms || person.getState() is IllWithoutSymptoms).ToList();
        var healthyPeople = _population.Where(person => person.getState() is Healthy).ToList();

        foreach (var sick in sickPeople)
        {
            bool hasSymptoms = sick.getState() is IllWithSymptoms;
            
            sick.increaseTimeOfInfection();
            
            
            foreach (var healthy in healthyPeople)
            {
                double distance = sick.calculateDistance(healthy);

                if (distance < 2 && healthy.getInfectedNearby() == 0)
                {
                    if (hasSymptoms)
                    {
                        healthy.setTypeOfInfected(1);
                    }
                    else
                    {
                        healthy.setTypeOfInfected(0);
                    }
                    healthy.increaseTimeWithInfected();
                    healthy.increaseInfectedNearby();
                }
            }
        }

        foreach (var healthy in healthyPeople)
        {
            if (healthy.getInfectedNearby() == 0 && healthy.getTimeWithInfected() > 0)
            {
                healthy.resetTimeWithInfected();
            }
            if(healthy.getInfectedNearby() > 0)
            {
                healthy.resetInfectedNearby();
            }
        }
        

    }
    
    
    public void startSimulation() //Metoda do sprawdzania konsolowego
    {
        Console.WriteLine($"Starting population: {_amountOfPopulation}, time of simulation {_timeOfSimulation}");
        for (double i = 0; i < _timeOfSimulation; i+= 0.04)
        {
            updateSimulationOnDeltaTime();
            updateCloseContactTime();
            if (i == Math.Truncate(i))
            {
                updateSimulationOnSecond();
            }
            int sickCount = _population.Count(person => person.getState() is IllWithSymptoms || person.getState() is IllWithoutSymptoms);      
            int healthyCount = _population.Count(person => person.getState() is Healthy);
            Console.WriteLine($"Time: {i:F2}, Healthy people: {healthyCount}, Infected people: {sickCount}");
        }
    }


    public List<Person> GetPopulation()
    {
        return _population;
    }
    
    
    public void Update()
    {
        
        updateSimulationOnDeltaTime();
        updateCloseContactTime();
        _timeElapsed += 0.04; 
        
        if (_timeElapsed == Math.Truncate(_timeElapsed))
        {
            updateSimulationOnSecond();
        }
        
        if(_timeElapsed > _timeOfSimulation)
        {
            IsSimulationComplete = true;
        }
    }

    public double getElapsedTime()
    {
        return _timeElapsed;
    }
    
    public Memento saveStateToMemento()
    {
        return new Memento(_population, _timeElapsed, IsSimulationComplete);
    }
    
    public void restoreStateFromMemento(Memento memento)
    {
        _population = memento.getPopulation(); 
        _timeElapsed = memento.getTimeElapsed();
        IsSimulationComplete = memento.getIsSymulationComplete();
    }
    
    
    
    
    
    
    
}