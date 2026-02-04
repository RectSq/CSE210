using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Build and propagate list
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("Red", 4));
        shapes.Add(new Rectangle("Blue", 5, 10));
        shapes.Add(new Circle("Green", 3));

        foreach (Shape s in shapes)
        {
            string color = s.GetColor();
            double area = s.GetArea();

            Console.WriteLine($"The {color} shape has an area of {area:F2}.");
        }
    }
}