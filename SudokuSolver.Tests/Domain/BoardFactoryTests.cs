using FakeItEasy;
using FluentAssertions;
using SudokuSolver.Core.Domain;
using SudokuSolver.Core.SharedKernel;
using Xunit;

namespace SudokuSolver.Tests.Domain
{
    public class BoardFactoryTests
    {
        private readonly IFeedback feedback = A.Fake<IFeedback>();
        private readonly BoardFactory factory;

        public BoardFactoryTests()
        {
            factory = new BoardFactory(feedback);
        }

        [Fact]
        public void Create_WithoutInitialValues_MustSucceed()
        {
            // Arrange
            var configuration = new SudokuConfiguration();
            
            // Act
            var board = factory.Create(configuration);

            // Assert
            board.Sections.Length.Should().Be(9);
        }

        [Fact]
        public void Create_WithInitialValues_MustSetValues()
        {
            // Arrange
            var initialValues = new Board();
            initialValues.Sections[2, 2].Cells[2, 2].SetValue(6);
            
            var configuration = new SudokuConfiguration
            {
                InitialValues = initialValues
            };
            
            // Act
            var board = factory.Create(configuration);

            // Assert
            board.Sections.Length.Should().Be(9);
            board.Sections[2, 2].Cells[2, 2].Value.Should().Be(6);
            board.Sections[2, 2].Cells[2, 2].IsFixed.Should().BeTrue();
        }
    }
}