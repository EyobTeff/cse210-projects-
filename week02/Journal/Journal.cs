using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(string prompt, string response)
    {
        entries.Add(new Entry(prompt, response));
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
            Console.WriteLine(entry);
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
                outputFile.WriteLine($"{entry.Date}~{entry.Prompt}~{entry.Response}");
        }
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split('~');
                if (parts.Length == 3)
                    entries.Add(new Entry(parts[1], parts[2]) { Date = parts[0] });
            }
            Console.WriteLine("Journal loaded successfully!");
        }
        else
            Console.WriteLine("File not found.");
    }
}
