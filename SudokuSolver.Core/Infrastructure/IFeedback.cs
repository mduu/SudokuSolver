using System.Collections.Generic;

namespace SudokuSolver.Core.Infrastructure
{
    public interface IFeedback
    {
        void Information(string message, IDictionary<string, object>? data = null);
        void Error(string errorMessage, IDictionary<string, object>? data = null);
    }
}