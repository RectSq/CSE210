using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptureLibrary library = new ScriptureLibrary();
            SelectionEngine engine = new SelectionEngine();
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Scripture App Main Menu ===");
                Console.WriteLine("1. Select a file and study");
                Console.WriteLine("2. Add a new scripture .txt file");
                Console.WriteLine("3. Remove an existing file");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    RunStudySession(library, engine, appDirectory);
                }
                else if (choice == "2")
                {
                    AddScriptureFile(appDirectory);
                }
                else if (choice == "3")
                {
                    RemoveScriptureFile(appDirectory);
                }
                else if (choice == "4")
                {
                    break;
                }
            }
        }

        static void RunStudySession(ScriptureLibrary library, SelectionEngine engine, string appDirectory)
        {
            string[] txtFiles = Directory.GetFiles(appDirectory, "*.txt");

            if (txtFiles.Length == 0)
            {
                Console.WriteLine("\nNo .txt files found. Please add one first.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nAvailable Files:");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
            }
            Console.WriteLine("0: Back To Menu");
            int selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex >= txtFiles.Length)
            {
                Console.Write("Enter the number of the file to load: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        // Returns to the main menu
                        return;
                    }
                    else if (choice > 0 && choice <= txtFiles.Length)
                    {
                        //selects file
                        selectedIndex = choice - 1;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please choose a valid number.");
                    }                }

            }

            string selectedFile = txtFiles[selectedIndex];

            try
            {
                library.LoadFromFile(selectedFile);
                library.LoadWeights(selectedFile);

                while (true)
                {
                    Console.WriteLine("\nPress [Enter] to draw a scripture, or type 'exit' to return to menu:");
                    if (Console.ReadLine()?.ToLower() == "exit") break;

                    Scripture selected = engine.GetNext(library.Scriptures);
                    library.SaveWeights(selectedFile);
                    library.LogSelection(selected);

                    Console.Clear();
                    Console.WriteLine("=== YOUR SCRIPTURE ===");
                    Console.WriteLine(selected.ToString());
                    Console.WriteLine("======================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ReadLine();
            }
        }

        static void AddScriptureFile(string appDirectory)
        {
            Console.WriteLine("\nEnter the full file path of the .txt file you want to add:");
            string sourcePath = Console.ReadLine()?.Trim('"');

            if (File.Exists(sourcePath) && Path.GetExtension(sourcePath).ToLower() == ".txt")
            {
                string fileName = Path.GetFileName(sourcePath);
                string destPath = Path.Combine(appDirectory, fileName);

                try
                {
                    File.Copy(sourcePath, destPath, overwrite: true);
                    Console.WriteLine($"Successfully added {fileName} to the library.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to copy file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid file path or the file is not a .txt file.");
            }
            Console.ReadLine();
        }

        static void RemoveScriptureFile(string appDirectory)
        {
            string[] txtFiles = Directory.GetFiles(appDirectory, "*.txt");

            if (txtFiles.Length == 0)
            {
                Console.WriteLine("\nNo .txt files to remove.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nSelect a file to remove:");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
            }

            Console.Write("Enter the number of the file to delete (or 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= txtFiles.Length)
            {
                string fileToDelete = txtFiles[choice - 1];
                string weightsToDelete = fileToDelete + ".weights.csv";

                try
                {
                    File.Delete(fileToDelete);
                    if (File.Exists(weightsToDelete))
                    {
                        File.Delete(weightsToDelete);
                    }
                    Console.WriteLine($"{Path.GetFileName(fileToDelete)} and its weight data have been removed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                }
            }
            Console.ReadLine();
        }
    }
}