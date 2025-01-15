using System;

namespace Adventure_await.Utils
{
    public static class InputHandler
    {
        public static int GetInt(int min, int max)
        {
            int choice = 0;
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    choice = int.Parse(input);

                    if (choice >= min && choice <= max)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between {min} and {max}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Please enter a number between {min} and {max}");
                }
            }

            return choice;
        }

        public static string GetString()
        {
            return Console.ReadLine();
        }
    }
}
