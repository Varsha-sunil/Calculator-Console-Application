using System;
using System.IO;

namespace CalculatorApp2
{
    class Logger
    {
        private readonly string logFilePath = @"V:\Celstream Technologies\samples_c#\Week 5 - Revision\CalculatorApp2\log.txt";

        public void LogInput(string input, string methodName)
        {
            Log($"[INF] {methodName} - NEW OPERATION: " + $"Input: {input}");
        }

        public void LogOutput( double result, string methodName)
        {
            Log($"[INF] {methodName} -  Result: {result}");
        }

        public void LogError(string errorMessage, string methodName)
        {
            Log($"[ERROR] {methodName} - Error: {errorMessage}");
        }

        private void Log(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:MM/dd/yyyy hh:mm:ss tt}: {message}";
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
    }
}
