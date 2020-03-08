using System;

namespace SudokuSolverCLI.Infra
{
    public class ConsoleColorGuard : IDisposable
    {
        private readonly ConsoleColor savedForegroundColor;
        private readonly ConsoleColor savedBackgroundColor;

        public ConsoleColorGuard(ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
        {
            savedForegroundColor = Console.ForegroundColor;
            savedBackgroundColor = Console.BackgroundColor;

            if (foregroundColor.HasValue)
            {
                Console.ForegroundColor = foregroundColor.Value;
            }

            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }
        }
        
        public void Dispose()
        {
            Console.ForegroundColor = savedForegroundColor;
            Console.BackgroundColor = savedBackgroundColor;
        }
    }
}