/*
   Corbin Meacham
   W04 Mindfulness Reminder Activity

   Description:
   - So I added a section to the menu for #5 to pull up a list of how many times each activity has been completed in that session. 
   This will help the user keep track of how many times each Mindfulness activity had been processed (including random ones). 
   - I also added a section to randomize the 3 main options and choose between each one. 
   This section has a shuffle set up, where the random (#4) option will cycle through all 1, 2, 3, before it repeats any of them. 
*/

using System;
using System.Threading;

abstract class BaseActivity
{
    protected string description;
    protected int duration;

    public BaseActivity(string description)
    {
        this.description = description;
    }

    public virtual void StartActivity()
    {
        Console.WriteLine($"{GetType().Name}");
        Console.WriteLine($"Description: {description}");

        duration = GetDuration();
        DisplayStartingMessage();

        RunActivity();

        DisplayEndingMessage();
    }

    protected abstract void RunActivity();

    protected virtual void DisplayStartingMessage()
    {
        Console.WriteLine($"Get ready to start the {GetType().Name}.");
        Console.WriteLine("Press Enter to begin...");
        Console.ReadLine();
    }

    protected virtual void DisplayEndingMessage()
    {
        Console.WriteLine($"You have completed the {GetType().Name} for {duration} seconds.");
        Pause();
    }

    protected int GetDuration()
{
    int duration;
    bool isValidInput;

    do
    {
        Console.Write("Enter the duration of the activity (in seconds): ");
        string input = Console.ReadLine();

        isValidInput = int.TryParse(input, out duration);

        if (!isValidInput || duration <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
            isValidInput = false;
        }
    }
    while (!isValidInput);

    return duration;
}

    protected void Pause()
    {
        Thread.Sleep(2000);
    }
}

class BreathingActivity : BaseActivity
{
    public BreathingActivity(string description) : base(description)
    {
    }

    protected override void RunActivity()
    {
        for (int i = 0; i < duration; i += 2)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(2000);

            Console.WriteLine("Breathe out...");
            Thread.Sleep(2000);
        }
    }
}

class ReflectionActivity : BaseActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    public ReflectionActivity(string description) : base(description)
    {
    }
protected override void RunActivity()
{
    Random rand = new Random();
    int index = rand.Next(prompts.Length);
    string prompt = prompts[index];

    Console.WriteLine($"Prompt: {prompt}");

    string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    DateTime startTime = DateTime.Now;

    foreach (string question in questions)
    {
        Console.WriteLine(question);
        DisplayLoaderBetweenQuestions();
        
        if (DateTime.Now - startTime >= TimeSpan.FromSeconds(duration))
            break;
    }
}
    private void DisplayLoaderBetweenQuestions()
    {
        Console.Write("Loading"); 

        for (int i = 0; i < 10; i++) 
        {
            Thread.Sleep(100); 
            Console.Write("."); 
        }

        Console.WriteLine(); 
    }
}

class ListingActivity : BaseActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(string description) : base(description)
    {
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        int index = rand.Next(prompts.Length);
        string prompt = prompts[index];

        Console.WriteLine($"Prompt: {prompt}");

        Console.WriteLine("Get ready to list items...");
        for (int i = 3; i > 0; i--)
        {
            Console.WriteLine($"Starting in {i} seconds...");
            Thread.Sleep(1000);
        }

        int itemCount = 0;
        Console.WriteLine("Start listing items (press Enter after each item, or type 'done' to finish):");
        
        DateTime startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "done")
                break;
            itemCount++;
        }
        
        Console.WriteLine($"You listed {itemCount} items during the {GetType().Name}.");
    }
}
class Program
{
static void Main(string[] args)
{
    List<BaseActivity> activities = new List<BaseActivity>
    {
        new BreathingActivity("This activity will help you relax by guiding you through breathing in and out slowly. Clear your mind and focus on your breathing."),
        new ReflectionActivity("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."),
        new ListingActivity("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    };

    List<int> shuffledIndices = new List<int> { 1, 2, 3 };
    Shuffle(shuffledIndices);

    int currentIndex = 0;

    int[] activityCounts = new int[3];

    while (true)
    {
        Console.WriteLine("Mindfulness App");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Random Activity");
        Console.WriteLine("5. View Activity Counts");
        Console.WriteLine("6. Exit");

        Console.Write("Choose an activity (1-6): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
            case "2":
            case "3":
                activities[int.Parse(choice) - 1].StartActivity();
                activityCounts[int.Parse(choice) - 1]++;
                break;
            case "4":
                if (currentIndex >= shuffledIndices.Count)
                {
                    Shuffle(shuffledIndices);
                    currentIndex = 0;
                }

                int activityIndex = shuffledIndices[currentIndex];
                activities[activityIndex - 1].StartActivity();
                activityCounts[activityIndex - 1]++;
                currentIndex++;
                break;
            case "5":
                Console.WriteLine("Activity Completion Counts:");
                Console.WriteLine($"Breathing Activity: {activityCounts[0]} times");
                Console.WriteLine($"Reflection Activity: {activityCounts[1]} times");
                Console.WriteLine($"Listing Activity: {activityCounts[2]} times");
                break;
            case "6":
                Console.WriteLine("Exiting the program...");
                Thread.Sleep(2000);
                return;
            default:
                Console.WriteLine("Invalid choice. Please choose again.");
                break;
        }
    }
}

// Method to shuffle a list - Used to ensure each option is cycled through if the user selects #4. Random Activity
static void Shuffle<T>(IList<T> list)
{
    Random rng = new Random();
    int n = list.Count;
    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
    }
}
}