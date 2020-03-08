namespace SudokuSolver.Core.Domain
{
    public class SudokuConfiguration
    {
        public byte NumberOfSections { get; set; } = 3;
        public byte SectionWidth { get; set; } = 3;
        public byte SectionHeight { get; set; } = 3;
        public Board? InitialValues { get; set; }
    }
}