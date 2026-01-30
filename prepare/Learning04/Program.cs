using System;



class Program
{
    static void Main(string[] args)
    {
        MathAssignment assignment = new MathAssignment("Roberto Rodriguez","Fractions","3.7","8-19");
        Console.WriteLine(assignment.GetSummary());
        Console.WriteLine(assignment.GetHomeWorkList());


        WritingAssignment assignment2 = new WritingAssignment("Ferry Hampton","Classical Literature","The many unfortunate accounts of Robert Longsdale");
        Console.WriteLine(assignment2.GetSummary());
        Console.WriteLine(assignment2.GetWritingInformation());
    }
}