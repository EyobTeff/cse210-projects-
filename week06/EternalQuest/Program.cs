using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base Goal class
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;
        protected bool _isComplete;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _isComplete = false;
        }

        public abstract int RecordEvent();
        public abstract string GetProgress();
        public abstract string GetStringRepresentation();

        public string GetName() => _name;
        public string GetDescription() => _description;
        public bool IsComplete() => _isComplete;
        public int GetPoints() => _points;

        public virtual void DisplayGoal()
        {
            string status = _isComplete ? "[X]" : "[ ]";
            Console.WriteLine($"{status} {_name} ({_description})");
        }
    }

    // Simple Goal class - one-time completion
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return _points;
            }
            return 0;
        }

        public override string GetProgress()
        {
            return _isComplete ? "Completed" : "Not Completed";
        }

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal:{_name}|{_description}|{_points}|{_isComplete}";
        }
    }

    // Eternal Goal class - never completes, gives points each time
    public class EternalGoal : Goal
    {
        private int _timesCompleted;

        public EternalGoal(string name, string description, int points) 
            : base(name, description, points)
        {
            _timesCompleted = 0;
        }

        public override int RecordEvent()
        {
            _timesCompleted++;
            return _points;
        }

        public override string GetProgress()
        {
            return $"Completed {_timesCompleted} times";
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{_name}|{_description}|{_points}|{_timesCompleted}";
        }

        public override void DisplayGoal()
        {
            Console.WriteLine($"[∞] {_name} ({_description}) - Completed {_timesCompleted} times");
        }
    }

    // Checklist Goal class - must be completed X times for bonus
    public class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _targetCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints) 
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _timesCompleted = 0;
        }

        public override int RecordEvent()
        {
            _timesCompleted++;
            if (_timesCompleted >= _targetCount)
            {
                _isComplete = true;
                return _points + _bonusPoints;
            }
            return _points;
        }

        public override string GetProgress()
        {
            return $"Completed {_timesCompleted}/{_targetCount} times";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_name}|{_description}|{_points}|{_timesCompleted}|{_targetCount}|{_bonusPoints}";
        }

        public override void DisplayGoal()
        {
            string status = _isComplete ? "[X]" : "[ ]";
            Console.WriteLine($"{status} {_name} ({_description}) - Completed {_timesCompleted}/{_targetCount} times");
        }
    }

    // Goal Manager class to handle all operations
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;
        private int _level;
        private int _xpToNextLevel;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
            _level = 1;
            _xpToNextLevel = 1000;
        }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void RecordGoalEvent(int goalIndex)
        {
            if (goalIndex >= 0 && goalIndex < _goals.Count)
            {
                int pointsEarned = _goals[goalIndex].RecordEvent();
                _score += pointsEarned;
                Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");

                // Check for level up
                if (_score >= _xpToNextLevel)
                {
                    _level++;
                    int excess = _score - _xpToNextLevel;
                    _xpToNextLevel = (int)(_xpToNextLevel * 1.5);
                    _score = excess;
                    Console.WriteLine($"LEVEL UP! You are now level {_level}!");
                    Console.WriteLine($"Next level at {_xpToNextLevel} points.");
                }
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }

        public void DisplayGoals()
        {
            Console.WriteLine("\nYour Goals:");
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals have been created yet.");
                return;
            }

            for (int i = 0; i < _goals.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _goals[i].DisplayGoal();
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"\nCurrent Score: {_score} points");
            Console.WriteLine($"Level: {_level}");
            Console.WriteLine($"Progress to next level: {_score}/{_xpToNextLevel} points");
        }

        public void SaveGoals(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                // Save user progress first
                outputFile.WriteLine($"UserProgress|{_score}|{_level}|{_xpToNextLevel}");

                // Save each goal
                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
        }

        public void LoadGoals(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("No saved goals found.");
                return;
            }

            _goals.Clear();
            string[] lines = File.ReadAllLines(filename);

            // First line is user progress
            if (lines.Length > 0)
            {
                string[] progressParts = lines[0].Split('|');
                if (progressParts[0] == "UserProgress" && progressParts.Length == 4)
                {
                    _score = int.Parse(progressParts[1]);
                    _level = int.Parse(progressParts[2]);
                    _xpToNextLevel = int.Parse(progressParts[3]);
                }
            }

            // Remaining lines are goals
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                string goalType = parts[0];

                switch (goalType)
                {
                    case "SimpleGoal":
                        SimpleGoal simpleGoal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                        if (bool.Parse(parts[4])) simpleGoal.RecordEvent(); // Mark complete if saved as complete
                        _goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        EternalGoal eternalGoal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                        for (int j = 0; j < int.Parse(parts[4]); j++) eternalGoal.RecordEvent(); // Record previous completions
                        _goals.Add(eternalGoal);
                        break;
                    case "ChecklistGoal":
                        ChecklistGoal checklistGoal = new ChecklistGoal(
                            parts[1], parts[2], int.Parse(parts[3]), 
                            int.Parse(parts[5]), int.Parse(parts[6]));
                        for (int j = 0; j < int.Parse(parts[4]); j++) checklistGoal.RecordEvent(); // Record previous completions
                        _goals.Add(checklistGoal);
                        break;
                }
            }
        }

        public void CreateNewGoal()
        {
            Console.WriteLine("\nThe types of Goals are:");
            Console.WriteLine("1. Simple Goal (one-time completion)");
            Console.WriteLine("2. Eternal Goal (never ending)");
            Console.WriteLine("3. Checklist Goal (requires multiple completions)");
            Console.Write("Which type of goal would you like to create? ");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();
            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();
            Console.Write("What is the amount of points associated with this goal? ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case 2:
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case 3:
                    Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("What is the bonus for accomplishing it that many times? ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("Goal added successfully!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();
            string filename = "goals.txt";

            Console.WriteLine("Welcome to Eternal Quest - Goal Tracker!");

            // Load existing goals if available
            goalManager.LoadGoals(filename);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. Show Score");
                Console.WriteLine("7. Quit");
                Console.Write("Select a choice from the menu: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        goalManager.CreateNewGoal();
                        break;
                    case 2:
                        goalManager.DisplayGoals();
                        break;
                    case 3:
                        goalManager.SaveGoals(filename);
                        Console.WriteLine("Goals saved successfully!");
                        break;
                    case 4:
                        goalManager.LoadGoals(filename);
                        Console.WriteLine("Goals loaded successfully!");
                        break;
                    case 5:
                        goalManager.DisplayGoals();
                        Console.Write("Which goal did you accomplish? ");
                        int goalIndex = int.Parse(Console.ReadLine()) - 1;
                        goalManager.RecordGoalEvent(goalIndex);
                        break;
                    case 6:
                        goalManager.DisplayScore();
                        break;
                    case 7:
                        running = false;
                        Console.WriteLine("Goodbye! Don't forget to save your goals!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}