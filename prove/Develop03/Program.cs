/*
   Corbin Meacham
   W03 Scripture Memorization Tool

   Description:
   - So I've added a section to convert multiple scripture verses into a dictionary
   This allows the User to work on memorizing multiple verses at the same time.
   - The program will randomly rotate between the verses. It is also set to remove
   three words at a time, that can be changed if the user wants less or more words blanked out. 
*/

using System;
using System.Collections.Generic;
using System.Linq;

public class Word {
    public string Text { get; private set; }
    public bool IsHidden { get; set; }

    public Word(string text) {
        Text = text;
        IsHidden = false;
    }
}

public class Scripture {
    private List<Word> words;

    public Scripture(List<Word> words) {
        this.words = words;
    }

    public void HideRandomWords(int numberToHide) {
        Random random = new Random();
        List<Word> wordsToHide = words.Where(word => !word.IsHidden).OrderBy(_ => random.Next()).Take(numberToHide).ToList();

        foreach (var word in wordsToHide) {
            word.IsHidden = true;
        }
    }

    public bool IsCompletelyHidden() {
        return words.All(word => word.IsHidden);
    }

    public string GetDisplayText() {
        return string.Join(" ", words.Select(word => word.IsHidden ? "___" : word.Text));
    }
}
public class Reference {
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int EndVerse { get; private set; }

    public Reference(string book, int chapter, int startVerse, int endVerse = -1) {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse == -1 ? startVerse : endVerse;
    }

    public override string ToString() {
        if (StartVerse == EndVerse) {
            return $"{Book} {Chapter}:{StartVerse}";
        }
        return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

public class Verse {
    public string Reference { get; private set; }
    public string Text { get; private set; }

    public Verse(string reference, string text) {
        Reference = reference;
        Text = text;
    }
}

class Program {
    static void Main(string[] args) {
        Dictionary<string, Verse> verseLibrary = new Dictionary<string, Verse> {
            {
                "Genesis 1:1",
                new Verse("Genesis 1:1", "In the beginning God created the heavens and the earth.")
            },
            {
                "John 3:16",
                new Verse("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.")
            },
            {
                "Proverbs 3:5-6",
                new Verse("Proverbs 3:5-6", "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.")
            },
            {
                "Philippians 4:13",
                new Verse("Philippians 4:13", "I can do all things through Christ which strengtheneth me.")
            },
            {
                "Jeremiah 28:12-13",
                new Verse("Jeremiah 28:12-13", "Then shall ye call upon me, and ye shall go and pray unto me, and I will hearken unto you. And ye shall seek me, and find me, when ye shall search for me with all your heart.")
            }
        };

        Random random = new Random();
        int verseIndex = random.Next(0, verseLibrary.Count);
        Verse chosenVerse = verseLibrary.Values.ToList()[verseIndex];

        Console.WriteLine($"Chosen Verse: {chosenVerse.Reference} - \"{chosenVerse.Text}\"");

        List<Word> words = chosenVerse.Text.Split(' ').Select(word => new Word(word)).ToList();
        Scripture scripture = new Scripture(words);

        Console.WriteLine("\nPress Enter to hide 3 words or type 'quit' to exit...");
        while (!scripture.IsCompletelyHidden()) {
            string input = Console.ReadLine();
            if (input.ToLower() == "quit") {
                Console.WriteLine("\nExiting program...");
                return;
            }
            scripture.HideRandomWords(3);
            Console.WriteLine("\nHidden Text:");
            Console.WriteLine($"{chosenVerse.Reference} - {scripture.GetDisplayText()}");
        }

        Console.WriteLine("\nAll words are hidden.");
    }
}