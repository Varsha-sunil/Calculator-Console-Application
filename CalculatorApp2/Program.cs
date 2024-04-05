using System;

namespace CalculatorApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            Logger logger = new Logger(); // Create an instance of Logger

            Calculator calculator = new Calculator(logger); // Pass logger to Calculator

            string again = "yes"; // Initialize 'again' with a default value
            do
            {
                Console.WriteLine("Simple Calculator \nAvailable Operators : + , - , * , / , % ");
                Console.WriteLine("Enter the expression:");
                string input = Console.ReadLine();

                try
                {
                    double result = calculator.Calculate(input);
                    Console.WriteLine($"Result: {result}");
                }
                catch (ArgumentException ex)
                {
                    logger.LogError(ex.Message, "Main"); // Log specific error message and specify the method name
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occurred: {ex.Message}", "Main"); // Log general error message and specify the method name
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine("Please try again.");

                    continue; // Continue to the next iteration of the loop
                }

                do
                {
                    Console.WriteLine("Choose only yes/no");
                    Console.WriteLine("Do you want to perform another calculation? (yes/no)");
                    again = Console.ReadLine().ToLower();
                } while (again != "yes" && again != "no");

            } while (again == "yes");
        }
    }
}
