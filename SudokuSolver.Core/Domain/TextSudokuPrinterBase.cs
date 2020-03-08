using System;

namespace SudokuSolver.Core.Domain
{
    public abstract class TextSudokuPrinterBase: IBoardPrinter
    {
        public void Print(Board board)
        {
            WriteLine(board, '-');
            for (var sectionY = 0; sectionY < board.NumberOfSections; sectionY++)
            {
                for (var cellY = 0; cellY < board.SectionHeight; cellY++)
                {
                    for (var sectionX = 0; sectionX < board.NumberOfSections; sectionX++)
                    {
                        Write("|");

                        for (var cellX = 0; cellX < board.SectionWidth; cellX++)
                        {
                            var cell = board.Sections[sectionX, sectionY].Cells[cellX, cellY];

                            WriteCellValue(cell, cell.Value?.ToString() ?? " ");
                        }
                    }
                    
                    Write("|");
                    WriteLine();
                }
                
                WriteLine(board, '-');
            }
        }

        protected virtual void WriteCellValue(Board.Cell cell, string value)
        {
            Write(value);
        }
        
        protected abstract void Write(string text);
        protected abstract void WriteLine(string? text = null);

        private void WriteLine(Board board, Char c)
        {
            var countChars = board.NumberOfSections * (board.SectionWidth + 1) + 2;
            var line = new string(c, countChars);
            WriteLine(line);
        }
    }
}