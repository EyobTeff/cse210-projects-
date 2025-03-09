
using System;


class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int input;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        do
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());

            if (input != 0)
            {
                numbers.Add(input);
            }
        } while (input != 0);

        
        int sum = numbers.Sum();
        Console.WriteLine($"The sum is: {sum}");

        
        double average = numbers.Average();
        Console.WriteLine($"The average is: {average}");

        
        int max = numbers.Max();
        Console.WriteLine($"The largest number is: {max}");

        
        if (numbers.Any(n => n > 0))
        {
    
            int smallestPositive = numbers.Where(n => n > 0).Min();
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }
        
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
