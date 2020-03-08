using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.SharedKernel
{
    public abstract class TextFeedbackBase : IFeedback
    {
        public void Information(string message, IDictionary<string, object>? data = null)
        {
            Log(false, message, data);
        }

        public void Error(string errorMessage, IDictionary<string, object>? data = null)
        {
            Log(true, errorMessage, data);
        }

        private void Log(bool isError, string message, IDictionary<string, object>? data = null)
        {
            var level = isError ? "Error" : "Info";

            var dataMsg = data != null
                ? "[" + string.Join(',', data.Select(d => $"{d.Key}={d.Value}")) + "]"
                : null;

            var msg = $"{DateTime.Now:s} {level}: {message} {dataMsg}".Trim();

            WriteLine(msg, isError);
        }

        protected abstract void WriteLine(string message, bool isError = false);
    }
}