using System;

public class GratitudeActivity : Activity
{
    public GratitudeActivity()
        : base("Gratitude Activity", "This activity helps you think of things you're grateful for.") {}

    public override void Run()
    {
        DisplayStartMessage();
        Console.WriteLine("\nWrite things you are grateful for:");

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        int count = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou entered {count} things you are grateful for.");
        DisplayEndMessage();
        ActivityLog.Add("Gratitude Activity", _duration);
    }
}
