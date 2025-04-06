using System;
using System.Collections.Generic;

public static class ActivityLog
{
    private static List<string> _log = new List<string>();

    public static void Add(string activity, int duration)
    {
        _log.Add($"{activity} - {duration} seconds - {DateTime.Now}");
    }

    public static void Show()
    {
        Console.WriteLine("\n--- Activity Log ---");
        foreach (var entry in _log)
        {
            Console.WriteLine(entry);
        }
    }
}
