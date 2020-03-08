using System;
using FluentAssertions;
using SudokuSolver.Core.Domain;
using Xunit;

namespace SudokuSolver.Tests.Domain
{
    public class BoardTests
    {
        [Fact]
        public void Ctor_InitializeDefaultBoard_MustBeValidBoardWithDefaults()
        {
            // Arrange
            
            // Act
            var board = new Board();

            // Assert
            board.Sections.Should().NotBeNull();
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var section = board.Sections[x, y];
                    section.Should().NotBeNull();

                    for (var cx = 0; cx < 3; cx++)
                    {
                        for (var cy = 0; cy < 3; cy++)
                        {
                            var cell = section.Cells[cx, cy];
                            cell.Should().NotBeNull();
                            cell.IsFixed.Should().BeFalse();
                            cell.Value.Should().BeNull();
                        }
                    }
                }
            }
        }

        [Fact]
        public void SetFixedValue_FirstTime_MustSetValueAndIsInitialValue()
        {
            // Arrange
            var cell = CreateDefaultCell();

            // Act
            cell.SetFixedValue(4);

            // Assert
            cell.Value.Should().Be(4);
            cell.IsFixed.Should().BeTrue();
        }

        [Fact]
        public void SetValue_FirstTime_MustSucceed()
        {
            // Arrange
            var cell = CreateDefaultCell();

            // Act
            cell.SetValue(4);

            // Assert
            cell.Value.Should().Be(4);
        }

        [Fact]
        public void SetValue_TwiceTime_MustFail()
        {
            // Arrange
            var cell = CreateDefaultCell();
            cell.SetValue(4);

            // Act
            Action act = () => cell.SetValue(5);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
        
        private Board.Cell CreateDefaultCell()
            => new Board.Cell(
                new Board.Section(new Board(), 3, 3, 0, 0),
                0,
                0);
    }
}