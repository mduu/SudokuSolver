using System;
using SudokuSolver.Core.SharedKernel;

namespace SudokuSolverCLI.Infra
{
    public class ConsoleFeedback : TextFeedbackBase
    {
        protected override void WriteLine(string message, bool isError = false)
        {
            if (isError)
            {
                if (Console.IsErrorRedirected)
                {
                    Console.Error.WriteLine(message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message);
            }
        }
    }
}