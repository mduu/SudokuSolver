using System.Text;

namespace SudokuSolver.Core.Domain
{
    public class TextStringBoardPrinter : TextSudokuPrinterBase
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        
        protected override void Write(string text) => stringBuilder.Append(text);
        protected override void WriteLine(string? text = null) => stringBuilder.AppendLine(text);
        public override string ToString() => stringBuilder.ToString();
    }
}