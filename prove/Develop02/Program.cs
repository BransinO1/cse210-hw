/*
   Corbin Meacham
   W02 Journal Entry Project

   Description:
   - I added a section to prompt the user to allow the user to
     add a reminder timeslot if that would assist in their journaling.
   - If added, the reminder time will be displayed, and the user will
     receive a prompt message at that time if they are inside the journal program.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static Journal journal = new Journal();
    static PromptGenerator promptGenerator = new PromptGenerator();
    static ReminderSetter reminderSetter = new ReminderSetter(journal);
    static void Main()
    {
        journal.Load();

        while (true)
        {
            DisplayMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    WriteNewEntry();
                    break;
                case 2:
                    DisplayJournal();
                    break;
                case 3:
                    SaveJournal();
                    break;
                case 4:
                    journal.Load();
                    break;
                case 5:
                    reminderSetter.SetReminderInteractive();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            journal.CheckAndDisplayReminder(promptGenerator, WriteNewEntry);
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Set a reminder");
        Console.WriteLine("6. Exit");
    }

    static int GetMenuChoice()
    {
        Console.Write("Enter your choice (1-6): ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            return choice;
        }
        return 0;
    }

    static void WriteNewEntry()
    {
        string randomPrompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {randomPrompt}");

        Console.Write("Your response: ");
        string userResponse = Console.ReadLine();

        journal.AddEntry(randomPrompt, userResponse);
        Console.WriteLine("Entry saved successfully!");
    }

    static void DisplayJournal()
    {
        journal.DisplayEntries();
    }

    static void SaveJournal()
    {
        Console.Write("Enter a filename to save the journal: ");
        string saveFileName = Console.ReadLine();

        try
        {
            journal.Save(saveFileName);
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the journal: {ex.Message}");
        }
    }
}

class Journal
{
    private List<DailyEvent> eventsList = new List<DailyEvent>();
    private TimeSpan reminderTime;

    public void AddEntry(string prompt, string response)
    {
        DailyEvent newEvent = new DailyEvent
        {
            Question = prompt,
            Response = response,
            Date = DateTime.Now
        };

        eventsList.Add(newEvent);
    }

    public void DisplayEntries()
    {
        if (eventsList.Count == 0)
        {
            Console.WriteLine("Journal is empty. Write some entries first.");
            return;
        }

        Console.WriteLine("Journal Entries:");
        foreach (var entry in eventsList)
        {
            Console.WriteLine($"Date: {entry.Date}, Prompt: {entry.Question}, Response: {entry.Response}");
        }
    }

    public void Save(string fileName)
    {
        try
        {
            string jsonEvents = JsonSerializer.Serialize(eventsList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonEvents);
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the journal: {ex.Message}");
        }
    }

    public void Load()
    {
        Console.Write("Enter a filename to load the journal: ");
        string loadFileName = Console.ReadLine();

        try
        {
            if (File.Exists(loadFileName))
            {
                string jsonEvents = File.ReadAllText(loadFileName);
                eventsList = JsonSerializer.Deserialize<List<DailyEvent>>(jsonEvents);
                Console.WriteLine("Journal loaded successfully!");
            }
            else
            {
                Console.WriteLine("File not found. Creating a new journal.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading the journal: {ex.Message}");
        }
    }

    public void SetReminder(TimeSpan reminderTime)
    {
        this.reminderTime = reminderTime;
    }

    static void SetReminderInteractive(Journal journal)
    {
        Console.WriteLine("Do you want to set a reminder?");
        Console.Write("Enter 'Y' for Yes, 'N' for No: ");
        string userInput = Console.ReadLine().ToUpper();

        if (userInput == "Y")
        {
            Console.WriteLine("Set a reminder time (HH:mm). Example: 14:22");

            while (true)
            {
                Console.Write("Enter your reminder time: ");
                string reminderInput = Console.ReadLine().Trim();

                if (TimeSpan.TryParse(reminderInput, out TimeSpan selectedTime))
                {
                    journal.SetReminder(selectedTime);
                    Console.WriteLine($"Reminder set for {selectedTime.ToString("hh\\:mm tt")}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please try again.");
                }
            }
        }
    }

    public void CheckAndDisplayReminder(PromptGenerator promptGenerator, Action writeNewEntry)
    {
        Console.WriteLine($"Current Time: {DateTime.Now}");
        Console.WriteLine($"Reminder Time: {reminderTime}");

        if (reminderTime != TimeSpan.MinValue && reminderTime.Hours == DateTime.Now.Hour && reminderTime.Minutes == DateTime.Now.Minute)
        {
            Console.WriteLine("Reminder Triggered!");
            string randomPrompt = promptGenerator.GetRandomPrompt();
            Console.WriteLine($"Reminder: {randomPrompt}");
            Console.Write("Would you like to write a new entry? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                writeNewEntry();
            }
        }
    }
}

class PromptGenerator
{
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was a challenge that I overcame today?",
        "What is something that I was grateful for today?",
        "What was the best part of my day?",
        "What was the worst part of my day?",
        "Is there a theme or quote that fits with my experiances today?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        return prompts[random.Next(prompts.Count)];
    }
}

class DailyEvent
{
    public string Question { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
}
class ReminderSetter
{
    private Journal journal;

    public ReminderSetter(Journal journal)
    {
        this.journal = journal;
    }

    public void SetReminderInteractive()
    {
        Console.WriteLine("Do you want to set a reminder?");
        Console.Write("Enter 'Y' for Yes, 'N' for No: ");
        string userInput = Console.ReadLine().ToUpper();

        if (userInput == "Y")
        {
            Console.WriteLine("Set a reminder time (HH:mm). Example: 14:22");

            while (true)
            {
                Console.Write("Enter your reminder time: ");
                string reminderInput = Console.ReadLine().Trim();

                if (DateTime.TryParseExact(reminderInput, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime selectedTime))
                {
                    journal.SetReminder(selectedTime.TimeOfDay);
                    Console.WriteLine($"Reminder set for {selectedTime.ToString("hh\\:mm tt")}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please try again.");
                }
            }
        }
    }
}

