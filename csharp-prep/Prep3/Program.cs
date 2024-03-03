using System;

class Program
{
    static void Main(string[] args)
    {
        // ----- Part 1 and 2 where the user inputs a magic number -----
        // Console.WriteLine("What is the magic number?");
        // int magicNumber = int.Parse(Console.ReadLine());

        // ---- Part 3 where the computer just randomly generates a number -----
        Random randomnumber = new Random();
        int magicNumber = randomnumber.Next(1,100);

        int guess = -1;
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("Guess Higher");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Guess Lower");
            }
            else
            {
                Console.WriteLine("You guessed it. Well done!");
            }
        }
    }
}