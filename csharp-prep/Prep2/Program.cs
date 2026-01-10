using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the grade percentage? ");
        int grade = int.Parse(Console.ReadLine());
        string letterGrade = "";
        int gradeDangle = 0;
        string letterGradeSign = "";

        if(grade >= 90){letterGrade = "A";}
        else if(grade >= 80){letterGrade = "B";}
        else if(grade >= 70){letterGrade = "C";}
        else if(grade >= 60){letterGrade = "D";}
        else if(grade < 60){letterGrade = "F";}

        
        gradeDangle = grade%10;
        if (gradeDangle >= 7&&grade<90&&grade>60)
        {
            letterGradeSign = "+";
        }
        else if (gradeDangle <= 3&&grade>60)
        {
            letterGradeSign = "-";
        }
        
        Console.WriteLine($"\n{letterGrade}{letterGradeSign}");
        if (grade >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Come on. Give it another shot!");
        }

    }
}