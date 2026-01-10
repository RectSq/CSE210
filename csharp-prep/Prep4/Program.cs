using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {

        List<int> numbers = new List<int>();
        int sum = 0;
        int max = 0;
        int min = 0;
        float average;

        //do loop
        Console.WriteLine("Enter a list of numbers, type 0 to finished.");
        do
        {
            Console.Write("Enter a Number: ");
            numbers.Add(int.Parse(Console.ReadLine()));

        } while(numbers[numbers.Count-1]!=0);

        //calc
        foreach(int num in numbers)
        {
            sum += num;
            max = Math.Max(max,num);
            min = Math.Min(min,num);

        }
        average = (float)sum/(numbers.Count-1);

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Largest: {max}");
        Console.WriteLine($"Largest: {min}");
    }
}