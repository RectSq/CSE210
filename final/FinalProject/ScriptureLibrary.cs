using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureApp
{
    public class ScriptureLibrary
    {
        public List<Scripture> Scriptures { get; private set; } = new List<Scripture>();

        public void LoadFromFile(string filePath)
        {
            Scriptures.Clear(); 
            if (!File.Exists(filePath)) 
                throw new FileNotFoundException($"The file '{filePath}' could not be found.");
            
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    Scriptures.Add(new Scripture(parts[0], parts[1]));
                }
            }
        }

        public void LoadWeights(string baseFilePath)
        {
            string weightFilePath = baseFilePath + ".weights.csv";
            if (!File.Exists(weightFilePath)) return;

            var lines = File.ReadAllLines(weightFilePath);
            var weightMap = new Dictionary<string, int>();

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int savedWeight))
                {
                    string reference = parts[0].Trim('"');
                    weightMap[reference] = savedWeight;
                }
            }

            foreach (var scripture in Scriptures)
            {
                if (weightMap.TryGetValue(scripture.Reference, out int w))
                {
                    scripture.Weight = w;
                }
            }
        }

        public void SaveWeights(string baseFilePath)
        {

            //creates the file as a csv useful for graphing and since we automatically read txt files we'd like to not try to pull the created files into that
            string weightFilePath = baseFilePath + ".weights.csv";
            using (StreamWriter writer = new StreamWriter(weightFilePath))
            {
                
                
                writer.WriteLine("Reference,Weight");
                
                foreach (var scripture in Scriptures)
                {
                // We wrap the reference in quotes ("") just in case the 
                // reference  itself contains a comma (like "Alma 12:1, 4")
                    writer.WriteLine($"\"{scripture.Reference}\",{scripture.Weight}");
                }
            }
        }

        public void LogSelection(Scripture scripture, string logPath = "StudyLog.csv")
        {
            bool fileExists = File.Exists(logPath);
            
            using (StreamWriter writer = new StreamWriter(logPath, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Date,Time,Reference");
                }
                
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd},{DateTime.Now:HH:mm},\"{scripture.Reference}\"");
            }
        }
    }
}