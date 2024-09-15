using System;

namespace ArithmeticPracticeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int num1, num2, correctAnswer, userAnswer;

            Console.WriteLine("Welcome to the Arithmetic Practice Game!");
            Console.WriteLine("Enter your answers and press Enter to submit.");

            while (true)
            {
                // Generate two random numbers for the arithmetic operation
                num1 = random.Next(1, 10);
                num2 = random.Next(1, 10);

                // Choose a random arithmetic operation (+, -, *, /)
                int operation = random.Next(1, 4);
                string operationSymbol = "";
                switch (operation)
                {
                    case 1:
                        operationSymbol = "+";
                        correctAnswer = num1 + num2;
                        break;
                    case 2:
                        operationSymbol = "-";
                        correctAnswer = num1 - num2;
                        break;
                    case 3:
                        operationSymbol = "*";
                        correctAnswer = num1 * num2;
                        break;
                    case 4:
                        operationSymbol = "/";
                        correctAnswer = num1 / num2;
                        break;
                }

                // Display the arithmetic problem
                Console.Write($"{num1} {operationSymbol} {num2} = ");
                string userInput = Console.ReadLine();

                // Try to parse the user's input
                if (int.TryParse(userInput, out userAnswer))
                {
                    if (userAnswer == correctAnswer)
                    {
                        Console.WriteLine("Correct!");
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, the correct answer is {correctAnswer}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a whole number.");
                }
            }
        }
    }
}