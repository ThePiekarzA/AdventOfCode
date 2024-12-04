namespace AdventOfCode._2024._04;

public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}

public class CeresSearch
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/4/input
    private const string InputFilePath = @"2024\04\input.txt";

    private const string Xmas = "XMAS";
    private const string Mas = "MAS";

    #region Part One

    public static int RunPartOne()
    {
        return FindXmasByFile(InputFilePath);
    }

    public static int FindXmasByFile(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        var occurrences = 0;
        for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var currentLine = lines[lineIndex];
            for (var charIndex = 0; charIndex < currentLine.Length; charIndex++)
            {
                if (currentLine[charIndex] != 'X') continue;

                AnalyzeAllDirections(charIndex, lines, lineIndex, ref occurrences);
            }
        }

        return occurrences;
    }

    private static void AnalyzeAllDirections(int charIndex, string[] lines, int lineIndex, ref int occurrences)
    {
        if (IsRightCheckPossible(charIndex, lines[0]))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.Right, Xmas)) occurrences++;
        }
        if (IsLeftCheckPossible(charIndex))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.Left, Xmas)) occurrences++;
        }

        if (IsUpCheckPossible(lineIndex))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.Up, Xmas)) occurrences++;
        }
        if (IsDownCheckPossible(lines, lineIndex))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.Down, Xmas)) occurrences++;
        }

        if (IsDownCheckPossible(lines, lineIndex) && IsRightCheckPossible(charIndex, lines[0]))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.DownRight, Xmas)) occurrences++;
        }
        if (IsDownCheckPossible(lines, lineIndex) && IsLeftCheckPossible(charIndex))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.DownLeft, Xmas)) occurrences++;
        }

        if (IsUpCheckPossible(lineIndex) && IsLeftCheckPossible(charIndex))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.UpLeft, Xmas)) occurrences++;
        }
        if (IsUpCheckPossible(lineIndex) && IsRightCheckPossible(charIndex, lines[0]))
        {
            if (AnalyzeDirection(lines, lineIndex, charIndex, Direction.UpRight, Xmas)) occurrences++;
        }
    }

    #endregion

    #region Part Two

    public static int RunPartTwo()
    {
        return FindMasByFile(InputFilePath);
    }

    public static int FindMasByFile(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        var occurrences = 0;
        for (var lineIndex = 1; lineIndex < lines.Length - 1; lineIndex++)
        {
            var currentLine = lines[lineIndex];
            for (var charIndex = 1; charIndex < currentLine.Length - 1; charIndex++)
            {
                if (currentLine[charIndex] != 'A') continue;

                if (FindCrossedMas(charIndex, lines, lineIndex)) occurrences++;
            }
        }

        return occurrences;
    }

    private static bool FindCrossedMas(int charIndex, string[] lines, int lineIndex)
    {
        return (AnalyzeDirection(lines, lineIndex - 1, charIndex - 1, Direction.DownRight, Mas) ||
                AnalyzeDirection(lines, lineIndex + 1, charIndex + 1, Direction.UpLeft, Mas)) &&
               (AnalyzeDirection(lines, lineIndex - 1, charIndex + 1, Direction.DownLeft, Mas) ||
                AnalyzeDirection(lines, lineIndex + 1, charIndex - 1, Direction.UpRight, Mas));
    }

    #endregion

    private static bool AnalyzeDirection(string[] lines, int lineIndex, int charIndex, Direction dir, string word)
    {
        var stringContainer = new char[word.Length];
        for (var i = 0; i < word.Length; i++)
        {
            switch (dir)
            {
                case Direction.Up:
                    stringContainer[i] = lines[lineIndex - i][charIndex];
                    continue;
                case Direction.UpRight:
                    stringContainer[i] = lines[lineIndex - i][charIndex + i];
                    continue;
                case Direction.Right:
                    stringContainer[i] = lines[lineIndex][charIndex + i];
                    continue;
                case Direction.DownRight:
                    stringContainer[i] = lines[lineIndex + i][charIndex + i];
                    continue;
                case Direction.Down:
                    stringContainer[i] = lines[lineIndex + i][charIndex];
                    continue;
                case Direction.DownLeft:
                    stringContainer[i] = lines[lineIndex + i][charIndex - i];
                    continue;
                case Direction.Left:
                    stringContainer[i] = lines[lineIndex][charIndex - i];
                    continue;
                case Direction.UpLeft:
                    stringContainer[i] = lines[lineIndex - i][charIndex - i];
                    continue;
            }
        }

        return string.Join("", stringContainer) == word;
    }


    #region Directional check helpers

    private static bool IsLeftCheckPossible(int charIndex)
    {
        return charIndex >= Xmas.Length - 1;
    }

    private static bool IsRightCheckPossible(int charIndex, string currentLine)
    {
        return charIndex < currentLine.Length - (Xmas.Length - 1);
    }

    private static bool IsDownCheckPossible(string[] lines, int lineIndex)
    {
        return lineIndex < lines.Length - (Xmas.Length - 1);
    }

    private static bool IsUpCheckPossible(int lineIndex)
    {
        return lineIndex >= Xmas.Length - 1;
    }

    #endregion
}
