namespace SudokuSolver.Core.Domain
{
    public interface ISolver
    {
        SudokuResult Solve(SudokuConfiguration configuration);
    }
}