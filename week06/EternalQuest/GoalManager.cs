public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            Goal goal = _goals[goalIndex];
            goal.RecordEvent();

            int points = goal.GetPoints();

            if (goal is ChecklistGoal checklist)
            {
                if (checklist.GetCurrentCount() == checklist.GetTargetCount())
                {
                    points += checklist.GetBonusPoints();
                }
            }

            _score += points;
            Console.WriteLine($"Congrats! You earned {points} points.");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Your current score is: {_score}");
    }

    // Save and Load methods would go here
}
