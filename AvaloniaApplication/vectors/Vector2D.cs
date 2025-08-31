using System;

namespace AvaloniaApplication.vectors;

public class Vector2D : IVector
{
    public double X
    {
        get;
        set;
    }

    public double Y
    {
        get;
        set;
    }

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public virtual double[] getComponents()
    {
        double[] components = { X, Y };
        return components;
    }

    public virtual double abs()
    {
        return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
    }

    public virtual double cdot(IVector vector)
    {
        double[] components = vector.getComponents();
        return X * components[0] + Y * components[1];
    }

    public Vector2D clone()
    {
        return new Vector2D(X, Y);
    }
}