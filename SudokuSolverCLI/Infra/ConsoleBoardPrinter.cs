using System;
using SudokuSolver.Core.Domain;

namespace SudokuSolverCLI.Infra
{
    public class ConsoleBoardPrinter : TextSudokuPrinterBase
    {
        protected override void WriteCellValue(Board.Cell cell, string value)
        {
            using var colorGuard = new ConsoleColorGuard(GetCellColor(cell));
            base.WriteCellValue(cell, value);
        }

        protected override void Write(string text)
        {
            Console.Write(text);
        }
        
        protected override void WriteLine(string? text = null)
        {
            Console.WriteLine(text);
        }

        private ConsoleColor GetCellColor(Board.Cell cell)
        {
            if (cell.IsError)
            {
                return ConsoleColor.Red;
            }

            if (cell.IsFixed)
            {
                return ConsoleColor.White;
            }

            return ConsoleColor.Gray;
        }
    }
}