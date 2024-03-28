/*
   Corbin Meacham
   Week 05 Program for Polymorphism - Designing a Eternal Quest

   Description:
   - So I originally only worked on a section to keep track of the user points
   This would help the user know how many total points they had completed and from
   the lists they could see how many goals had been accomplished. 
   - Then I wanted to add some levels that scaled a bit (doubled in point requirements every level) as they user worked on goals
   So I created an experiance system to keep track of user points and notify the user when they level up
   They can also check the current level and next level requirements in the Total Points selection screen
*/

using System;
using System.Collections.Generic;
using System.IO;

// Base class for all activities
class Activity
{
    protected string name;
    protected int completedCount;
    protected int targetCount;

    public string Name => name;
    public virtual int CompletedCount => completedCount;
    public int TargetCount => targetCount;
    public int Points { get; protected set; }

    public Activity(string name, int points)
    {
        this.name = name;
        this.Points = points;
    }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {name}. Points gained: {Points}");
        IncrementCompletedCount();
    }

    public virtual void DisplayStatus()
    {
        Console.WriteLine($"Activity: {name}");
    }

    protected void IncrementCompletedCount()
    {
        completedCount++;
    }
}

// Simple Goals
class SimpleGoal : Activity
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        completedCount++;
        Console.WriteLine($"Event recorded for {name}. Points gained: {Points}");
    }
}

// Eternal Goals
class EternalGoal : Activity
{
    private int completionCount;

    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        completionCount++;
        Console.WriteLine($"Event recorded for {name}. Points gained: {Points}");
    }

    public override int CompletedCount => completionCount;
}

// Checklist Goals
class ChecklistGoal : Activity
{
    private int bonusPoints;

    public int BonusPoints => bonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
    }
    public override void RecordEvent()
    {
        completedCount++;
        int totalPointsEarned = Points + (completedCount == targetCount ? bonusPoints : 0);
        Program.UpdateTotalPoints(totalPointsEarned); // Update total points
        Console.WriteLine($"Event recorded for {name}. Points gained: {Points}");
        if (completedCount >= targetCount)
        {
            Console.WriteLine($"Total points earned: {totalPointsEarned}");
            Console.WriteLine($"Goal {name} is completed!");
            Console.WriteLine($"Bonus of {bonusPoints} Earned");
        }
    }

public static class GlobalState
{
    public static int TotalPoints { get; set; }
}

class Program
{
    static List<Activity> goals = new List<Activity>();
    static int totalPoints = 0;
    static int userLevel = 1;

