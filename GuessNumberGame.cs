using System;

namespace HelloWorld
{
    /**
     * Simple number guessing game.
     */
    public static class GuessNumberGame
    {
        public static void Run()
        {
            Random r = new Random();
            int winNumber = r.Next(0, 100);
            bool won = false;
            do
            {
                Console.Write("Guess a number: ");
                int guess = int.Parse(Console.ReadLine());
                if (guess > winNumber)
                {
                    Console.Write("Too high!");
                }else if (guess < winNumber)
                {
                    Console.Write("Too low!");
                }
                else
                {
                    won = true;
                }
                
            } while (!won);
            Console.Write("You won!");
        }
    }
}