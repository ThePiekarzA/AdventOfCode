using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023._12;

public static partial class HotSprings
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2023/day/12/input
    private const string InputFilePath = @"2023\12\input.txt";

    [GeneratedRegex("[?]")]
    private static partial Regex UnknownConditionsRegex();

    [GeneratedRegex("[#]+")]
    private static partial Regex DamagedConditionsRegex();

    [GeneratedRegex("[#?]+")]
    private static partial Regex PossibleDamagedRegex();

    [GeneratedRegex(@"(?<=\.)(#+)(?=(\.|$))")]
    private static partial Regex CompleteDamagedGroupsRegex();

    public static int RunPartOne()
    {
        return SumPossibleArrangements(InputFilePath);
    }

    //"???.###"new[] { 1, 1, 3 }, 1
    public static void FasterApproach(string conditions, int[] damagedConditions)
    {
        // Sort damaged conditions descending to find largest damaged group
        Array.Sort(damagedConditions);
        Array.Reverse(damagedConditions);

        // Find all complete damagex condition groups
        var completeDamagedGroups = CompleteDamagedGroupsRegex().Matches(conditions);

        // Find all known damaged conditions
        var damagedGroups = DamagedConditionsRegex().Matches(conditions);
        

    }

    public static void UnfoldRecord(string foldedConditionRecord, string foldedConditions, out string unfoldedConditionRecord, out int[] unfoldedConditions)
    {
        var conditionRecord = new StringBuilder(foldedConditionRecord);
        var conditions = foldedConditions.Split(',').Select(int.Parse).ToList();
        for (var i = 0; i < 4; i++)
        {
            conditions.AddRange(foldedConditions.Split(',').Select(int.Parse).ToList());

            conditionRecord.Append('?');
            conditionRecord.Append(foldedConditionRecord);
        }

        unfoldedConditionRecord = conditionRecord.ToString();
        unfoldedConditions = conditions.ToArray();
    }

    public static int SumPossibleArrangements(string inputFilePath)
    {
        var rows = File.ReadAllLines(inputFilePath);

        var possibleArrangementsSum = 0;
        foreach (var row in rows)
        {
            var firstSplit = row.Split(' ');
            var damagedConditions = firstSplit[1].Split(',').Select(int.Parse).ToArray();

            possibleArrangementsSum += CountPossibleArrangements(firstSplit[0], damagedConditions);
        }

        return possibleArrangementsSum;
    }

    public static int CountPossibleArrangements(string damagedConditionRecord, int[] damagedConditions)
    {
        var unknownConditionsCount = damagedConditionRecord.Count(c => c == '?');
        var possibleCombinations = GetAllCombinations(unknownConditionsCount);

        var unknownConditionsLocations = UnknownConditionsRegex().Matches(damagedConditionRecord);

        var knownDamagedConditions = damagedConditionRecord.Count(c => c == '#');
        var expectedDamagedConditions = damagedConditions.Sum();

        var possibleArrangements = 0;
        var conditionRecordCandidate = new StringBuilder();
        Match location;
        int startIndex;
        int lastLocationIndex;
        string possibleArrangement;
        int i;
        foreach (var possibleCombination in possibleCombinations)
        {
            if (expectedDamagedConditions != knownDamagedConditions + possibleCombination.Count(c => c == '#'))
            {
                continue;
            }

            conditionRecordCandidate.Clear();

            for (i = 0; i < unknownConditionsCount; i++)
            {
                location = unknownConditionsLocations[i];
                startIndex = i == 0 ? 0 : unknownConditionsLocations[i - 1].Index + 1;
                
                conditionRecordCandidate.Append(damagedConditionRecord[startIndex..location.Index]);
                conditionRecordCandidate.Append(possibleCombination[i]);
            }

            lastLocationIndex = unknownConditionsLocations[^1].Index;
            if (lastLocationIndex != damagedConditionRecord.Length - 1)
            {
                conditionRecordCandidate.Append(damagedConditionRecord[(lastLocationIndex + 1)..]);
            }

            possibleArrangement = conditionRecordCandidate.ToString();
            if (DamagedConditionsRegex().Matches(possibleArrangement).Select(m => m.Length)
                .SequenceEqual(damagedConditions))
            {
                possibleArrangements++;
            }
        }

        return possibleArrangements;
    }

    private static IEnumerable<char[]> GetAllCombinations(int unknownConditionsCount)
    {
        var combinationsCount = (ulong)Math.Pow(2, unknownConditionsCount);
        var combination = new char[unknownConditionsCount];
        //var boolArray = new bool[64];
        int j;
        for (ulong i = 0; i < combinationsCount; i++)
        {
            //var bitArray = new BitArray(new[] { i });
            
            //bitArray.CopyTo(boolArray, 0);
            var bitRepresentation = Convert.ToString((long)i, 2);
            for (j = 0; j < unknownConditionsCount; j++)
            {
                combination[j] = bitRepresentation[j] == '0' ? '.' : '#';
            }

            yield return combination;
        }
    }
}
