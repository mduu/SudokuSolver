using System;

namespace SudokuSolver.Core.Domain
{
    public class Board
    {
        public byte NumberOfSections { get; }
        public byte SectionWidth { get; }
        public byte SectionHeight { get; }
        public Section[,] Sections { get; }

        public Board(byte numberOfSections = 3, byte sectionWidth = 3, byte sectionHeight = 3)
        {
            NumberOfSections = numberOfSections;
            SectionWidth = sectionWidth;
            SectionHeight = sectionHeight;
            
            Sections = new Section[numberOfSections, numberOfSections];
            for (var x = 0; x < numberOfSections; x++)
            {
                for (var y = 0; y < numberOfSections; y++)
                {
                    Sections[x, y] = new Section(this, sectionWidth, sectionHeight, x, y);
                }
            }
        }

        public class Section
        {
            public Cell[,] Cells { get; }
            public Board ParentBoard { get; }
            public int X { get; }
            public int Y { get; }

            public Section(Board parentBoard, byte sectionWidth, byte sectionHeight, int sectionX, int sectionY)
            {
                ParentBoard = parentBoard;
                X = sectionX;
                Y = sectionY;
                
                Cells = new Cell[sectionWidth, sectionHeight];
                for (var x = 0; x < sectionWidth; x++)
                {
                    for (var y = 0; y < sectionHeight; y++)
                    {
                        Cells[x, y] = new Cell(this, x, y);
                    }
                }
            }
        }
        
        public class Cell
        {
            public Guid CellId { get; } = Guid.NewGuid();
            public Section ParentParentSection { get; }
            public int X { get; }
            public int Y { get; }
            public byte? Value { get; private set; }
            public bool IsFixed { get; private set; }
            public bool IsError { get; private set; }
            public string? ErrorMessage { get; private set; }

            public Cell(Section parentSection, int x, int y)
            {
                ParentParentSection = parentSection;
                X = x;
                Y = y;
            }

            public void SetFixedValue(byte initialValue)
            {
                ClearValue();
                Value = initialValue;
                IsFixed = true;
            }

            public void ClearValue()
            {
                Value = null;
                IsFixed = false;
                IsError = false;
                ErrorMessage = null;
            }

            public void SetValue(byte finalResult)
            {
                if (IsFixed)
                {
                    throw new InvalidOperationException("Cell contains fixed value. Value can not be set!");
                }
                
                if (Value != null)
                {
                    throw new InvalidOperationException("Final result can not set twice!");
                }

                Value = finalResult;
            }

            public void SetError(string errorMessage)
            {
                if (IsFixed)
                {
                    throw new InvalidOperationException("Cell is a fixed value. Errors can not be logged to fixed cells!");
                }

                IsError = true;
                ErrorMessage = errorMessage;
            }
        }
    }
}