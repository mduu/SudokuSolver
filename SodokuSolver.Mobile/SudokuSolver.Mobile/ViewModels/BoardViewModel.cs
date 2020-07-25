using SudokuSolver.Core.Domain;

namespace SudokuSolver.Mobile.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        private const byte NumberOfSections = 3;
        private const byte SectionWidth = 3;
        private const byte SectionHeight = 3;

        private Board board;
        private string duration;
        private bool showSolveDetails;

        public BoardViewModel()
        {
            Title = "Board";
            Board = new Board(NumberOfSections, SectionWidth, SectionHeight);
            Duration = "";
            ShowSolveDetails = false;
        }

        public Board Board
        {
            get => board;
            private set => SetProperty(ref board, value);
        }

        public string Duration
        {
            get => duration; 
            private set => SetProperty(ref duration, value);
        }

        public bool ShowSolveDetails
        {
            get => showSolveDetails;
            set => SetProperty(ref showSolveDetails, value);
        }
    }
}