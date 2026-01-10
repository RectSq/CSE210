using System;

class Program
{
    static void Main(string[] args)
    {
        
        Random numberGenerator = new Random();
        string play;
        int guess;
        do{
        int magicNumber = numberGenerator.Next(1,101);
        
        do
        {
            Console.Write("Guess the Number: ");
            guess = int.Parse(Console.ReadLine());

            if (guess > magicNumber)
            {
                Console.WriteLine("\nTry guessing a bit lower.");
            }
            else if (guess < magicNumber)
            {
                Console.WriteLine("\nTry guessing a bit higher.");
            }

        } while(guess!=magicNumber);
        Console.WriteLine("\n Congratulations You Won!");
        Console.Write("\nDo you want to play again? (y/n) ");
        play = Console.ReadLine();
        } while(play == "y"||play == "yes");
    }
}