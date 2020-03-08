namespace SudokuSolver.Core.Domain
{
    public interface ISolveStrategy
    {
        void Solver(Board board);
    }
}