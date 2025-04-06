using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "List people you are grateful for.",
        "List your strengths.",
        "List things that made you smile recently."
    };

    public ListingActivity()
        : base("Listing Activity", "This activity helps you reflect by listing positive things.") {}

    public override void Run()
    {
        DisplayStartMessage();
        Random rand = new Random();
        Console.WriteLine($"\n{_prompts[rand.Next(_prompts.Count)]}");
        Console.WriteLine("Start listing items:");

        ShowCountdown(3);
        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            items.Add(input);
        }

        Console.WriteLine($"\nYou listed {items.Count} items.");
        DisplayEndMessage();
        ActivityLog.Add("Listing Activity", _duration);
    }
}
