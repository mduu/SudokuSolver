using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Domain
{
    public static class BoardQueryExtensions
    {
        public static IEnumerable<Board.Cell> GetCellsInColumn(this Board board, Board.Cell cell)
            => board.GetAllCells()
                .Where(c => c.X == cell.X && c.ParentParentSection.X == cell.ParentParentSection.X);

        public static IEnumerable<Board.Cell> GetCellsInRow(this Board board, Board.Cell cell) 
            => board.GetAllCells()
                .Where(c => c.Y == cell.Y && c.ParentParentSection.Y == cell.ParentParentSection.Y);

        public static IEnumerable<Board.Section> GetAllSections(this Board board)
        {
            for (var sectionX = 0; sectionX < board.NumberOfSections; sectionX++)
            {
                for (var sectionY = 0; sectionY < board.NumberOfSections; sectionY++)
                {
                    yield return board.Sections[sectionX, sectionY];
                }
            }
        }

        public static IEnumerable<Board.Cell> GetAllCells(this Board board)
        {
            var allSections = board.GetAllSections();

            foreach (var section in allSections)
            {
                for (var cellX = 0; cellX < board.SectionWidth; cellX++)
                {
                    for (var cellY = 0; cellY < board.SectionHeight; cellY++)
                    {
                        yield return section.Cells[cellX, cellY];
                    }
                }
            }
        }

        public static IEnumerable<Board.Cell> GetAllCellsInSection(this Board board, Board.Cell cell) 
            => GetAllCellsInSection(cell.ParentParentSection);

        public static IEnumerable<Board.Cell> GetAllCellsInSection(this Board.Section section)
        {
            for (var cellX = 0; cellX < section.ParentBoard.SectionWidth; cellX++)
            {
                for (var cellY = 0; cellY < section.ParentBoard.SectionHeight; cellY++)
                {
                    yield return section.Cells[cellX, cellY];
                }
            }
        }

        public static Board.Cell? GetCellByCellId(this Board board, Guid cellId)
            => board.GetAllCells()
                .FirstOrDefault(c => c.CellId == cellId);
    }
}