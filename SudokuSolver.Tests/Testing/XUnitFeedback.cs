using SudokuSolver.Core.SharedKernel;
using Xunit.Abstractions;

namespace SudokuSolver.Tests.Testing
{
    public class XUnitFeedback : TextFeedbackBase
    {
        private readonly ITestOutputHelper output;

        public XUnitFeedback(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        protected override void WriteLine(string message, bool isError = false)
        {
            output.WriteLine(message);
        }
    }
}