using AdventOfCode.Common;

namespace AdventOfCode._2025._01;

enum Direction
{
    Left,
    Right
}

public class SecretEntrance
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/1
    private const string InputFilePath = @"2025\01\input.txt";

    public static int RunPartOne()
    {
        return FindPassword(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return FindPasswordMethod0x434C49434B(InputFilePath);
    }

    public static int FindPasswordMethod0x434C49434B(string instructionsFilePath)
    {
        var instructions = ParseInputFile(instructionsFilePath);

        var dialPosition = 50;
        var zeroPasses = 0;

        foreach (var instruction in instructions)
        {
            var directionSign = instruction.Key == Direction.Left ? -1 : 1;
            var distance = instruction.Value;

            for (var i = 0; i < distance; i++)
            {
                dialPosition += directionSign;

                if (Algebra.MathMod(dialPosition, 100) == 0)
                    zeroPasses++;
            }

            dialPosition = Algebra.MathMod(dialPosition, 100);
        }

        return zeroPasses;
    }

    public static int FindPassword(string instructionsFilePath)
    {
        var instructions = ParseInputFile(instructionsFilePath);

        var dialPosition = 50;
        var zeroStops = 0;

        foreach (var instruction in instructions)
        {
            var directionSign = instruction.Key == Direction.Left ? -1 : 1;
            dialPosition = Algebra.MathMod(dialPosition + instruction.Value * directionSign, 100);

            if (dialPosition == 0)
                zeroStops++;
        }

        return zeroStops;
    }

    private static List<KeyValuePair<Direction, int>> ParseInputFile(string path)
    {
        var instructions = new List<KeyValuePair<Direction, int>>();

        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var direction = line[0] == 'L' ? Direction.Left : Direction.Right;
            var steps = int.Parse(line.Substring(1));
            instructions.Add(new KeyValuePair<Direction, int>(direction, steps));
        }

        return instructions;
    }
}
