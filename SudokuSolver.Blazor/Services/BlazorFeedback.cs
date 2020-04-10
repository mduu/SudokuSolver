using System.Collections.Generic;
using SudokuSolver.Core.Infrastructure;

namespace SudokuSolver.Blazor.Services
{
    public class BlazorFeedback : IFeedback
    {
        public void Information(string message, IDictionary<string, object>? data = null)
        {
        }

        public void Error(string errorMessage, IDictionary<string, object>? data = null)
        {
        }
    }
}