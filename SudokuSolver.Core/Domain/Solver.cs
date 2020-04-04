using System.Diagnostics;
using SudokuSolver.Core.Infrastructure;

namespace SudokuSolver.Core.Domain
{
    public class Solver : ISolver
    {
        private readonly BoardFactory boardFactory;
        private readonly ISolveStrategy solveStrategy;
        private readonly IFeedback feedback;

        public Solver(
            BoardFactory boardFactory,
            ISolveStrategy solveStrategy,
            IFeedback feedback)
        {
            this.boardFactory = boardFactory;
            this.solveStrategy = solveStrategy;
            this.feedback = feedback;
        }
        
        public SudokuResult Solve(SudokuConfiguration configuration)
        {
            feedback.Information("Start solving ...");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var board = boardFactory.Create(configuration);
            solveStrategy.Solver(board);
            stopWatch.Stop();
            
            feedback.Information($"Solved in {stopWatch.ElapsedMilliseconds}ms");
            
            return new SudokuResult(stopWatch.Elapsed, board);
        }
    }
}