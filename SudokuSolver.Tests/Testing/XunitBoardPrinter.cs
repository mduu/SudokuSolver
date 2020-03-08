using System.Text;
using SudokuSolver.Core.Domain;
using Xunit.Abstractions;

namespace SudokuSolver.Tests.Testing
{
    public class XunitBoardPrinter : TextSudokuPrinterBase
    {
        private readonly ITestOutputHelper output;
        private readonly StringBuilder currentLineBuilder = new StringBuilder();

        public XunitBoardPrinter(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        protected override void Write(string text)
        {
            AddToCurrentLine(text);
        }

        protected override void WriteLine(string? text = null)
        {
            if (text != null)
            {
                AddToCurrentLine(text);
            }

            output.WriteLine(currentLineBuilder.ToString());
            currentLineBuilder.Clear();
        }

        private void AddToCurrentLine(string text)
            => currentLineBuilder.Append(text);
    }
}