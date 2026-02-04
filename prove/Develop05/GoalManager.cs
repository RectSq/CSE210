using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        while (true)
        {
            DisplayPlayerInfo();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoalDetails();
            else if (choice == "3") SaveGoals();
            else if (choice == "4") LoadGoals();
            else if (choice == "5") RecordEvent();
            else if (choice == "6") break;
            else Console.WriteLine("Invalid choice. Please try again.");
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine();
        Console.WriteLine($"You have {_score} points.");
        
        string rank = "Novice";
        if (_score > 500) rank = "Apprentice";
        if (_score > 1000) rank = "Expert";
        if (_score > 2000) rank = "Master";
        
        Console.WriteLine($"Current Rank: {rank}");
        Console.WriteLine();
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string desc = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        string points = Console.ReadLine();

        if (type == "1")
        {
            _goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (type == "2")
        {
            _goals.Add(new EternalGoal(name, desc, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }

        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < _goals.Count)
        {
            Goal goal = _goals[index];
            
            goal.RecordEvent();

            int pointsEarned = goal.GetPoints();

            // Check for bonus if it's a checklist goal
            if (goal is ChecklistGoal checklistGoal)
            {
                 pointsEarned += checklistGoal.GetBonus();
            }

            _score += pointsEarned;

            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
            Console.WriteLine($"You now have {_score} points.");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // data we're writing to the file
            outputFile.WriteLine(_score); 

            //loops over the list of goals
            foreach (Goal goal in _goals)
            {
                //writes to the file
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            _score = int.Parse(lines[0]); // Restore score
            _goals.Clear(); // Clear current goals to avoid duplicates

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(":");
                string type = parts[0];
                string[] data = parts[1].Split("|");

                if (type == "SimpleGoal")
                {
                    SimpleGoal sg = new SimpleGoal(data[0], data[1], data[2]);
                    if (bool.Parse(data[3])) sg.RecordEvent(); // Mark complete if saved that way
                    _goals.Add(sg);
                }
                else if (type == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(data[0], data[1], data[2]));
                }
                else if (type == "ChecklistGoal")
                {
                    ChecklistGoal cg = new ChecklistGoal(data[0], data[1], data[2], int.Parse(data[4]), int.Parse(data[3]));
                    cg.SetCurrentAmount(int.Parse(data[5]));
                    _goals.Add(cg);
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}