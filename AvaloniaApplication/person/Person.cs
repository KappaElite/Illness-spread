using System;
using System.Reflection.Metadata;
using AvaloniaApplication.person.states;
using AvaloniaApplication.vectors;

namespace AvaloniaApplication.person;

public class Person
{
    private IState _currentState;
    private Vector2D _position;
    private Vector2D _velocity;
    private double _timeWithInfected;
    private int _infectedNearby;
    private int _typeOfInfected;
    private double _timeOfInfection;

    public Person(double startingX, double startingY, double startingVelocityX, double startingVelocityY, IState state)
    {
        _position = new Vector2D(startingX, startingY);
        _velocity = new Vector2D(startingVelocityX, startingVelocityY);
        _currentState = state;
        _timeWithInfected = 0;
        _infectedNearby = 0;
    }

    public Person clone()
    {
        var clonedPerson = new Person(_position.X, _position.Y, _velocity.X, _velocity.Y, _currentState);
        clonedPerson._timeWithInfected = _timeWithInfected;
        clonedPerson._infectedNearby = _infectedNearby;
        clonedPerson._typeOfInfected = _typeOfInfected;
        clonedPerson._timeOfInfection = _timeOfInfection;
        return clonedPerson;
    }

    public void setPosition(double x, double y)
    {
        _position.X = x;
        _position.Y = y;
    }

    public void setVelocity(double x, double y)
    {
        _velocity.X = x;
        _velocity.Y = y;
    }
    public Vector2D getPosition()
    {
        return _position;
    }
    
    public Vector2D getVelocity()
    {
        return _velocity;
    }

    public void setState(IState state)
    {
        _currentState = state;
    }

    public IState getState()
    {
       return _currentState.getState();
    }

    public void changeState()
    {
        _currentState.handle(this);
    }

    public void increaseTimeWithInfected()
    {
        _timeWithInfected += 0.04;
        changeState();
    }
    public void resetTimeWithInfected()
    {
        _timeWithInfected = 0;
    }
    public double getTimeWithInfected(){
        return _timeWithInfected;
    }
    public void increaseInfectedNearby()
    {
        _infectedNearby++;
    }
    public void resetInfectedNearby()
    {
        _infectedNearby = 0;
    }

    public int getInfectedNearby()
    {
        return _infectedNearby;
    }
    
    public int getTypeOfInfected()
    {
        return _typeOfInfected;
    }
    public void setTypeOfInfected(int typeOfInfected)
    {
        _typeOfInfected = typeOfInfected;
    }
    public void increaseTimeOfInfection()
    {
        _timeOfInfection += 0.04;
        changeState();
    }
    public double getTimeOfInfection()
    {
        return _timeOfInfection;
    }
   

    public double calculateDistance(Person other)
    {
        double dx = _position.X - other.getPosition().X;
        double dy = _position.Y - other.getPosition().Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}