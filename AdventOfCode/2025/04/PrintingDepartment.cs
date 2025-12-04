namespace AdventOfCode._2025._04;

public class PrintingDepartment
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/4
    private const string InputFilePath = @"2025\04\input.txt";

    private const char Paper = '@';
    private const char Nothing = '.';

    public static int RunPartOne()
    {
        return CountAccessibleRolls(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return RemoveRolls(InputFilePath);
    }

    public static int RemoveRolls(string inputFilePath)
    {
        var paperGrid = ParseInputFile(inputFilePath);

        var rollsRemoved = 0;

        var gridLength = paperGrid[0].Length;
        var gridHeight = paperGrid.Length;
        
        var rollsToBeRemoved = new List<Tuple<int, int>>();
        do
        {
            rollsToBeRemoved.Clear();
            for (var y = 0; y < gridHeight; y++)
            {
                for (var x = 0; x < gridLength; x++)
                {
                    if (paperGrid[y][x] == Paper)
                    {
                        if (IsRollAccessible(paperGrid, y, x))
                            rollsToBeRemoved.Add(new Tuple<int, int>(x, y));
                    }
                }
            }

            foreach (var roll in rollsToBeRemoved)
            {
                paperGrid[roll.Item2][roll.Item1] = Nothing;
            }

            rollsRemoved += rollsToBeRemoved.Count;
        } while (rollsToBeRemoved.Count > 0);

        return rollsRemoved;
    }

    public static int CountAccessibleRolls(string inputFilePath)
    {
        var paperGrid = ParseInputFile(inputFilePath);

        var accessibleRollsNumber = 0;

        var gridLength = paperGrid[0].Length;
        var gridHeight = paperGrid.Length;

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridLength; x++)
            {
                if (paperGrid[y][x] == Paper)
                {
                    if (IsRollAccessible(paperGrid, y, x))
                        accessibleRollsNumber++;
                }
            }
        }

        return accessibleRollsNumber;
    }

    private static bool IsRollAccessible(char[][] paperGrid, int y, int x)
    {
        var neighbourRolls = 0;
        CheckNeighbour(paperGrid, y - 1, x - 1, ref neighbourRolls);
        CheckNeighbour(paperGrid, y - 1, x, ref neighbourRolls);
        CheckNeighbour(paperGrid, y - 1, x + 1, ref neighbourRolls);
        CheckNeighbour(paperGrid, y, x + 1, ref neighbourRolls);
        CheckNeighbour(paperGrid, y + 1, x + 1, ref neighbourRolls);
        CheckNeighbour(paperGrid, y + 1, x, ref neighbourRolls);
        CheckNeighbour(paperGrid, y + 1, x - 1, ref neighbourRolls);
        CheckNeighbour(paperGrid, y, x - 1, ref neighbourRolls);

        return neighbourRolls < 4;
    }

    private static void CheckNeighbour(char[][] paperGrid, int y, int x, ref int neighbourRolls)
    {
        if (y < 0 || y > (paperGrid[0].Length - 1) ||
            x < 0 || x > (paperGrid.Length - 1))
            return;

        if (paperGrid[y][x] == Paper)
        {
            neighbourRolls++;
        }
    }

    private static char[][] ParseInputFile(string inputFilePath)
    {
        var rawFile = File.ReadAllLines(inputFilePath);
        var paperGrid = rawFile.Select(r => r.ToArray()).ToArray(); 
        return paperGrid;
    }
}
