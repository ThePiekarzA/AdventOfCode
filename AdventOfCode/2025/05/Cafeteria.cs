namespace AdventOfCode._2025._05;

public class Cafeteria
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/5
    private const string InputFilePath = @"2025\05\input.txt";

    public static ulong RunPartOne()
    {
        return CountFreshIngredients(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return CountFreshIngredientIds(InputFilePath);
    }

    public static ulong CountFreshIngredients(string inputFilePath)
    {
        ParseInputFile(inputFilePath, out var ranges, out var ingredientIds);

        ulong freshIngredients = 0;
        foreach(var id in ingredientIds)
        {
            if(ranges.Any(r => r.Item1 <= id && r.Item2 >= id))
                freshIngredients++;
        }

        return freshIngredients;
    }

    public static ulong CountFreshIngredientIds(string inputFilePath)
    {
        ParseInputFile(inputFilePath, out var ranges, out _);

        while(UnifyRanges(ranges)) {};

        ulong freshIdCount = 0;
        foreach (var range in ranges)
        {
            freshIdCount += range.Item2 - range.Item1 + 1;
        }

        return freshIdCount;
    }

    // Range cases
    // -----
    //         =====

    // -----
    //    =====

    // ------
    //      ====

    // -----
    //      ======

    // --------------
    //    ===

    // -----------
    //    ========

    // -----------
    // ===    
    private static bool UnifyRanges(List<Tuple<ulong, ulong>> ranges)
    {
        for (var i = 0; i < ranges.Count; i++)
        {
            for (var j = 0; j < ranges.Count; j++)
            {
                if (i == j)
                    continue;

                var firstRangeIndex = i;
                var secondRangeIndex = j;

                // Find out which range starts earlier
                if (ranges[secondRangeIndex].Item1 < ranges[firstRangeIndex].Item1)
                {
                    (secondRangeIndex, firstRangeIndex) = (firstRangeIndex, secondRangeIndex);
                }

                // Now first range start is the lowest id
                // Let's find if the ranges are overlapping
                if (ranges[secondRangeIndex].Item1 <= ranges[firstRangeIndex].Item2 + 1)
                {
                    // Ranges are overlapping
                    // Let's check if the second is not entirely overlapped by first
                    if (ranges[secondRangeIndex].Item2 > ranges[firstRangeIndex].Item2)
                    {
                        // We need to produce new range
                        ranges[firstRangeIndex] = new Tuple<ulong, ulong>(ranges[firstRangeIndex].Item1, ranges[secondRangeIndex].Item2);
                    }

                    // We can now get rid of the second range as it's covered by first
                    ranges.RemoveAt(secondRangeIndex);

                    return true;
                }
                else
                {
                    // Ranges are not overlapping, we can go to the next
                }
            }
        }

        return false;
    }

    private static void ParseInputFile(string inputFilePath, out List<Tuple<ulong, ulong>> ranges, out List<ulong> ids)
    {
        var lines = File.ReadAllLines(inputFilePath);

        ranges = new List<Tuple<ulong, ulong>>();
        ids = new List<ulong>();

        var lineIndex = 0;
        for (; lineIndex < lines.Length; lineIndex++)
        {
            if (string.IsNullOrEmpty(lines[lineIndex]))
            {
                lineIndex++;
                break; 
            }

            var range = lines[lineIndex].Split('-');
            ranges.Add(new Tuple<ulong, ulong>(ulong.Parse(range[0]), ulong.Parse(range[1])));
        }

        for (; lineIndex < lines.Length; lineIndex++)
        {
            ids.Add(ulong.Parse(lines[lineIndex]));
        }
    }
}
