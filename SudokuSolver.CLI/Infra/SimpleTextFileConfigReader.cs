using System;
using System.IO;
using System.Linq;
using SudokuSolver.Core.Domain;
using SudokuSolver.Core.Infrastructure;

namespace SudokuSolverCLI.Infra
{
    /// <summary>
    /// Reads the configuration from a simple textfile in the following format.
    ///
    /// |123456789|
    /// |12 45 789|
    /// ...
    ///
    /// One digit per Sudoku cell.
    /// </summary>
    public class SimpleTextFileConfigReader
    {
        private readonly IFeedback feedback;

        public SimpleTextFileConfigReader(IFeedback feedback)
        {
            this.feedback = feedback;
        }
        
        public SudokuConfiguration ParseConfiguration(string filePath)
        {
            if (!File.Exists(filePath))
            {
                feedback.Error($"File [{filePath}] not found!");
                return null;
            }

            var values = ParseValues(filePath);
            if (values == null)
            {
                return null;
            }

            var board = ConvertValuesIntoBoard(values);
            
            return new SudokuConfiguration
            {
                InitialValues = board
            };
        }

        private byte[,]? ParseValues(string filePath, int numberOfSections = 3, int sectionWidth = 3, int sectionHeight = 3)
        {
            var lines = File.ReadLines(filePath).ToArray();
            var lineNo = 1;
            var y = 0;
            var numberOfRows = numberOfSections * sectionHeight;
            var numberOfCols = numberOfSections * sectionWidth;
            
            var values = new byte[numberOfCols, numberOfRows];

            foreach (var line in lines)
            {
                var text = line.Trim();

                var expectedNumberOfChars = numberOfCols + 2;
                if (text.Length != expectedNumberOfChars)
                {
                    LogParserError(lineNo, $"Expected {expectedNumberOfChars} but found {text.Length}!");
                    return null;
                }

                if (text[0] != '|' || text[^1] != '|')
                {
                    LogParserError(lineNo, $"Line must start and end with a '|'!");
                    return null;
                }

                for (var x = 0; x < numberOfCols; x++)
                {
                    var colNo = x + 1;
                    var digit = text[colNo].ToString();
                    if (!byte.TryParse(digit, out var value) && digit != " ")
                    {
                        LogParserError(lineNo, $"No valid byte value in column {colNo}, char '{digit}'!");
                        return null;
                    }

                    values[x, y] = digit == " " ? (byte)0 : value;
                }

                lineNo++;
                y++;
            }

            return values;
        }

        private Board ConvertValuesIntoBoard(byte[,]? values, int numberOfSections = 3, int sectionWidth = 3, int sectionHeight = 3)
        {
            if (values == null)
            {
                return null;
            }
            
            var board = new Board();

            for (int sectionY = 0; sectionY < numberOfSections; sectionY++)
            {
                for (int sectionX = 0; sectionX < numberOfSections; sectionX++)
                {
                    for (int cellX = 0; cellX < sectionWidth; cellX++)
                    {
                        for (int cellY = 0; cellY < sectionHeight; cellY++)
                        {
                            var value = values[
                                sectionX * sectionWidth + cellX,
                                sectionY * sectionHeight + cellY];
                            if (value > 0)
                            {
                                board.Sections[sectionX, sectionY].Cells[cellX, cellY].SetValue(value);
                            }
                        }
                    }
                }
            }

            return board;
        }

        private void LogParserError(int lineNo, string errorMessage)
            => feedback.Error($"Error in line {lineNo}: {errorMessage}");
    }
}