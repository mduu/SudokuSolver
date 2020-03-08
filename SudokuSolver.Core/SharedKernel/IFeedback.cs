using System.Collections.Generic;

namespace SudokuSolver.Core.SharedKernel
{
    public interface IFeedback
    {
        void Information(string message, IDictionary<string, object>? data = null);
        void Error(string errorMessage, IDictionary<string, object>? data = null);
    }
}