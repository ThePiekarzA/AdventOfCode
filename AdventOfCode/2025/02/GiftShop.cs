namespace AdventOfCode._2025._02;

public class GiftShop
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/2
    private const string InputFilePath = @"2025\02\input.txt";

    public static ulong RunPartOne()
    {
        return FindInvalidIds(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return FindInvalidIdsMultiSequence(InputFilePath);
    }

    public static ulong FindInvalidIdsMultiSequence(string inputFilePath)
    {
        var ranges = ParseInputFile(inputFilePath);

        ulong invalidIdsSum = 0;

        foreach (var range in ranges)
        {
            for (var id = range.Item1; id <= range.Item2; id++)
            {
                if (AnalyzeId(id))
                    invalidIdsSum += id;
            }
        }

        return invalidIdsSum;
    }

    private static bool AnalyzeId(ulong id)
    {
        var stringId = id.ToString();

        for (var sequenceLength = stringId.Length / 2; sequenceLength > 0; sequenceLength--)
        {
            // Skip sequences that wont fully fit in id
            if (stringId.Length % sequenceLength != 0)
                continue;

            var sequenceOccurrences = stringId.Length / sequenceLength;

            var sequence = stringId[..sequenceLength];
            var isSequenceValid = true;
            for (var i = 1; i < sequenceOccurrences; i++)
            {
                if (!sequence.Equals(stringId.Substring(sequenceLength * i, sequenceLength)))
                {
                    isSequenceValid = false; 
                    break;
                }
            }

            if (isSequenceValid)
                return true;
        }

        return false;
    }

    public static ulong FindInvalidIds(string inputFilePath)
    {
        var ranges = ParseInputFile(inputFilePath);

        ulong invalidIdsSum = 0;

        foreach (var range in ranges)
        {
            for (var id = range.Item1; id <= range.Item2; id++)
            {
                var stringId = id.ToString();

                if (stringId.Length % 2 != 0)
                    continue;

                var halfLength = stringId.Length / 2;
                if (stringId[..halfLength] == stringId[halfLength..])
                    invalidIdsSum += id;
            }
        }

        return invalidIdsSum;
    }

    private static List<Tuple<ulong, ulong>> ParseInputFile(string inputFilePath)
    {
        var rawIds = File.ReadAllText(inputFilePath);
        var rawRanges = rawIds.Split(',');

        var idRanges = new List<Tuple<ulong, ulong>>();
        foreach (var range in rawRanges)
        {
            var ranges = range.Split('-');
            idRanges.Add(new Tuple<ulong, ulong>(
                ulong.Parse(ranges[0]), ulong.Parse(ranges[1])));
        }

        return idRanges;
    }
}
