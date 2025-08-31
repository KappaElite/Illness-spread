using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using AvaloniaApplication.person.states;
using AvaloniaApplication.Simulation;

namespace AvaloniaApplication;

public partial class MainWindow : Window
{
    private const double ScaleFactor = 10.0;
    private SimulationSpread _simulation;
    private readonly DispatcherTimer _timer;
    private TextBlock _simulationTimeTextBlock;
    private Caretaker _caretaker;

    public MainWindow()
    {
        InitializeComponent();
        
        _simulation = new SimulationSpread(50, 50, 100, 50);
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(40)
        };
        _timer.Tick += OnSimulationTick;
        _timer.Start();
        _simulationTimeTextBlock = this.FindControl<TextBlock>("SimulationTimeTextBlock");
        _caretaker = new Caretaker();
    }

    private void OnSimulationTick(object sender, EventArgs e)
    {
        if (_simulation.IsSimulationComplete)
        {
            _timer.Stop();
            Console.WriteLine("Simulation Completed");
            return;
        }
        _simulation.Update();
        DrawSimulation();
    }

    private void DrawSimulation()
    {
        var canvas = this.FindControl<Canvas>("SimulationCanvas");
        canvas.Children.Clear();

        foreach (var person in _simulation.GetPopulation())
        {
            var ellipse = new Ellipse
            {
                Fill = person.getState() switch
                {
                    Healthy => Brushes.Green,
                    IllWithSymptoms => Brushes.Red,
                    IllWithoutSymptoms => Brushes.Orange,
                    Resistant => Brushes.Blue,
                    _ => Brushes.Gray
                },
                Width = 5,
                Height = 5
            };

            Canvas.SetLeft(ellipse, person.getPosition().X * ScaleFactor);
            Canvas.SetTop(ellipse, person.getPosition().Y * ScaleFactor);
            canvas.Children.Add(ellipse);
        }
        
        _simulationTimeTextBlock.Text = $"Simulation Time: {_simulation.getElapsedTime():F2} seconds";
    }
    
    private void OnSaveState(object sender, RoutedEventArgs e)
    {
        var memento = _simulation.saveStateToMemento();
        _caretaker.addMemento(memento);
        Console.WriteLine("State saved.");
    }

    private void OnRestoreState(object sender, RoutedEventArgs e)
    {
        var lastMemento = _caretaker.getLast();
        if (lastMemento != null)
        {
            _simulation.restoreStateFromMemento(lastMemento);
            redrawSimulation();
            Console.WriteLine("State restored.");
        }
        else
        {
            Console.WriteLine("No state to restore.");
        }
    }


    private void redrawSimulation()
    {
        DrawSimulation();
    }
}
