using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Core.SharedKernel;

namespace SudokuSolver.Core.Domain
{
    public class ExcludeStrategy : ISolveStrategy
    {
        private const bool DebugMode = false;
        private readonly IFeedback feedback;
        private readonly IBoardPrinter boardPrinter;
        private readonly IDictionary<Guid, CellSolveData> allCellData = new Dictionary<Guid, CellSolveData>();

        public ExcludeStrategy(IFeedback feedback, IBoardPrinter boardPrinter)
        {
            this.feedback = feedback;
            this.boardPrinter = boardPrinter;
        }

        public void Solver(Board board)
        {
            feedback.Information($"Start solving with {nameof(ExcludeStrategy)}...");

            var countIterations = 0;
            var anyValueChanged = false;
            do
            {
                BuildPossibleValues(board);
                
                anyValueChanged = TrySolveNextSinglePossibleValue(board);

                if (!anyValueChanged)
                {
                    anyValueChanged = TrySolveUniqueValuesInSections(board);
                }

                if (DebugMode)
                {
                    boardPrinter.Print(board);
                }

                countIterations++;
            } while (anyValueChanged);

            feedback.Information($"Done solving with {nameof(ExcludeStrategy)} in {countIterations} iterations.");
        }
        
        private void BuildPossibleValues(Board board)
        {
            allCellData.Clear();
            foreach (var cell in board.GetAllCells())
            {
                BuildPossibleValuesForCell(board, cell);
            }
        }

        private void BuildPossibleValuesForCell(Board board, Board.Cell cell)
        {
            if (cell.Value.HasValue)
            {
                return;
            }

            var cellsInRow = board.GetCellsInRow(cell);
            var cellsInColumn = board.GetCellsInColumn(cell);
            var cellsInSection = board.GetAllCellsInSection(cell);
            
            var allRelevantCells = 
                cellsInRow.Concat(cellsInColumn).Concat(cellsInSection).Distinct();
            
            AddPossibleValues(board, cell, allRelevantCells.ToArray());
        }

        private void AddPossibleValues(Board board, Board.Cell cell, IReadOnlyCollection<Board.Cell> cells)
        {
            var cellSolveData = new CellSolveData();
            
            foreach (var value in GetAllValues(board))
            {
                if (!cells.Any(c => c.Value.HasValue && c.Value == value) && 
                    cellSolveData.PossibleValues.All(p => p != value))
                {
                    cellSolveData.PossibleValues.Add(value);
                }
            }

            allCellData[cell.CellId] = cellSolveData;
        }
        
        private IEnumerable<byte> GetAllValues(Board board)
            => Enumerable.Range(1, board.SectionWidth * board.SectionHeight)
                .Select(Convert.ToByte);
        
        private bool TrySolveNextSinglePossibleValue(Board board)
        {
            var cellItem = allCellData
                .Where(c => c.Value.PossibleValues.Count == 1)
                .Select(c => (CellId: c.Key, cellData: c.Value))
                .FirstOrDefault();

            if (cellItem == default)
            {
                return false;
            }

            var cell = board.GetCellByCellId(cellItem.CellId);
            if (cell == null || cell.Value.HasValue)
            {
                return false;
            }

            var value = cellItem.cellData.PossibleValues[0];
            cell.SetValue(value);
            
            LogCellMove(cell, $"Found single possible value {value}");
            
            return true;
        }

        private bool TrySolveUniqueValuesInSections(Board board)
        {
            var allSections = board.GetAllSections();
            foreach (var section in allSections)
            {
                foreach (var cell in section.GetAllCellsInSection())
                {
                    if (!allCellData.ContainsKey(cell.CellId))
                    {
                        continue;
                    }
                    
                    var cellData = allCellData[cell.CellId];
                    foreach (var possibleCellValue in cellData.PossibleValues)
                    {
                        bool CellHasValue(Board.Cell c, byte v)
                        {
                            if (c.Value.HasValue && c.Value.Value == v)
                            {
                                return true;
                            }

                            if (!allCellData.ContainsKey(c.CellId))
                            {
                                return false;
                            }

                            return allCellData[c.CellId].PossibleValues.Any(p => p == v);
                        }

                        var otherSections = section
                            .GetAllCellsInSection()
                            .Where(c => c.CellId != cell.CellId && CellHasValue(c, possibleCellValue));

                        if (!otherSections.Any())
                        {
                            cell.SetValue(possibleCellValue);
                            LogCellMove(cell, $"Found unique value {possibleCellValue}");
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void LogCellMove(Board.Cell cell, string message)
            => feedback.Information($"{message} (Cell: {cell.ParentParentSection.X},{cell.ParentParentSection.Y}/{cell.X},{cell.Y})");

        private void PrintIfWatchingCell(Board.Cell cell, Board board)
            => boardPrinter.Print(board);

        class CellSolveData
        {
            public IList<byte> PossibleValues { get; } = new List<byte>();
        }
    }
}