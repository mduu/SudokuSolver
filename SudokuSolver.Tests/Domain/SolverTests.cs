using FluentAssertions;
using SudokuSolver.Core.Domain;
using SudokuSolver.Tests.Testing;
using SudokuSolverCLI.Infra;
using Xunit;
using Xunit.Abstractions;

namespace SudokuSolver.Tests.Domain
{
    public class SolverTests
    {
        private readonly Solver solver;
        private readonly ISolveStrategy strategy;
        private readonly IBoardPrinter testingPrinter;

        public SolverTests(ITestOutputHelper output)
        {
            var feedback = new XUnitFeedback(output);
            var boardFactory = new BoardFactory(feedback);
            testingPrinter = new XunitBoardPrinter(output);
            strategy = new ExcludeStrategy(new ConsoleFeedback(), new XunitBoardPrinter(output));
            solver = new Solver(boardFactory, strategy, feedback);
        }

        [Fact]
        public void Solve_Sudoku1()
        {
            // Arrange
            var initialValues = new Board();
            initialValues.Sections[0, 0].Cells[1, 0].SetValue(9);
            initialValues.Sections[0, 0].Cells[2, 0].SetValue(1);
            initialValues.Sections[0, 0].Cells[1, 2].SetValue(6);

            initialValues.Sections[1, 0].Cells[0, 0].SetValue(2);
            initialValues.Sections[1, 0].Cells[2, 0].SetValue(7);
            initialValues.Sections[1, 0].Cells[1, 1].SetValue(9);

            initialValues.Sections[2, 0].Cells[0, 0].SetValue(8);
            initialValues.Sections[2, 0].Cells[2, 0].SetValue(6);
            initialValues.Sections[2, 0].Cells[0, 2].SetValue(5);
            initialValues.Sections[2, 0].Cells[2, 2].SetValue(4);

            initialValues.Sections[0, 1].Cells[0, 0].SetValue(3);
            initialValues.Sections[0, 1].Cells[2, 0].SetValue(8);
            initialValues.Sections[0, 1].Cells[2, 1].SetValue(2);
            initialValues.Sections[0, 1].Cells[2, 2].SetValue(9);

            initialValues.Sections[1, 1].Cells[0, 0].SetValue(4);
            initialValues.Sections[1, 1].Cells[0, 1].SetValue(1);
            initialValues.Sections[1, 1].Cells[1, 1].SetValue(5);
            initialValues.Sections[1, 1].Cells[2, 1].SetValue(9);
            initialValues.Sections[1, 1].Cells[2, 2].SetValue(8);

            initialValues.Sections[2, 1].Cells[0, 0].SetValue(9);
            initialValues.Sections[2, 1].Cells[0, 1].SetValue(7);
            initialValues.Sections[2, 1].Cells[0, 2].SetValue(4);
            initialValues.Sections[2, 1].Cells[2, 2].SetValue(2);

            initialValues.Sections[0, 2].Cells[0, 0].SetValue(8);
            initialValues.Sections[0, 2].Cells[2, 0].SetValue(6);
            initialValues.Sections[0, 2].Cells[0, 2].SetValue(9);
            initialValues.Sections[0, 2].Cells[2, 2].SetValue(4);

            initialValues.Sections[1, 2].Cells[1, 1].SetValue(2);
            initialValues.Sections[1, 2].Cells[0, 2].SetValue(6);
            initialValues.Sections[1, 2].Cells[2, 2].SetValue(5);

            initialValues.Sections[2, 2].Cells[1, 0].SetValue(1);
            initialValues.Sections[2, 2].Cells[0, 2].SetValue(3);
            initialValues.Sections[2, 2].Cells[1, 2].SetValue(7);

            // Act
            var result = solver.Solve(CreateStandardSudokuConfig(initialValues));

            // Assert
            result.Should().NotBeNull();
            testingPrinter.Print(result.SolvedBoard);
            
            var stringPrinter = new TextStringBoardPrinter();
            stringPrinter.Print(result.SolvedBoard);
            var text = stringPrinter.ToString().Trim();

            text.Should().Be(@"--------------
|591|247|836|
|483|596|127|
|267|381|594|
--------------
|378|462|951|
|642|159|783|
|159|738|462|
--------------
|836|974|215|
|715|823|649|
|924|615|378|
--------------".Trim());
        }

        private static SudokuConfiguration CreateStandardSudokuConfig(Board initialValues)
            => new SudokuConfiguration
            {
                InitialValues = initialValues
            };
    }
}