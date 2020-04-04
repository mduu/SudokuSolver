using System;
using System.IO;
using SimpleInjector;
using SudokuSolver.Core.Domain;
using SudokuSolver.Core.SharedKernel;
using SudokuSolverCLI.Infra;

namespace SudokuSolverCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerFactory.Create();
            var feedback = container.GetInstance<IFeedback>();
            var configFileName = 
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                    "Documents",
                    "sudoku.txt");

            Greet();
            
            var configuration = ReadConfiguration(container, configFileName);
            if (configuration != null)
            {
                var solver = container.GetInstance<ISolver>();
                var solverResult = solver.Solve(configuration);

                var boardPrinter = container.GetInstance<IBoardPrinter>();
                boardPrinter.Print(solverResult.SolvedBoard);
                feedback.Information($"Solved in {solverResult.Duration.TotalMilliseconds} ms.");
            }
            
            feedback.Information("Exited.");
        }

        private static void Greet()
        {
            using var colorGuard = new ConsoleColorGuard(ConsoleColor.Cyan);
            Console.WriteLine("Welcome to Sudoku Solver!");
        }

        private static SudokuConfiguration ReadConfiguration(Container container, string configFileName)
            => container.GetInstance<SimpleTextFileConfigReader>()
                .ParseConfiguration(configFileName);
    }
}