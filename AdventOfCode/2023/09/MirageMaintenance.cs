namespace AdventOfCode._2023._09;

public static class MirageMaintenance
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2023/day/9/input
    private const string InputFilePath = @"2023\09\input.txt";

    public static int RunPartOne()
    {
        return SumExtrapolationValues(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return SumExtrapolationValuesBackwards(InputFilePath);
    }

    public static int SumExtrapolationValues(string inputFilePath)
    {
        var file = File.ReadAllLines(inputFilePath);

        return file.Select(line => line.Split(' ').Select(int.Parse).ToList()).Select(ExtrapolateValue).Sum();
    }

    public static int SumExtrapolationValuesBackwards(string inputFilePath)
    {
        var file = File.ReadAllLines(inputFilePath);

        return file.Select(line => line.Split(' ').Select(int.Parse).ToList()).Select(ExtrapolateValueBackwards).Sum();
    }

    public static int ExtrapolateValue(List<int> history)
    {
        var extrapolationSequences = CalculateSequences(history);

        extrapolationSequences[^1].Add(0);
        for (var i = extrapolationSequences.Count - 2; i >= 0; i--)
        {
            var currentSequence = extrapolationSequences[i];
            var nextSequence = extrapolationSequences[i + 1];
            currentSequence.Add(currentSequence[^1] + nextSequence[^1]);
        }

        return extrapolationSequences[0][^1];
    }

    public static int ExtrapolateValueBackwards(List<int> history)
    {
        var extrapolationSequences = CalculateSequences(history);

        extrapolationSequences[^1].Add(0);
        for (var i = extrapolationSequences.Count - 2; i >= 0; i--)
        {
            var currentSequence = extrapolationSequences[i];
            var nextSequence = extrapolationSequences[i + 1];
            var extrapolatedValue = currentSequence[0] - nextSequence[0];
            currentSequence.Insert(0, extrapolatedValue);
        }

        return extrapolationSequences[0][0];
    }

    private static List<List<int>> CalculateSequences(List<int> history)
    {
        var extrapolationSequences = new List<List<int>>() { history };
        var currentSequence = extrapolationSequences[0];
        do
        {
            var nextSequence = new List<int>();
            for (var i = 1; i < currentSequence.Count; i++)
            {
                var difference = currentSequence[i] - currentSequence[i - 1];
                nextSequence.Add(difference);
            }

            extrapolationSequences.Add(nextSequence);
            currentSequence = extrapolationSequences[^1];
        } while (currentSequence.Any(v => v != 0));

        return extrapolationSequences;
    }
}