    static void Main(string[] args)
    {
        bool quit = false;
        while (!quit)
        {
            Console.WriteLine("\nEternal Quest - Main Menu");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Display Total Points");
            Console.WriteLine("7. Quit");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateNewGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    DisplayTotalPoints();
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

// Create New Goals
    static void CreateNewGoal()
    {
        Console.WriteLine("\nSelect the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        int points;
        bool validPoints = false;
        do
        {
            Console.Write("Enter points: ");
            string pointsInput = Console.ReadLine();
            validPoints = int.TryParse(pointsInput, out points);
            if (!validPoints)
            {
                Console.WriteLine("Invalid entry. Please enter a valid number for points.");
            }
        } while (!validPoints);

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, points));
                break;
            case "3":
                int targetCount = 1;
                Console.Write("Enter target count for the checklist goal: ");
                while (!int.TryParse(Console.ReadLine(), out targetCount))
                {
                    Console.WriteLine("Invalid entry. Please enter a valid number for target count.");
                    Console.Write("Enter target count for the checklist goal: ");
                }
                int bonusPoints;
                Console.Write("Enter bonus points for completing the checklist goal: ");
                while (!int.TryParse(Console.ReadLine(), out bonusPoints))
                {
                    Console.WriteLine("Invalid entry. Please enter a valid number for bonus points.");
                    Console.Write("Enter bonus points for completing the checklist goal: ");
                }
                goals.Add(new ChecklistGoal(name, points, targetCount, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid choice. Goal creation canceled.");
                break;
        }

        Console.WriteLine("New goal created successfully!");
    }

// List Current Goals
    static void ListGoals()
    {
        Console.WriteLine("\nList of Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            string completionStatus = "[ ]";
            string status = "";

            if (goals[i] is SimpleGoal)
            {
                status = $"-- Currently Completed: {goals[i].CompletedCount}/1";
                if (goals[i].CompletedCount >= 1)
                {
                    completionStatus = "[X]";
                }
            }
            else if (goals[i] is ChecklistGoal)
            {
                status = $"-- Currently Completed: {goals[i].CompletedCount}/{goals[i].TargetCount}";
                if (goals[i].CompletedCount >= goals[i].TargetCount)
                {
                    completionStatus = "[X]";
                }
            }
            else if (goals[i] is EternalGoal)
            {
                status = $"-- Currently Completed: {goals[i].CompletedCount}/∞";
                if (goals[i].CompletedCount >= 1)
                {
                    completionStatus = "[X]";
                }
            }

            Console.WriteLine($"{i + 1}. {completionStatus} {goals[i].Name} {status}");
        }

        Console.WriteLine("\nTo mark a goal as completed, record an event for the goal.");
    }

// Save Current Goals
    static void SaveGoals()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                foreach (Activity goal in goals)
                {
                    writer.WriteLine($"{goal.Name}|{goal.Points}|{goal.CompletedCount}");
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

// Load Previous Goals
    static void LoadGoals()
    {
        try
        {
            goals.Clear();

            string[] lines = File.ReadAllLines("goals.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid data format in file: {line}");
                    continue;
                }

                string name = parts[0];
                int points;
                if (!int.TryParse(parts[1], out points))
                {
                    Console.WriteLine($"Invalid points format in file: {line}");
                    continue;
                }

                int completedCount;
                if (!int.TryParse(parts[2], out completedCount))
                {
                    Console.WriteLine($"Invalid completed count format in file: {line}");
                    continue;
                }

                Activity goal;
                if (completedCount == 0)
                {
                    goal = new SimpleGoal(name, points);
                }
                else if (completedCount == 1)
                {
                    goal = new EternalGoal(name, points);
                }
                else if (completedCount > 1 && parts.Length >= 4)
                {
                    int bonusPoints;
                    if (!int.TryParse(parts[3], out bonusPoints))
                    {
                        Console.WriteLine($"Invalid bonus points format in file: {line}");
                        continue;
                    }

                    goal = new ChecklistGoal(name, points, completedCount, bonusPoints);
                }
                else
                {
                    Console.WriteLine($"Invalid line format in file: {line}");
                    continue;
                }

                goals.Add(goal);
            }

            Console.WriteLine("Goals loaded successfully!");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

// Record Accomplished Goals   
    static void RecordEvent()
        {
        Console.Write("Enter the goal number (index) to record event: ");
        string input = Console.ReadLine();

        int goalIndex;
        if (!int.TryParse(input, out goalIndex) || goalIndex < 1 || goalIndex > goals.Count)
        {
            Console.WriteLine("Invalid input. Please enter the goal number (index) from the list.");
            return;
        }

        Activity selectedGoal = goals[goalIndex - 1];
        Console.WriteLine($"Recording event for goal: {selectedGoal.Name}");

        selectedGoal.RecordEvent();

        Console.WriteLine("Event recorded!");

        if (selectedGoal is SimpleGoal || selectedGoal is EternalGoal)
        {
            Console.WriteLine("Goal updated!");
        }

        UpdateTotalPoints(selectedGoal.Points);
        
        CheckLevelUp();
        }

// Update Total Points to keep track of them 
    public static void UpdateTotalPoints(int pointsEarned)
        {
        totalPoints += pointsEarned;
        Console.WriteLine($"Total Points: {totalPoints}");
        }

// Check Level Up based on Points for user 
    static int previousLevel = 0;

    static void CheckLevelUp()
        {
        int nextMilestone = 100;
        int level = 0;

        while (totalPoints >= nextMilestone)
        {
            level++;
            nextMilestone *= 2;
        }

        userLevel = level;

        if (userLevel > previousLevel)
        {
            Console.WriteLine($"Congratulations! You've reached level {userLevel}!");
            previousLevel = userLevel;
        }
        }

// Display Points and Level Requirements
    static void DisplayTotalPoints()
        {
        Console.WriteLine($"Current Level: {userLevel}");
        int nextMilestone = 100;
        int level = 0;
            
        while (totalPoints >= nextMilestone)
        {
            level++;
            nextMilestone *= 2;
        }

        Console.WriteLine($"Next milestone, level {level + 1}: {nextMilestone} points");
        Console.WriteLine($"Total Points: {totalPoints}");
        }
    }
}