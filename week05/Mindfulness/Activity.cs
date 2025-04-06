using System;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---\n{_description}");
        Console.Write("\nEnter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    public void DisplayEndMessage()
    {
        Console.WriteLine($"\nGreat job! You've completed the {_name} activity for {_duration} seconds.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        string[] spinner = { "/", "-", "\\", "|" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[i++ % spinner.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }

    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b");
        }
    }

    public virtual void Run() { }
}
