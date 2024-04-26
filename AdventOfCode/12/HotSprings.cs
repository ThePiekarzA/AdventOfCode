using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._12;

public static partial class HotSprings
{
    private const string InputFilePath = @"12\input.txt";

    [GeneratedRegex("[?]")]
    private static partial Regex UnknownConditionsRegex();

    [GeneratedRegex("[#]+")]
    private static partial Regex DamagedConditionsRegex();

    public static int RunPartOne()
    {
        return SumPossibleArrangements(InputFilePath);
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
