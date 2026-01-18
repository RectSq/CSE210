using System;
using System.Collections.Generic;

class Program
{
    private static String[] options = ["Write","Display","Load","Save","Quit"];
    private static String[] prompts = [
        "What did you eat today? ","What was the best part of today? ","How long did all your work take? "
    ];
public class Entry
{
    public String _date;
    public String _promptText;
    public String _entryText;

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine($"{_entryText}\n");
    }
}
public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

public void SaveToFile(string file)
{
    using (StreamWriter outputFile = new StreamWriter(file))
    {
        foreach (Entry entry in _entries)
        {
            // use pipe as separator
            outputFile.WriteLine($"{entry._date}|{entry._promptText}|{entry._entryText}");
        }
    }
    Console.WriteLine("Journal saved successfully!");
}
public void LoadFromFile(string file)
{
    // clear out entries
    _entries.Clear();

    string[] lines = System.IO.File.ReadAllLines(file);

    foreach (string line in lines)
    {
        
        string[] parts = line.Split("|");

        // Create entries.
        Entry newEntry = new Entry();
        newEntry._date = parts[0];
        newEntry._promptText = parts[1];
        newEntry._entryText = parts[2];

        _entries.Add(newEntry);
    }
    Console.WriteLine("Journal loaded successfully!");
}
}
static void Main(string[] args)
{

    Journal theJournal = new Journal();
    Random random = new Random();
    int randomNumber;
    String choice = "";

    //quit if 5
    while (choice != "5")
    {
        Console.WriteLine("Please select one of the following choices:");
        
        //options can easily be added to the list.
        for(int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i+1} {options[i]}");
        }
        Console.Write("What would you like to do? ");
        choice = Console.ReadLine();
        Console.Write("\n");
        if (choice == "1"||choice.ToLower() == options[0])
        {
            randomNumber = random.Next(prompts.Length);
            string prompt = prompts[randomNumber];
            Console.WriteLine(prompt);
            Console.Write("> ");
            
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();

            Entry newEntry = new Entry();
            newEntry._date = date;
            newEntry._promptText = prompt;
            newEntry._entryText = response;

            theJournal.AddEntry(newEntry);
        }
        else if (choice == "2"||choice.ToLower() == options[1])
        {
            theJournal.DisplayAll();
        }
        else if(choice == "3"||choice.ToLower() == options[2])
        {
            Console.WriteLine("What file would you like to load from?");
            choice = Console.ReadLine();
            theJournal.LoadFromFile(choice);
        }
        else if(choice == "4"||choice.ToLower() == options[3])
        {
            Console.WriteLine("What file would you like to save to?");
            choice = Console.ReadLine();
            theJournal.SaveToFile(choice);
        }   
        else if(choice == "5"||choice.ToLower() == options[4])
        {
            break;
        }                     


    }
}
}