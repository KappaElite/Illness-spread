# Illness Spread

## Project Description
This **C# application** simulates the spread of an infectious disease within a moving population.  
The simulation models:
- interactions,
- movement,
- infection dynamics over time,  

and provides the ability to **save** and **restore** the simulation state.  

## Design Patterns Used

### **State Pattern**
Used to represent the **health status** of each person:
- `Healthy`  
- `IllWithSymptoms`  
- `IllWithoutSymptoms`  
- `Resistant`  

Each state implements the `IState` interface and encapsulates its own **transition logic**.

---

### **Memento Pattern**
Enables **saving and restoring** the simulation state.  
- The **Memento** class stores snapshots of the population and simulation time.  
- The **Caretaker** manages these snapshots.  

---

## Main Features
- Dynamic simulation of **population movement** and **infection spread**  
- Individual **health state transitions** based on proximity and infection duration  
- Ability to **save** and **restore** simulation progress  
- **Extensible architecture** for adding new states or behaviors  
