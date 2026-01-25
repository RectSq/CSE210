using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string[] lines = System.IO.File.ReadAllLines("Scriptures.txt");
        Reference ref1;
        Scripture scripture;
        string newScriptureText;
        int scriptureSelected = random.Next(lines.Length);
        string[] parts = lines[scriptureSelected].Split("|");

        if(parts.Length==4){
            ref1 = new Reference(parts[0],int.Parse(parts[1]),int.Parse(parts[2]));
        }
        else
        {
            ref1 = new Reference(parts[0],int.Parse(parts[1]),int.Parse(parts[2]),int.Parse(parts[3]));
        }
        newScriptureText = parts[parts.Length-1];
        scripture = new Scripture(ref1, newScriptureText);


        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("");
            Console.WriteLine("Press Enter to continue or type 'quit' to finish, or 'next' for a new scripture:");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }
            if(input.ToLower() == "next")
            {
                scriptureSelected = random.Next(lines.Length);
                parts = lines[scriptureSelected].Split("|");
                
                if(parts.Length==4){
                ref1 = new Reference(parts[0],int.Parse(parts[1]),int.Parse(parts[2]));
                }
                else
                {
                    ref1 = new Reference(parts[0],int.Parse(parts[1]),int.Parse(parts[2]),int.Parse(parts[3]));
                }
                newScriptureText = parts[parts.Length-1];
                scripture = new Scripture(ref1, newScriptureText);
            }
            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}