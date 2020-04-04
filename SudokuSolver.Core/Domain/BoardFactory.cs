using System.Collections.Generic;
using SudokuSolver.Core.Infrastructure;

namespace SudokuSolver.Core.Domain
{
    public class BoardFactory
    {
        private readonly IFeedback feedback;

        public BoardFactory(IFeedback feedback)
        {
            this.feedback = feedback;
        }

        public Board Create(SudokuConfiguration configuration)
        {
            var board = CreateEmptyBoard(configuration);

            if (configuration.InitialValues != null)
            {
                ApplyInitialValues(configuration.InitialValues, board);
            }

            return board;
        }

        private static Board CreateEmptyBoard(SudokuConfiguration configuration)
            => new Board(
                configuration.NumberOfSections,
                configuration.SectionWidth,
                configuration.SectionHeight);

        private void ApplyInitialValues(Board sourceBoard, Board targetBoard)
        {
            for (var sectionX = 0; sectionX < sourceBoard.NumberOfSections; sectionX++)
            {
                for (var sectionY = 0; sectionY < sourceBoard.NumberOfSections; sectionY++)
                {
                    var sourceSection = sourceBoard.Sections[sectionX, sectionY];
                    for (var cellX = 0; cellX < sourceBoard.SectionWidth; cellX++)
                    {
                        for (var cellY = 0; cellY < sourceBoard.SectionHeight; cellY++)
                        {
                            var sourceCell = sourceSection.Cells[cellX, cellY];
                            ApplyInitialValue(targetBoard, sourceCell, sectionX, sectionY, cellX, cellY);
                        }
                    }
                }
            }
        }

        private void ApplyInitialValue(Board targetBoard, Board.Cell sourceCell, int sectionX, int sectionY, int cellX, int cellY)
        {
            if (!sourceCell.Value.HasValue)
            {
                return;
            }

            var targetCell = targetBoard.Sections[sectionX, sectionY].Cells[cellX, cellY];
            targetCell.SetFixedValue(sourceCell.Value.Value);

            feedback.Information($"Initial value set to {sourceCell.Value}", new Dictionary<string, object>
            {
                {"Section X", sectionX},
                {"Section Y", sectionY},
                {"Cell X", cellX},
                {"Cell Y", cellY},
                {"Initial Value", sourceCell.Value},
            });
        }
    }
}