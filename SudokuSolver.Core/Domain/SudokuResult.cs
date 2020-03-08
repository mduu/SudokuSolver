using System;

namespace SudokuSolver.Core.Domain
{
    public class SudokuResult
    {
        public TimeSpan Duration { get; }
        public Board SolvedBoard { get; }
        
        public SudokuResult(TimeSpan duration, Board solvedBoard)
        {
            Duration = duration;
            SolvedBoard = solvedBoard;
        }
    }
}