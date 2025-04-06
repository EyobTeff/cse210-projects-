using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone.",
        "Think of a time you helped someone in need.",
        "Think of a time you overcame a challenge."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful?",
        "What did you learn about yourself?",
        "How can this apply to other areas of your life?"
    };

    public ReflectionActivity()
        : base("Reflection Activity", "This activity helps you reflect on personal strength.") {}

    public override void Run()
    {
        DisplayStartMessage();
        Random rand = new Random();
        Console.WriteLine($"\n{_prompts[rand.Next(_prompts.Count)]}\n");

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write($"> {_questions[rand.Next(_questions.Count)]}\n");
            ShowSpinner(5);
            Console.WriteLine();
        }

        DisplayEndMessage();
        ActivityLog.Add("Reflection Activity", _duration);
    }
}
