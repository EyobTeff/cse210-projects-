using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity (Creative)");
            Console.WriteLine("5. View Activity Log");
            Console.WriteLine("6. Quit");
            Console.Write("\nChoose an option: ");

            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": activity = new GratitudeActivity(); break;
                case "5": ActivityLog.Show(); Console.ReadKey(); continue;
                case "6": return;
                default: Console.WriteLine("Invalid choice."); Thread.Sleep(1000); continue;
            }

            activity?.Run();
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
