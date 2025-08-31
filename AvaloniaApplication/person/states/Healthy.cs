using System;
using System.Threading.Channels;

namespace AvaloniaApplication.person.states;

public class Healthy:IState
{
    public IState getState()
    {
        return this;
    }

    public void handle(Person person)
    {
        if (person.getTimeWithInfected() > 3)
        {   
            int chanceOfSymptoms = 80;
            bool ifSymptoms = Random.Shared.Next(0, 100) < chanceOfSymptoms;
            Console.WriteLine("Zmiana stanu na chorego");
            if (person.getTypeOfInfected() == 1)
            {
                if (ifSymptoms)
                {
                    person.setState(new IllWithSymptoms());
                }
                else
                {
                    person.setState(new IllWithoutSymptoms());
                }
                
            }
            else
            {
                int chanceOfBecomingIll = 50;
                bool ifBecomeIll = Random.Shared.Next(0, 100) < chanceOfBecomingIll;
                
                if (ifBecomeIll)
                {
                    if (ifSymptoms)
                    {
                        person.setState(new IllWithSymptoms());
                    }
                    else
                    {
                        person.setState(new IllWithoutSymptoms());
                    }
                    
                }
            }

        }
    }
}