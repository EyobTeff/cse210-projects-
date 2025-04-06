using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() 
        : base("Breathing Activity", "This activity helps you relax through slow breathing.") {}

    public override void Run()
    {
        DisplayStartMessage();
        int cycle = _duration / 6;

        for (int i = 0; i < cycle; i++)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(3);
            Console.WriteLine();

            Console.Write("Breathe out... ");
            ShowCountdown(3);
            Console.WriteLine();
        }

        DisplayEndMessage();
        ActivityLog.Add("Breathing Activity", _duration);
    }
}
